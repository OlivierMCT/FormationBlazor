using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATodo.DAL.Entities {
    public class CategoryEntity : CAEntityBase {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;

        public ICollection<TodoEntity> Todos { get; set; } = new HashSet<TodoEntity>();
    }
}
