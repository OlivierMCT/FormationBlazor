using CATodo.BLLContracts;
using Microsoft.AspNetCore.Mvc;

namespace CATodo.WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICATodoService _todoService;

        public TodoController(
            ILogger<CategoryController> logger,
            ICATodoService TodoService
        ) {
            _logger = logger;
            _todoService = TodoService;
        }

        [HttpGet]
        public IEnumerable<Todo> GetAll() {
            return _todoService.ListAllTodos();
        }

        [HttpGet, Route("{id:int}")]
        public ActionResult<Todo> GetById([FromRoute(Name = "id")] int todoId) {
            try {
                return _todoService.ListOneTodo(todoId);
            } catch(CATodoException tex) {
                return NotFound(tex.Message);
            }
        }
    }
}