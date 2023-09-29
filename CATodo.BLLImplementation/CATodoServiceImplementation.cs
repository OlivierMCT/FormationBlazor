using CATodo.BLLContracts;
using CATodo.DAL;
using CATodo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CATodo.BLLImplementation {
    public class CATodoServiceImplementation : ICATodoService {
        public CATodoContext Context { get; set; }
        public CATodoServiceImplementation(CATodoContext context) {
            Context = context;
        }

        private void CheckTodoId(int todoId) {
            if (!Context.Todos.Any(t => t.TodoId == todoId))
                throw new CATodoException($"😮 la tâche n°{todoId} n'existe pas/plus");
        }
        private void CheckCategoryId(int categoryId) {
            if (!Context.Categories.Any(c => c.CategoryId == categoryId))
                throw new CATodoException($"😮 la catégorie n°{categoryId} n'existe pas/plus");
        }

        public ICollection<Todo> ListAllTodos() {
            return Context.Todos.Include(t => t.Category)
                .Select(t => t.ToTodo())
                .ToList();
        }

        public Todo ListOneTodo(int todoId) {
            CheckTodoId(todoId);
            return Context.Todos.Include(t => t.Category)
                .Where(t => t.TodoId == todoId)
                .Select(t => t.ToTodo())
                .First();
        }

        public ICollection<Todo> ListTodosByCategory(int categoryId) {
            CheckCategoryId(categoryId);
            return Context.Todos.Include(t => t.Category)
                .Where(t => t.CategoryId == categoryId)
                .Select(t => t.ToTodo())
                .ToList();
        }

        public ICollection<Todo> SearchTodos(string keyword) {
            return Context.Todos.Include(t => t.Category)
                .Where(t => t.Title.Contains(keyword))
                .Select(t => t.ToTodo())
                .ToList();
        }

        public void RemoveTodo(int todoId) {
            CheckTodoId(todoId);
            TodoEntity entity = Context.Todos.Find(todoId)!;
            if (!entity.IsRemovable())
                throw new CATodoException($"😮 la tâche n°{todoId} n'est pas supprimable");
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public Todo ToggleTodo(int todoId) {
            CheckTodoId(todoId);
            TodoEntity entity = Context.Todos.Find(todoId)!;
            entity.IsDone = !entity.IsDone;
            Context.SaveChanges();
            Context.Entry(entity).Reference(e => e.Category).Load();
            return entity.ToTodo();
        }

        public Todo CreateTodo(TodoCreate todoInfo) {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(todoInfo, new ValidationContext(todoInfo), errors, true)) {
                throw new CATodoException(string.Join(";", errors.Select(err => err.ErrorMessage)));
            }
            TodoEntity entity = new() { 
                CategoryId = todoInfo.CategoryId!.Value,
                Description = todoInfo.Description!,
                DueDate = todoInfo.DueDate!.Value,
                IsDone = false,
                Latitude = todoInfo.Latitude,
                Longitude = todoInfo.Longitude, 
                Title = todoInfo.Title!
            };
            Context.Add(entity);
            Context.SaveChanges();
            Context.Entry(entity).Reference(e => e.Category).Load();
            return entity.ToTodo();
        }

        public Todo UpdateTodo(TodoUpdate todoInfo) {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(todoInfo, new ValidationContext(todoInfo), errors, true)) {
                throw new CATodoException(string.Join(";", errors.Select(err => err.ErrorMessage)));
            }
            CheckTodoId(todoInfo.Id!.Value);
            TodoEntity entity = Context.Todos.Find(todoInfo.Id.Value)!;
            entity.Description = todoInfo.Description!;
            entity.DueDate = todoInfo.DueDate!.Value;
            entity.IsDone = todoInfo.IsDone!.Value;
            entity.Latitude = todoInfo.Latitude;
            entity.Longitude = todoInfo.Longitude;
            entity.Title = todoInfo.Title!;
            Context.SaveChanges();
            return entity.ToTodo();
        }

        public ICollection<Category> ListAllCategories() {
            List<int> countTodos = Context.Categories.Select(c => c.Todos.Count).ToList();
            double averageCountTodos = countTodos.Average();
            return Context.Categories
                    .Select(c => new { Entity = c, CountTodos = c.Todos.Count })
                    .Select(obj => obj.Entity.ToCategory(obj.CountTodos, averageCountTodos))
                    .ToList();
        }

        public Category ListOneCategory(int categoryId) {
            CheckCategoryId(categoryId);
            List<int> countTodos = Context.Categories.Select(c => c.Todos.Count).ToList();
            double averageCountTodos = countTodos.Average();
            return Context.Categories
                    .Where(c => c.CategoryId == categoryId)
                    .Select(c => new { Entity = c, CountTodos = c.Todos.Count })
                    .Select(obj => obj.Entity.ToCategory(obj.CountTodos, averageCountTodos))
                    .First();
        }

        public void RemoveCategory(int categoryId) {
            throw new NotImplementedException();
        }

        public Todo CreateCategory(CategoryCreate categoryInfo) {
            throw new NotImplementedException();
        }

        public Todo UpdateCategory(CategoryUpdate categoryInfo) {
            throw new NotImplementedException();
        }

        public Task<ICollection<Todo>> ListAllTodosAsync() {
            return Task.Run(() => ListAllTodos());
        }

        public Task<Todo> ListOneTodoAsync(int todoId) {
            return Task.Run(() => ListOneTodo(todoId));
        }

        public Task<ICollection<Todo>> ListTodosByCategoryAsync(int categoryId) {
            throw new NotImplementedException();
        }

        public Task<ICollection<Todo>> SearchTodosAsync(string keyword) {
            throw new NotImplementedException();
        }

        public Task RemoveTodoAsync(int todoId) {
            throw new NotImplementedException();
        }

        public Task<Todo> ToggleTodoAsync(int todoId) {
            throw new NotImplementedException();
        }

        public Task<Todo> CreateTodoAsync(TodoCreate todoInfo) {
            throw new NotImplementedException();
        }

        public Task<Todo> UpdateTodoAsync(TodoUpdate todoInfo) {
            throw new NotImplementedException();
        }

        public Task<ICollection<Category>> ListAllCategoriesAsync() {
            throw new NotImplementedException();
        }

        public Task<Category> ListOneCategoryAsync(int categoryId) {
            throw new NotImplementedException();
        }

        public Task RemoveCategoryAsync(int categoryId) {
            throw new NotImplementedException();
        }

        public Task<Todo> CreateCategoryAsync(CategoryCreate categoryInfo) {
            throw new NotImplementedException();
        }

        public Task<Todo> UpdateCategoryAsync(CategoryUpdate categoryInfo) {
            throw new NotImplementedException();
        }
    }
}