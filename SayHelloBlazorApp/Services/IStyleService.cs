using SayHelloBlazorApp.Models;

namespace SayHelloBlazorApp.Services {
    public interface IStyleService {
        Task<List<Couleur>> ObtenirLesCouleursAsync();
    }
}