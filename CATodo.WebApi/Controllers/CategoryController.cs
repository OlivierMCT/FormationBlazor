using CATodo.BLLContracts;
using Microsoft.AspNetCore.Mvc;

namespace CATodo.WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICATodoService _todoService;

        public CategoryController(
            ILogger<CategoryController> logger,
            ICATodoService TodoService
        ) {
            _logger = logger;
            _todoService = TodoService;
        }

        [HttpGet]
        public IEnumerable<Category> GetAll() {
            return _todoService.ListAllCategories();
        }

        [HttpGet, Route("{id:int}")]
        public ActionResult<Category> GetById([FromRoute(Name = "id")] int categoryId) {
            try {
                return _todoService.ListOneCategory(categoryId);
            } catch(CATodoException tex) {
                return NotFound(tex.Message);
            }
        }
    }
}