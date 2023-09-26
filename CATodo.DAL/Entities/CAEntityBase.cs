using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATodo.DAL.Entities {
    public abstract class CAEntityBase {
        public DateTime LastUpdated { get; set; }
        public Guid RowId { get; set; }
    }
}
