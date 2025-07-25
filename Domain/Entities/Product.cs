using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }         
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        
        public int CategoryId { get; set; }

        
        public Category Category { get; set; } = null!;
        public ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();
    }
}
