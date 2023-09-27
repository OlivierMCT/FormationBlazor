using CATodo.BlazorApp.Models;

namespace CATodo.BlazorApp.Services {
    public interface IMessageService {
        event Action<ToastBlazorModel> OnNewMessage;

        void PostMessage(ToastBlazorModel toast);
    }

    public class MessageService : IMessageService {
        public event Action<ToastBlazorModel> OnNewMessage = null!;

        public void PostMessage(ToastBlazorModel toast) {
            OnNewMessage.Invoke(toast);
        }
    }
}
