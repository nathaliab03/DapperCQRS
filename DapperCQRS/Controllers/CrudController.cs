using DapperCQRS.Commands;
using DapperCQRS.Commands.Handlers;
using DapperCQRS.Models;
using DapperCQRS.Queries;
using DapperCQRS.Queries.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace DapperCQRS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudController
    {
        private readonly GetAllCategoriesHandler _getAllCategoriesHandler;
        private readonly GetCategoryByIdHandler _getCategoryByIdHandler;
        private readonly CreateCategoryHandler _createCategoryHandler;
        private readonly DeleteCategoryHandler _deleteCategoryHandler;
        private readonly UpdateCategoryHandler _updateCategoryHandler;

        public CrudController(GetAllCategoriesHandler getAllCategoriesHandler, GetCategoryByIdHandler getCategoryByIdHandler, CreateCategoryHandler createCategoryHandler, DeleteCategoryHandler deleteCategoryHandler, UpdateCategoryHandler updateCategoryHandler)
        {
            _getAllCategoriesHandler = getAllCategoriesHandler;
            _getCategoryByIdHandler = getCategoryByIdHandler;
            _createCategoryHandler = createCategoryHandler;
            _deleteCategoryHandler = deleteCategoryHandler;
            _updateCategoryHandler = updateCategoryHandler;
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _getAllCategoriesHandler.HandleAsync(new GetAllCategoriesQuery());
        }

        [HttpGet("category/{id}")]
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _getCategoryByIdHandler.HandleAsync(new GetCategoryByIdQuery() { Id = id });
        }

        [HttpPost("category")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand command)
        {
            await _createCategoryHandler.HandleAsync(command);
            return Ok();
        }

        [HttpPut("category")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryCommand command)
        {
            await _updateCategoryHandler.HandleAsync(command);
            return Ok();
        }

        [HttpDelete("category/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            await _deleteCategoryHandler.HandleAsync(new DeleteCategoryCommand { Id = id });
            return Ok();
        }
    }
}
