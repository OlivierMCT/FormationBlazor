using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATodo.DAL.Entities {
    public class TodoEntity : CAEntityBase {
        public int TodoId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public CategoryEntity Category { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}

