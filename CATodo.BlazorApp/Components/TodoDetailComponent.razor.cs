using CATodo.BlazorApp.Models;
using CATodo.BLLContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CATodo.BlazorApp.Components {
    public partial class TodoDetailComponent {
        [Parameter] public Todo? Todo { get; set; }
        [Parameter] public Category? Category { get; set; }
        public TodoBlazorModel? TodoModel { get; set; }

        protected override void OnParametersSet() {
            if (Todo == null || Category == null) return;
            TodoModel = Todo.ToBlazorModel(Category);
        }

        [Inject] public IJSRuntime JSRuntime { get; set; } = null!;
        public void GoToMap() {
            if(TodoModel?.MapUrl == null) return;
            JSRuntime.InvokeVoidAsync("openNewTab", TodoModel.MapUrl);
        }

        [Parameter] public EventCallback<Todo> OnDeleting { get; set; }
        public void Remove() {
            if (Todo == null) return;
            OnDeleting.InvokeAsync(Todo);
        }

        [Parameter] public EventCallback<Todo> OnToggling { get; set; }
        public void ToggleCheck() {
            if (Todo == null) return;
            OnToggling.InvokeAsync(Todo);
        }
    }
}
