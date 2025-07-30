using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Category;
using MediatR;

namespace Application.Commands_Queries.Category.Commands.Create
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        public CreateCategoryHandler(ICategoryRepository<Domain.Entities.Category> context)
        {
            Context = context;
        }

        public ICategoryRepository<Domain.Entities.Category> Context { get; }

        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.Entities.Category
            { Name = request.category.Name };
            if (string.IsNullOrEmpty(category.Name))
            {
                return await Task.FromResult(false);
            }
            await Context.CreateCategoryAsync(category);
            await Context.SaveChangesAsync();
            return await Task.FromResult(true);

        }
    }
}
