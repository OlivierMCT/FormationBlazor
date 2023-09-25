namespace SayHelloBlazorApp.Models {
    public class ListeCouleursDto {
        public List<CouleurDto> Results { get; set; } = null!;
    }
    public class CouleurDto {
        public string Name { get; set; } = null!;
        public string HexCode { get; set; } = null!;
    }
}
