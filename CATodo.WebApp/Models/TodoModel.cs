using CATodo.BLLContracts;
using System.ComponentModel.DataAnnotations;

namespace CATodo.WebApp.Models {
    public class TodoModel {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public string Description { get; init; } = null!;
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DueDate { get; init; }
        public bool IsDone { get; init; }
        public bool IsRemovable { get; init; }
        public TodoStatus Status { get; init; }
        public string StatusText { get; init; } = null!;
        public bool IsLocation { get; init; }
        public string? Location { get; init; }
        public string? MapUrl { get; init; }
        public CategoryModel Category { get; init; } = null!;
    }
}
