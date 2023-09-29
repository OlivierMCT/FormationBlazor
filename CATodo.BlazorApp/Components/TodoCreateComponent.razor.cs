using CATodo.BlazorApp.Models;
using CATodo.BlazorApp.Services;
using CATodo.BLLContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace CATodo.BlazorApp.Components {
    public partial class TodoCreateComponent {
        [Inject] NavigationManager Navigator { get; set; } = null!;

        public EditForm AjoutForm { get; set; } = null!;
        public bool IsFormInvalid { get { return AjoutForm.EditContext!.GetValidationMessages().Any(); } }
        public List<string> ErrorMessages { 
            get { return AjoutForm.EditContext!.GetValidationMessages().Distinct().ToList(); } 
        }
        public bool IsFormSubmitted { get; set; }
        public bool IsFormSubmitting { get; set; }


        public List<Category> Categories { get; set; } = new();
        public TodoCreate TodoCreateModel { get; set; } = new() {
            DueDate = DateTime.Now.AddDays(1)
        };
        public string DebugTodoCreateModel {
            get { return System.Text.Json.JsonSerializer.Serialize(TodoCreateModel); }
        }

        public void UpdateLatitude(double? value) {
            TodoCreateModel.Latitude = value;
            var fieldLongitude = AjoutForm.EditContext!.Field(nameof(TodoCreateModel.Longitude));
            AjoutForm.EditContext!.NotifyFieldChanged(fieldLongitude);
        }

        public void UpdateLongitude(double? value) {
            TodoCreateModel.Longitude = value;
            var fieldLatitude = AjoutForm.EditContext!.Field(nameof(TodoCreateModel.Latitude));
            AjoutForm.EditContext!.NotifyFieldChanged(fieldLatitude);
        }

        public void ClickSubmit() {
            IsFormSubmitted = true;
            IsFormSubmitting = true;
        }

        protected override async Task OnInitializedAsync() {
            Categories = (await TodoService.ListAllCategoriesAsync()).OrderBy(t => t.Name).ToList();
            AjoutForm.EditContext!.OnFieldChanged += (_, _) => {
                if (!IsFormSubmitting) IsFormSubmitted = false;
            };
            AjoutForm.EditContext!.OnValidationRequested += (_, _) => {
                if (IsFormSubmitting) {
                    var fieldLatitude = AjoutForm.EditContext!.Field(nameof(TodoCreateModel.Latitude));
                    AjoutForm.EditContext!.NotifyFieldChanged(fieldLatitude);

                    var fieldLongitude = AjoutForm.EditContext!.Field(nameof(TodoCreateModel.Longitude));
                    AjoutForm.EditContext!.NotifyFieldChanged(fieldLongitude);

                    IsFormSubmitting = false;
                }
            };
        }


        [Inject] public ICATodoService TodoService { get; set; } = null!;
        [Inject] public IMessageService MessageService { get; set; } = null!;
        public async void Save() {
            ToastBlazorModel? toast = null;
            try {
                Todo todo = await TodoService.CreateTodoAsync(TodoCreateModel);
                toast = new() {
                    Level = ToastLevel.Success,
                    Message = $"La tâche '{todo.Title}' a été ajouté avec le numéro {todo.Id}",
                    Title = "Enregistré"
                };
                Navigator.NavigateTo("/");
            } catch (CATodoException tex) {
                toast = new() { Level = ToastLevel.Error, Message = tex.Message, Title = "Erreur" };
            } finally {
                if (toast != null) { MessageService.PostMessage(toast); }
            }
        }
    }
}
