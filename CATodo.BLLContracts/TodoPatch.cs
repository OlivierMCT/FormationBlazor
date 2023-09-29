using Peri.Core;
using System.ComponentModel.DataAnnotations;

namespace CATodo.BLLContracts {
    public class TodoPatch {
        [Required(ErrorMessage = "🧐 l'identifiant est obligatoire")]
        public int? Id { get; set; }
        
        [Required(ErrorMessage = "🧐 l'état est obligatoire")]
        public bool? IsDone { get; init; }
    }
}