using CATodo.BlazorApp.Models;
using CATodo.BlazorApp.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CATodo.BlazorApp.Components {
    public partial class ToastComponent {
        public List<ToastBlazorModel> Toasts { get; set; } = new();

        [Inject] public IMessageService MessageService { get; set; } = null!;
        protected override void OnInitialized() {
            MessageService.OnNewMessage += DoOnNewMessage;
        }

        private void DoOnNewMessage(ToastBlazorModel toast) {
            _ = AddToast(toast);
        }

        private async Task AddToast(ToastBlazorModel toast) { 
            Toasts.Add(toast);
            StateHasChanged();

            await Task.Delay(5000);
            RemoveToast(toast); 
        }

        private void RemoveToast(ToastBlazorModel toast) {
            Toasts.Remove(toast);
            StateHasChanged();
        }
    }
}
