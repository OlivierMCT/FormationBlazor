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
            } catch (CATodoException tex) {
                return NotFound(tex.Message);
            }
        }

        [HttpDelete, Route("{id:int}")]
        public ActionResult DeleteById(int id) {
            try {
                _todoService.RemoveTodo(id);
                return NoContent();
            } catch (CATodoException tex) {
                return BadRequest(tex.Message);
            }
        }

        [HttpPatch, Route("{id:int}")]
        public ActionResult<Todo> UpdateDone([FromRoute(Name = "id")] int todoId, [FromBody] TodoPatch infos) {
            try {
                if (todoId != infos.Id) return BadRequest();
                Todo todo = _todoService.ListOneTodo(todoId);
                if (todo.IsDone != infos.IsDone)
                    todo = _todoService.ToggleTodo(infos.Id.Value);
                return Ok(todo);
            } catch (CATodoException tex) {
                return NotFound(tex.Message);
            }
        }
    }
}