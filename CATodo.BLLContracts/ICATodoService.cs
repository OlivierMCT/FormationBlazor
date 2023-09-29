namespace CATodo.BLLContracts {
    public interface ICATodoService {
        ICollection<Todo> ListAllTodos();
        Todo ListOneTodo(int todoId);
        ICollection<Todo> ListTodosByCategory(int categoryId);
        ICollection<Todo> SearchTodos(string keyword);
        void RemoveTodo(int todoId);
        Todo ToggleTodo(int todoId);
        Todo CreateTodo(TodoCreate todoInfo);
        Todo UpdateTodo(TodoUpdate todoInfo);

        ICollection<Category> ListAllCategories();
        Category ListOneCategory(int categoryId);
        void RemoveCategory(int categoryId);
        Todo CreateCategory(CategoryCreate categoryInfo);
        Todo UpdateCategory(CategoryUpdate categoryInfo);

        // -- Asynchrone -----------------------------------

        Task<ICollection<Todo>> ListAllTodosAsync();
        Task<Todo> ListOneTodoAsync(int todoId);
        Task<ICollection<Todo>> ListTodosByCategoryAsync(int categoryId);
        Task<ICollection<Todo>> SearchTodosAsync(string keyword);
        Task RemoveTodoAsync(int todoId);
        Task<Todo> ToggleTodoAsync(int todoId);
        Task<Todo> CreateTodoAsync(TodoCreate todoInfo);
        Task<Todo> UpdateTodoAsync(TodoUpdate todoInfo);

        Task<ICollection<Category>> ListAllCategoriesAsync();
        Task<Category> ListOneCategoryAsync(int categoryId);
        Task RemoveCategoryAsync(int categoryId);
        Task<Todo> CreateCategoryAsync(CategoryCreate categoryInfo);
        Task<Todo> UpdateCategoryAsync(CategoryUpdate categoryInfo);
    }
}