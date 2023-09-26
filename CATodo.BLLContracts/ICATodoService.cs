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
    }
}