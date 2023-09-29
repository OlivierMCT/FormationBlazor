using CATodo.BLLContracts;
using CATodo.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CATodo.WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICATodoService _todoService;
        private readonly IHubContext<TodoHub> _todoHub;

        public TodoController(
            ILogger<CategoryController> logger,
            ICATodoService TodoService,
            IHubContext<TodoHub> todoHub
        ) {
            _logger = logger;
            _todoService = TodoService;
            _todoHub = todoHub;
        }

        [HttpGet]
        public IEnumerable<Todo> GetAll() {
            return _todoService.ListAllTodos();
        }

        [HttpGet, Route("{id:int}"), ActionName("GetById")]
        public ActionResult<Todo> GetById([FromRoute(Name = "id")] int todoId) {
            try {
                return _todoService.ListOneTodo(todoId);
            } catch (CATodoException tex) {
                return NotFound(tex.Message);
            }
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<ActionResult> DeleteByIdAsync(int id) {
            try {
                _todoService.RemoveTodo(id);
                await _todoHub.Clients.All.SendAsync("PushDeleteTodo", id);
                return NoContent();
            } catch (CATodoException tex) {
                return BadRequest(tex.Message);
            }
        }

        [HttpPatch, Route("{id:int}")]
        public async Task<ActionResult<Todo>> UpdateDoneAsync([FromRoute(Name = "id")] int todoId, [FromBody] TodoPatch infos) {
            try {
                if (todoId != infos.Id) return BadRequest();
                Todo todo = _todoService.ListOneTodo(todoId);
                if (todo.IsDone != infos.IsDone)
                    todo = _todoService.ToggleTodo(infos.Id.Value);
                await _todoHub.Clients.All.SendAsync("PushUpdateTodo", todo);
                return Ok(todo);
            } catch (CATodoException tex) {
                return NotFound(tex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> CreateOneAsync([FromBody] TodoCreate infos) {         
            if(infos.Latitude.HasValue != infos.Longitude.HasValue)
                return BadRequest("Les coordonnées sont invalides");
            Todo newTodo = _todoService.CreateTodo(infos);

            await _todoHub.Clients.All.SendAsync("PushNewTodo", newTodo);
            return CreatedAtAction("GetById", new { id = newTodo.Id }, newTodo);
        }
    }
}