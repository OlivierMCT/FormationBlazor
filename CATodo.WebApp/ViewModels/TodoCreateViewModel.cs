using CATodo.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CATodo.WebApp.ViewModels {
    public class TodoCreateViewModel {
        [DisplayName("Catégorie")]
        [Required(ErrorMessage = "la catégorie est obligatoire")]
        public int? CategoryId { get; set; }

        [DisplayName("Titre")]
        [Required(ErrorMessage = "le titre est obligatoire")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "la description est obligatoire")] 
        public string? Description { get; set; }

        [DisplayName("A faire avant le")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "l'écheance est obligatoire")]
        public DateTime? DueDate { get; set; }
        
        [Range(-90, 90, ErrorMessage = "la latitude est invalide")]        
        public double? Latitude { get; set; }        
        [Range(-180, 180, ErrorMessage = "la longitude est invalide")]
        public double? Longitude { get; set; }

        public List<SelectListItem>? Categories { get; set; } = null!;
    }
}
