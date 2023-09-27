using CATodo.BlazorApp.Models;
using CATodo.BlazorApp.Services;
using CATodo.BLLContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace CATodo.BlazorApp.Components {
    public partial class TodoCreateComponent {
        [Inject] NavigationManager Navigator { get; set; } = null!;

        public List<Category> Categories { get; set; } = new();
        public TodoCreate TodoCreateModel { get; set; } = new() {
            DueDate = DateTime.Now.AddDays(1)
        };
        public string DebugTodoCreateModel {
            get { return System.Text.Json.JsonSerializer.Serialize(TodoCreateModel); }
        }

        [Inject] public ICATodoService TodoService { get; set; } = null!; 
        protected override async Task OnInitializedAsync() {
            Categories = (await TodoService.ListAllCategoriesAsync()).OrderBy(t => t.Name).ToList();
        }

        public void Save() {
            Navigator.NavigateTo("/");
        }
    }
}
