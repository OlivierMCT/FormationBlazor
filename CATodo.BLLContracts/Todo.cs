namespace CATodo.BLLContracts {
    public class Todo {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public string Description { get; init; } = null!;
        public DateTime DueDate { get; init; }
        public bool IsDone { get; init; }
        public bool IsRemovable { get; init; }
        public TodoStatus Status { get; init; }
        public TodoCoordinate? Location { get; init; }
        public int CategoryId { get; init; }
        public string CategoryName { get; init; } = null!;
    }

    public enum TodoStatus { 
        Archived, Closed, Pending, Late
    }

    public class TodoCoordinate { 
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}