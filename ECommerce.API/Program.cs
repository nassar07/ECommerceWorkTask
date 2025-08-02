using System.Text.Json.Serialization;
using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Presistence;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            //builder.Services.AddControllers().AddJsonOptions(x =>
            //{
            //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //});



            builder.Services.AddHttpContextAccessor();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ECommerceDbContext>()
                .AddDefaultTokenProviders();

            // Fix for CS1503: Pass a lambda to configure MediatRServiceConfiguration
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(MidiatrEntryPoint).Assembly);
            });

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

            // Fix for CS0747 and CS1003: Move the scoped service registration outside of the JwtBearer configuration block
            builder.Services.AddScoped<IRepository<Domain.Entities.Product>,ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository<Category>, CategoryRepository>();
            builder.Services.AddScoped<IECommerceDbContext, ECommerceDbContext>();
            builder.Services.AddScoped<IOrderRepository , OrderRepository>();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors
                (builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


            app.MapControllers();

            app.Run();
        }
    }
}
