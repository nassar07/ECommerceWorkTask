using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.DTO.Product
{
    public class CreateProductDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public int CategoryId { get; set; }
        public string OwnerId { get; set; }

        [FromForm]
        public string SizesJson { get; set; } = null!;

        //[NotMapped]
        //public List<ProductSizeForCreateDTO> Sizes { get; set; } = new();
    }
}
