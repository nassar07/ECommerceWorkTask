using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Product.Commands.Delete
{
    public record DeleteProductCommand(int ProductId) : IRequest<bool>;

}
