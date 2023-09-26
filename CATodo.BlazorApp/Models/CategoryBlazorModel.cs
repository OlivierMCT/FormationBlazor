namespace CATodo.BlazorApp.Models {
    public class CategoryBlazorModel {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string Color { get; init; } = null!;
        public bool IsPopular { get; init; }
    }
}