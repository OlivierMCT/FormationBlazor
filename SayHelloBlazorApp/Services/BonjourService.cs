namespace SayHelloBlazorApp.Services {
    public class BonjourService : IBonjourService {
        public string ObtenirSalutation() {
            return DateTime.Now.Hour switch {
                < 5 or > 23 => "Bonne nuit",
                >= 17 => "Bonsoir",
                _ => "Bonjour"
            };
        }

        public string GenererMessage(string prenom) {
            return $"{ObtenirSalutation()} {prenom} !";
        }
    }
}
