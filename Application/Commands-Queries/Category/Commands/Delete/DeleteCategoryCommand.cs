using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands_Queries.Category.Commands.Delete
{
    public record DeleteCategoryCommand(int Id) : IRequest<bool>;
    
}
