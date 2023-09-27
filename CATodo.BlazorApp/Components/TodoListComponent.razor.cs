using CATodo.BlazorApp.Models;
using CATodo.BlazorApp.Services;
using CATodo.BLLContracts;
using Microsoft.AspNetCore.Components;

namespace CATodo.BlazorApp.Components {
    public partial class TodoListComponent {
        [Inject] public ICATodoService TodoService { get; set; } = null!;

        public List<Category> Categories { get; set; } = new();
        public List<Category> SelectedCategories { get; set; } = new();
        public void ToggleCategory(Category c) {
            if (IsSelected(c)) {
                SelectedCategories.Remove(c);
            } else {
                SelectedCategories.Add(c);
            }
        }
        public bool IsSelected(Category c) => SelectedCategories.Contains(c);
        public bool IsDisplayable(Todo t) => SelectedCategories.Any(c => c.Id == t.CategoryId);

        public List<Todo> Todos { get; set; } = new();

        public bool IsLoading { get; set; }
        public bool IsCategoriesMenuOpen { get; set; } = false;

        private bool _isTodosSortedByTitle = true;
        public bool IsTodosSortedByTitle {
            get { return _isTodosSortedByTitle; }
            set {
                _isTodosSortedByTitle = value;
                var req = Todos.AsEnumerable();
                req = IsTodosSortedByTitle ? req.OrderBy(t => t.Title) : req.OrderByDescending(t => t.Title);
                Todos = req.ToList();
            }
        }

        protected override async Task OnInitializedAsync() {
            IsLoading = true;
            var taskCategories = TodoService.ListAllCategoriesAsync();
            var taskTodos = TodoService.ListAllTodosAsync();
            await Task.WhenAll(taskCategories, taskTodos);
            Categories = taskCategories.Result.OrderBy(t => t.Name).ToList();
            Categories.ForEach(c => SelectedCategories.Add(c));
            Todos = taskTodos.Result.OrderBy(t => t.Title).ToList();
            IsLoading = false;
        }

        public Category? GetCategoryFor(Todo todo) {
            return Categories.FirstOrDefault(c => c.Id == todo.CategoryId);
        }

        [Inject] public IMessageService MessageService { get; set; } = null!;
        public async Task DeleteTodoAsync(Todo todo) {
            try {
                await TodoService.RemoveTodoAsync(todo.Id);

                Todos.RemoveAt(Todos.FindIndex(t => t.Id == todo.Id));
                //Todos = (await TodoService.ListAllTodosAsync()).OrderBy(t => t.Title).ToList();
            } catch (CATodoException tex) {
                MessageService.PostMessage(new ToastBlazorModel() {
                    Level = ToastLevel.Error,
                    Message = tex.Message,
                    Title = "Erreur"
                });
            }
        }

        public async Task ToggleTodoAsync(Todo todo) {
            try {
                Todo todoUpdated = await TodoService.ToggleTodoAsync(todo.Id);
                int index = Todos.FindIndex(t => t.Id == todo.Id);
                Todos.RemoveAt(index);
                Todos.Insert(index, todoUpdated);
            } catch (CATodoException tex) {
                MessageService.PostMessage(new ToastBlazorModel() {
                    Level = ToastLevel.Error,
                    Message = tex.Message,
                    Title = "Erreur"
                });
            }
        }
    }
}
