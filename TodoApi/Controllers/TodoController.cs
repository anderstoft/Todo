using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Model;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/todo")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            var todoItemWithUser = _context.TodoItems
                .Include(item => item.User).ToList();

            return todoItemWithUser;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoItems
                .Include(i => i.User)
                .FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todoToUpdate = _context.TodoItems
                .Include(i => i.User)
                .FirstOrDefault(t => t.Id == id);
            if (todoToUpdate == null)
            {
                return NotFound();
            }

            todoToUpdate.IsComplete = item.IsComplete;
            todoToUpdate.Name = item.Name;

            if (todoToUpdate.User != null)
            {
                todoToUpdate.User.FirstName = item.User.FirstName;
                todoToUpdate.User.LastName = item.User.LastName;
            }
            else
            {
                todoToUpdate.User = item.User;
            }

            if (await TryUpdateModelAsync<TodoItem>(todoToUpdate,
            "",
            c => c.Name, c => c.IsComplete, c => c.User))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {

                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
