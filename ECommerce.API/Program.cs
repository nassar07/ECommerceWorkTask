using Infrastructure.Identity;
using Infrastructure.Presistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace ECommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ECommerceDbContext>(
                options => options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                    ));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ECommerceDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                // User settings
                options.User.RequireUniqueEmail = true;
                
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        SaveSigninToken = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:issuer"],
                        ValidAudience = builder.Configuration["JWT:audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                    };
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //app.UseCors
            //    (builder =>
            //    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
           

            app.MapControllers();

            app.Run();
        }
    }
}
