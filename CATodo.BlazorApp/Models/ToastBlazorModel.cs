namespace CATodo.BlazorApp.Models {
    public enum ToastLevel {
        Success, Warning, Error, Information
    }

    public class ToastBlazorModel {
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public ToastLevel Level { get; set; }
    }
}
