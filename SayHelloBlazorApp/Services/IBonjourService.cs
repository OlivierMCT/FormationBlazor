namespace SayHelloBlazorApp.Services {
    public interface IBonjourService {
        string GenererMessage(string prenom);
        string ObtenirSalutation();
    }
}