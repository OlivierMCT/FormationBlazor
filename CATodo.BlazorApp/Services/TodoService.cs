using CATodo.BLLContracts;
using System.Net.Http.Json;

namespace CATodo.BlazorApp.Services {
    public class TodoService : ICATodoService {        
        #region Méthodes Synchrones
        public ICollection<Todo> ListAllTodos() {
            throw new NotImplementedException();
        }

        public Todo ListOneTodo(int todoId) {
            throw new NotImplementedException();
        }

        public ICollection<Todo> ListTodosByCategory(int categoryId) {
            throw new NotImplementedException();
        }

        public ICollection<Todo> SearchTodos(string keyword) {
            throw new NotImplementedException();
        }

        public void RemoveTodo(int todoId) {
            throw new NotImplementedException();
        }

        public Todo ToggleTodo(int todoId) {
            throw new NotImplementedException();
        }

        public Todo CreateTodo(TodoCreate todoInfo) {
            throw new NotImplementedException();
        }

        public Todo UpdateTodo(TodoUpdate todoInfo) {
            throw new NotImplementedException();
        }

        public ICollection<Category> ListAllCategories() {
            throw new NotImplementedException();
        }

        public Category ListOneCategory(int categoryId) {
            throw new NotImplementedException();
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
        #endregion

        private readonly HttpClient _http;
        public TodoService(HttpClient http) {
            this._http = http;
        }

        public async Task<ICollection<Todo>> ListAllTodosAsync() {
            string url = _http.BaseAddress + "todo";
            List<Todo>? todos = await _http.GetFromJsonAsync<List<Todo>>(url);
            if (todos == null) Console.Error.WriteLine("pas de tâche");
            return todos ?? new List<Todo>();
        }

        public async Task<Todo> ListOneTodoAsync(int todoId) {
            string url = _http.BaseAddress + "todo/" + todoId;
            return await _http.GetFromJsonAsync<Todo>(url) ?? throw new CATodoException("pas de tâche n°" + todoId);
        }

        public Task<ICollection<Todo>> ListTodosByCategoryAsync(int categoryId) {
            throw new NotImplementedException();
        }

        public Task<ICollection<Todo>> SearchTodosAsync(string keyword) {
            throw new NotImplementedException();
        }

        public async Task RemoveTodoAsync(int todoId) {
            string url = _http.BaseAddress + "todo/" + todoId;
            var response = await _http.DeleteAsync(url);
            if (! response.IsSuccessStatusCode)
            {
                string msg = "Impossible de supprimer la tâche.";
                msg += response.StatusCode switch {
                    System.Net.HttpStatusCode.NotFound => " Elle n'existe pas !",
                    System.Net.HttpStatusCode.Unauthorized => " Identifiez-vous d'abord.",
                    System.Net.HttpStatusCode.Forbidden => " Pas les habilitations pour le faire...",
                    System.Net.HttpStatusCode.BadRequest => ExtractMessage(response),
                    _ => ""
                };
                throw new CATodoException(msg);
            }
        }

        private string ExtractMessage(HttpResponseMessage response) {
            var task = response.Content.ReadAsStringAsync();
            task.Wait();
            return task.Result;
        }

        public async Task<Todo> ToggleTodoAsync(int todoId) {
            Todo todoToPatch = await ListOneTodoAsync(todoId);            
            string url = _http.BaseAddress + "todo/" + todoId;
            TodoPatch dto = new TodoPatch() { Id = todoId, IsDone = !todoToPatch.IsDone };
            var response = await _http.PatchAsync(url, JsonContent.Create(dto));
            if (!response.IsSuccessStatusCode)
            {
                string msg = "Impossible d'inverser la tâche.";
                msg += response.StatusCode switch {
                    System.Net.HttpStatusCode.NotFound => " Elle n'existe pas !",
                    System.Net.HttpStatusCode.Unauthorized => " Identifiez-vous d'abord.",
                    System.Net.HttpStatusCode.Forbidden => " Pas les habilitations pour le faire...",
                    System.Net.HttpStatusCode.BadRequest => ExtractMessage(response),
                    _ => ""
                };
                throw new CATodoException(msg);
            }
            return await response.Content.ReadFromJsonAsync<Todo>() ?? throw new CATodoException("pas de tâche n°" + todoId);
        }

        public Task<Todo> CreateTodoAsync(TodoCreate todoInfo) {
            throw new NotImplementedException();
        }

        public Task<Todo> UpdateTodoAsync(TodoUpdate todoInfo) {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Category>> ListAllCategoriesAsync() {
            string url = _http.BaseAddress + "category";
            List<Category>? categories = await _http.GetFromJsonAsync<List<Category>>(url);
            if (categories == null) Console.Error.WriteLine("pas de catégorie");
            return categories ?? new List<Category>();
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
