using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }
        public string Size { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public int ProductId { get; set; }

        
        public Product Product { get; set; } = null!;
    }
}
