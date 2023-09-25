using SayHelloBlazorApp.Models;

namespace SayHelloBlazorApp.Services {
    public class StyleService {
        public List<Couleur> ObtenirLesCouleurs() {
            return new List<Couleur>() { 
                new Couleur() { Code = "red", Libelle = "rouge vif" },
                new Couleur() { Code = "lime", Libelle = "vert fluo" },
                new Couleur() { Code = "aqua", Libelle = "bleu océan" },
                new Couleur() { Code = "black", Libelle = "noir absolu" },
                new Couleur() { Code = "purple", Libelle = "pluie violette" },
                new Couleur() { Code = "yellow", Libelle = "jaune citron" },
                new Couleur() { Code = "pink", Libelle = "rose bonbon" },
                new Couleur() { Code = "silver", Libelle = "gris souris" },
            };
        }
    }
}
