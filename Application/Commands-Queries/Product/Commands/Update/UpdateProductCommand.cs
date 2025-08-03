using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Product.Commands.Update
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public UpdateProductDTO Product { get; set; }

        public UpdateProductCommand(UpdateProductDTO product)
        {
            Product = product;
        }
    }





}
