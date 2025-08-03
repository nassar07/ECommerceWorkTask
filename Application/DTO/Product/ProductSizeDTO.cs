using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Product
{
    public class ProductSizeDTO
    {
        public int Id { get; set; }
        public string Size { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
