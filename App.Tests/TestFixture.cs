using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Product.Commands.Create;
using Infrastructure;
using Infrastructure.Presistence;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.Tests
{
    public class TestFixture
    {
        public ServiceProvider ServiceProvider { get; }

        public TestFixture()
        {
            var services = new ServiceCollection();
            
            
            services.AddLogging(builder => builder.AddConsole());


            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);
            });


            services.AddDbContext<ECommerceDbContext>(options => options.UseInMemoryDatabase("TestDb_"));

            services.AddScoped<IRepository<Domain.Entities.Product>, ProductRepository>();

            var fileServiceMock = new Mock<IFileService>();

            fileServiceMock.Setup(f => f.SaveImageAsync(It.IsAny<IFormFile>())).ReturnsAsync("images/test.png");

            services.AddSingleton<IFileService>(fileServiceMock.Object);

            ServiceProvider = services.BuildServiceProvider();

        }


        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = ServiceProvider.CreateScope();
            var mediatr = scope.ServiceProvider.GetService<IMediator>();
            return await mediatr.Send(request);
        }


        public async Task<T?> FindAsync<T>(int id) where T : class
        {
            using var scope = ServiceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
            return await db.Set<T>().FindAsync(id);
        }



    }
}
