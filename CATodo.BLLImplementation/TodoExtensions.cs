using CATodo.BLLContracts;
using CATodo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATodo.BLLImplementation {
    internal static class TodoExtensions {
        internal static Todo ToTodo(this TodoEntity entity) {
            return new Todo() {
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category.Name,
                Description = entity.Description,
                DueDate = entity.DueDate,
                Id = entity.TodoId,
                IsDone = entity.IsDone,
                IsRemovable = entity.IsRemovable(),
                Location = entity.GetLocation(),
                Status = entity.GetStatus(),
                Title = entity.Title
            };
        }

        internal static bool IsRemovable(this TodoEntity entity) {
            return entity.IsDone && entity.DueDate < DateTime.Today;
        }

        internal static TodoCoordinate? GetLocation(this TodoEntity entity) {
            return entity.Latitude.HasValue && entity.Longitude.HasValue ?
                new TodoCoordinate() {
                    Latitude = entity.Latitude.Value,
                    Longitude = entity.Longitude.Value
                } : null;
        }

        internal static TodoStatus GetStatus(this TodoEntity entity) {
            if(entity.IsDone) {
                return entity.DueDate < DateTime.Today.AddDays(-30) ? TodoStatus.Archived : TodoStatus.Closed;
            } else {
                return entity.DueDate < DateTime.Today ? TodoStatus.Late : TodoStatus.Pending;
            }
        }
    }
}
