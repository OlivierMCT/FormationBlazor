using CATodo.BLLContracts;

namespace CATodo.WebApp.Models.Extensions {
    public static class CategoryModelExtensions {
        public static CategoryModel ToCategoryModel(this Category model) {
            return new CategoryModel() {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                IsPopular = model.IsPopular
            };
        }
    }
}

