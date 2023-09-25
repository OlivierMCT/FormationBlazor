using Microsoft.AspNetCore.Components;
using SayHelloBlazorApp.Models;
using SayHelloBlazorApp.Services;

namespace SayHelloBlazorApp.Components {
    public partial class Bonjour {
        public string Prenom { get; set; } = "Olivier";

        public Couleur? Couleur { get; set; }
        public string CodeCouleur { 
            get => Couleur?.Code ?? string.Empty;
            set => Couleur = Couleurs.FirstOrDefault(c => c.Code == value);
        }
        public List<Couleur> Couleurs { get; set; } = new List<Couleur>();

        public string Message { get => EstMessageAffichable ? BonjourService.GenererMessage(Prenom) : ""; }
        public bool EstMessageAffichable { 
            get { return !string.IsNullOrWhiteSpace(Prenom) && Couleur != null;  } 
        }

        [Inject] public IBonjourService BonjourService { get; set; } = null!;
        [Inject] public IStyleService StyleService { get; set; } = null!;

        public bool CouleursEnChargement { get; set; }
        protected override async Task OnInitializedAsync() {
            CouleursEnChargement = true;
            await Task.Delay(5000);
            Couleurs = (await StyleService.ObtenirLesCouleursAsync()).OrderBy(c => c.Libelle).ToList();
            CouleursEnChargement = false;
        }
    }
}
