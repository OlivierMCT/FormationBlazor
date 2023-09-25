using SayHelloBlazorApp.Models;
using System.Net.Http.Json;

namespace SayHelloBlazorApp.Services {
    public class StyleService : IStyleService {
        private readonly HttpClient _http;
        public StyleService(HttpClient http) { this._http = http; }

        public async Task<List<Couleur>> ObtenirLesCouleursAsync() {
            var couleurs = await _http.GetFromJsonAsync<ListeCouleursDto>(_http.BaseAddress + "?limit=1000");
            return couleurs!.Results
                .Select(dto => new Couleur() { Code = dto.HexCode, Libelle = dto.Name }).ToList();
        }
    }
}
