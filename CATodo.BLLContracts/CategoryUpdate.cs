namespace CATodo.BLLContracts {
    public class CategoryUpdate {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}