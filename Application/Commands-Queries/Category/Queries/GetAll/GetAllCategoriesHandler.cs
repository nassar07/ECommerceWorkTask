using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Category;
using MediatR;

namespace Application.Category.Queries.GetAll
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDTO>>
    {
        public GetAllCategoriesHandler(ICategoryRepository<Domain.Entities.Category> context)
        {
            Context = context;
        }

        public ICategoryRepository<Domain.Entities.Category> Context { get; }

        public async Task<List<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await Context.GetAllCategoriesAsync();
            if (categories == null)
            {
                throw new InvalidOperationException("No categories found.");
            }
            return Task.FromResult(categories).Result;

        }
    }
}
