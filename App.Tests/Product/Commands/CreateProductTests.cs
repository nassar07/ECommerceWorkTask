using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using Application.Product.Commands.Create;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace App.Tests.Product.Commands
{
    public class CreateProductTests : IClassFixture<TestFixture>
    {
        public CreateProductTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        public TestFixture _fixture { get; }



        [Fact]
        public async Task Should_Create_Product_Successfully()
        {
            var dto = new CreateProductDTO
            {
                Title = "Test Product",
                Description = "Test Description",
                CategoryId = 1,
                OwnerId = "d98e0a20-f179-407e-8bb0-4fd0c16f0ce5",
                SizesJson = JsonConvert.SerializeObject(new List<ProductSizeForCreateDTO>
            {
                new ProductSizeForCreateDTO { Size = "M", Price = 100, Quantity = 5 }
            }),
                Image = new FormFile(Stream.Null, 0, 0, "Image", "test.png")
            };

            var command = new CreateProductCommand(dto);


            var result = await _fixture.SendAsync(command);


            result.Should().NotBeNull();
            result.Title.Should().Be(dto.Title);
            result.Description.Should().Be(dto.Description);
            result.ImageUrl.Should().Be("images/test.png");


            var savedProduct = await _fixture.FindAsync<Domain.Entities.Product>(result.Id);


            savedProduct.Should().NotBeNull();
            savedProduct!.Title.Should().Be(dto.Title);
            
            
        }

    }
}
