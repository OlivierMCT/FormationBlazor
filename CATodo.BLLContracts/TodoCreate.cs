using Peri.Core;
using System.ComponentModel.DataAnnotations;

namespace CATodo.BLLContracts {
    public class TodoCreate {
        [Required(AllowEmptyStrings = false, ErrorMessage = "🧐 le titre est obligatoire")]
        [MaxLength(50, ErrorMessage = "🧐 le titre est trop long")]
        public string? Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "🧐 la description est obligatoire")]
        [MaxLength(255, ErrorMessage = "🧐 la description est trop longue")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "🧐 la date d'échéance est obligatoire")]
        [DateInFuture(IncludeToday = true, ErrorMessage = "🧐 la date d'échéance est invalide")]
        public DateTime? DueDate { get; set; }

        [Range(-90, 90, ErrorMessage = "🧐 la latitude est invalide")]
        public double? Latitude { get; set; }
        [Range(-180, 180, ErrorMessage = "🧐 la longitude est invalide")]
        public double? Longitude { get; set; }

        [Required(ErrorMessage = "🧐 la catégorie est obligatoire")]
        public int? CategoryId { get; set; }
    }
}