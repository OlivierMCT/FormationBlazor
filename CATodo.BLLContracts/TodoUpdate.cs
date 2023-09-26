using Peri.Core;
using System.ComponentModel.DataAnnotations;

namespace CATodo.BLLContracts {
    public class TodoUpdate {
        [Required(ErrorMessage = "🧐 l'identifiant est obligatoire")]
        public int? Id { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "🧐 le titre est obligatoire")]
        [MaxLength(50, ErrorMessage = "🧐 le titre est trop long")]
        public string? Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "🧐 la description est obligatoire")]
        [MaxLength(255, ErrorMessage = "🧐 la description est trop longue")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "🧐 la date d'échéance est obligatoire")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "🧐 l'état est obligatoire")]
        public bool? IsDone { get; init; }

        [Range(-90, 90, ErrorMessage = "🧐 la latitude est invalide")]
        public double? Latitude { get; set; }
        [Range(-180, 180, ErrorMessage = "🧐 la longitude est invalide")]
        public double? Longitude { get; set; }
    }
}