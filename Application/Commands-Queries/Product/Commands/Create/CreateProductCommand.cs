using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Product.Commands.Create
{
    public class CreateProductCommand : IRequest<ProductResponseDTO>
    {
        public CreateProductDTO ProductFromRequest { get; set; }

        public CreateProductCommand(CreateProductDTO dto)
        {
            ProductFromRequest = dto;
        }
    }
}
