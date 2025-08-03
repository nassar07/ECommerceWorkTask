using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.Product
{
    public class UpdateProductDTO
    {
        public int ProductId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string SizesJson { get; set; }
    }

}
