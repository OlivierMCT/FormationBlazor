using CATodo.BLLContracts;
using System.Globalization;
using System.Reflection;

namespace CATodo.BlazorApp.Models {
    public static class ModelsExtensions {
        public static TodoBlazorModel ToBlazorModel(this Todo todo, Category category) {
            return new TodoBlazorModel() {
                Category = category.ToBlazorModel(),
                Description = todo.Description,
                DueDate = todo.DueDate,
                Id = todo.Id,
                IsDone = todo.IsDone,
                IsLocation = todo.Location != null,
                IsRemovable = todo.IsRemovable,
                Location = todo.GetLocationText(),
                MapUrl = todo.GetLocationUrl(),
                Status = todo.Status,
                StatusText = todo.GetStatusText(),
                Title = todo.Title,
            };
        }

        public static string GetLocationText(this Todo model) {
            if (model.Location == null) return "";
            return string.Format("{0:N6}x{1:N6}", model.Location.Latitude, model.Location.Longitude);
        }
        public static string GetLocationUrl(this Todo model) {
            if (model.Location == null) return "";
            return string.Format(new CultureInfo("en-US"), "https://www.google.com/maps/?q={0},{1}", model.Location.Latitude, model.Location.Longitude);
        }
        public static string GetStatusText(this Todo model) {
            return model.Status switch {
                TodoStatus.Archived => "c'est fait mais ça date",
                TodoStatus.Closed => "ça c'est fait",
                TodoStatus.Pending => "trankil 😎",
                TodoStatus.Late => "on se bouge !",
                _ => ""
            };
        }

        public static CategoryBlazorModel ToBlazorModel(this Category category) {
            return new CategoryBlazorModel() {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                IsPopular = category.IsPopular
            };
        }
    }
}
