using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Category;
using MediatR;

namespace Application.Category.Queries.GetById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO>
    {
        public GetCategoryByIdHandler(ICategoryRepository<Domain.Entities.Category> context)
        {
            Context = context;
        }

        public ICategoryRepository<Domain.Entities.Category> Context { get; }

        public async Task<CategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
            {
                throw new ArgumentException("Category ID must be greater than zero.", nameof(request.id));
            }
            var Category = await Context.GetCategoryByIdAsync(request.id);

            if (Category == null)
                return null;

            return new CategoryDTO
            { Id = Category.Id, Name = Category.Name };

        }
    }
}
