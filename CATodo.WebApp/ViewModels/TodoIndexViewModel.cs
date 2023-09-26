using CATodo.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CATodo.WebApp.ViewModels {
    public class TodoIndexViewModel {
        public List<TodoModel> Todos { get; set; } = null!;
        public List<CategoryModel> Categories { get; set; } = null!;
        public List<SelectListItem> CategoryListItems { get; set; } = null!;
    }
}
