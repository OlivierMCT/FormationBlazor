using CATodo.BLLContracts;
using CATodo.WebApp.Models.Extensions;
using CATodo.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CATodo.WebApp.Controllers {
    public class TodoController : Controller {
        public ICATodoService TodoService { get; }
        public TodoController(ICATodoService todoService) {
            TodoService = todoService;
        }

        public IActionResult Index([FromQuery] string? categoryId) {
            var categories = TodoService.ListAllCategories().OrderBy(c => c.Name).Select(c => c.ToCategoryModel());

            int? catId = null;
            if (int.TryParse(categoryId, out int id)) { catId = id; }
            var data = catId.HasValue ? TodoService.ListTodosByCategory(catId.Value) : TodoService.ListAllTodos();
            var todos = data.OrderBy(t => t.Title).Select(
                t => t.ToTodoModel(categories.First(c => c.Id == t.CategoryId))
            ).ToList();

            var categoryListItems = new List<SelectListItem>() { new SelectListItem() { Text = "Toutes", Value = "" } };
            categoryListItems.AddRange(
                categories.Select(c => new SelectListItem() {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = c.Id == catId
                })
            );
            var vm = new TodoIndexViewModel() {
                Categories = categories.ToList(),
                Todos = todos.ToList(),
                CategoryListItems = categoryListItems
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete([FromQuery] string? categoryId) {
            TodoService.RemoveTodo(int.Parse(HttpContext.Request.Form["todoId"].ToString()));
            return RedirectToAction("Index", new { categoryId = categoryId });
        }

        [HttpPost]
        public IActionResult Toggle([FromQuery] string? categoryId) {
            TodoService.ToggleTodo(int.Parse(HttpContext.Request.Form["todoId"].ToString()));
            return RedirectToAction("Index", new { categoryId = categoryId });
        }

        [HttpGet]
        public IActionResult Create() {
            var vm = new TodoCreateViewModel() {
                DueDate = DateTime.Today,
                Categories = GenerateCategoryListItems()
            };
            return View(vm);
        }

        private List<SelectListItem> GenerateCategoryListItems() {
            var categories = TodoService.ListAllCategories().OrderBy(c => c.Name).Select(c => c.ToCategoryModel());
            var categoryListItems = new List<SelectListItem>() {
                new SelectListItem() { Text = "Choisissez une catégorie", Value = "" }
            };
            categoryListItems.AddRange(
                categories.Select(c => new SelectListItem() {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            );
            return categoryListItems;
        }

        [HttpPost]
        public IActionResult Create(TodoCreateViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    TodoService.CreateTodo(new TodoCreate() {
                        CategoryId = vm.CategoryId,
                        Description = vm.Description,
                        DueDate = vm.DueDate,
                        Latitude = vm.Latitude,
                        Longitude = vm.Longitude,
                        Title = vm.Title
                    });
                    return RedirectToAction("Index");
                } catch (CATodoException ex) {
                    ex.Message
                        .Split(";")
                        .ToList()
                        .ForEach(m => ModelState.AddModelError("", m));
                }
            }
            vm.Categories = GenerateCategoryListItems();
            return View(vm);
        }
    }
}
