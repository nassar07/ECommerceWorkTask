using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Order
{
    public class CreateOrderResultDto
    {
        public int OrderId { get; set; }
        public string PaymentUrl { get; set; }
    }
}
