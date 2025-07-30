using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Commands_Queries.Category.Commands.Delete
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        public DeleteCategoryHandler(ICategoryRepository<Domain.Entities.Category> context)
        {
            Context = context;
        }

        public ICategoryRepository<Domain.Entities.Category> Context { get; }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var query = await Context.DeleteCategoryAsync(request.Id);
            if (query == false)
                return false;
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
