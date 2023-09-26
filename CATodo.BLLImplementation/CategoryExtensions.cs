using CATodo.BLLContracts;
using CATodo.DAL.Entities;

namespace CATodo.BLLImplementation {
    internal static class CategoryExtensions {
        internal static Category ToCategory(this CategoryEntity entity, int countTodos, double averageCountTodos) {
            return new Category() {
                Color = entity.Color,
                Id = entity.CategoryId,
                Name = entity.Name,
                IsPopular = countTodos > averageCountTodos
            };
        }

    }
}
