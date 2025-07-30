using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Category;
using MediatR;

namespace Application.Commands_Queries.Category.Commands.Update
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDTO>
    {
        private readonly ICategoryRepository<Domain.Entities.Category> _context;
        public UpdateCategoryHandler(ICategoryRepository<Domain.Entities.Category> context)
        {
            _context = context;
        }
        public async Task<CategoryDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new ArgumentException("Category ID must be greater than zero.", nameof(request.Id));
            }
            var existingCategory = await _context.GetCategoryByIdAsync(request.Id);
            if (existingCategory == null)
            {
                return null;
            }
            existingCategory.Name = request.Category.Name;
            var updatedCategory = await _context.UpdateCategoryAsync(request.Id, existingCategory);
            await _context.SaveChangesAsync();
            return new CategoryDTO
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name
            };
        }
    }
}
