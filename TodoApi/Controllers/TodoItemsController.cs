using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoRepository _repository;

        public TodoItemsController(TodoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var item = await _repository.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            var createdItem = await _repository.AddAsync(item);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdItem.Id }, createdItem);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(item);
            return NoContent();
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
