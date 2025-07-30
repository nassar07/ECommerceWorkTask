using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Product.Commands.Update
{
    public record UpdateProductCommand(int ProductId, UpdateProductDTO Product) : IRequest<bool>;




}
