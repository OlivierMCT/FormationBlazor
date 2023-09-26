using CATodo.BLLContracts;
using Microsoft.AspNetCore.Components;

namespace CATodo.BlazorApp.Components {
    public partial class TodoListComponent {
        [Inject] public ICATodoService TodoService { get; set; } = null!;
        public List<Category> Categories { get; set; } = new ();
        public bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync() {
            IsLoading = true;
            var taskCategories = TodoService.ListAllCategoriesAsync();
            await Task.WhenAll(taskCategories);
            Categories = taskCategories.Result.OrderBy(t => t.Name).ToList();
            IsLoading = false;
        }
    }
}
