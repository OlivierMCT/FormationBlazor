using CATodo.BLLContracts;
using System.Globalization;

namespace CATodo.WebApp.Models.Extensions {
    public static class TodoModelExtensions {
        public static TodoModel ToTodoModel(this Todo model, CategoryModel category) {
            return new TodoModel() { 
                Category = category,
                Description = model.Description,
                DueDate = model.DueDate,
                Id = model.Id,
                IsDone = model.IsDone,
                IsLocation = model.Location != null,
                IsRemovable = model.IsRemovable,
                Location = model.GetLocationText(),
                MapUrl = model.GetLocationUrl(),
                Status = model.Status,
                StatusText = model.GetStatusText(),
                Title = model.Title,
            };
        }

        public static string GetLocationText(this Todo model) {
            if (model.Location == null) return "";
            return string.Format("{0:N6}x{1:N6}", model.Location.Latitude, model.Location.Longitude);
        }
        public static string GetLocationUrl(this Todo model) {
            if (model.Location == null) return "";
            return string.Format(new CultureInfo("en-US") ,"https://www.google.com/maps/?q={0},{1}", model.Location.Latitude, model.Location.Longitude);
        }
        public static string GetStatusText(this Todo model) {
            return model.Status switch {
                TodoStatus.Archived => "c'est fait mais ca date",
                TodoStatus.Closed => "ca c'est fait",
                TodoStatus.Pending => "trankil 😎",
                TodoStatus.Late => "on se bouge !",
                _ => ""
            };
        }
    }
}

