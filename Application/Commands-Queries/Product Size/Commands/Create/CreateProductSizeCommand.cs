using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Commands_Queries.Product_Size.Commands
{
    public record CreateProductSizeCommand(ProducSizesDTO producSizes) : IRequest<int>;
    

}
