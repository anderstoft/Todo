using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoClient.Services;
using Todo.Model;

namespace TodoClient.Pages
{
    public class AddTodoItemModel : PageModel
    {
        private readonly IRepository repository;

        [BindProperty]
        public TodoItem TodoItem { get; set; } = new TodoItem();

        public AddTodoItemModel(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> OnPostSaveTodo(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool success;

            // Edit flow
            if (TodoItem.Id > 0)
            {
                success = await repository.UpdateTodoItem(todoItem);
            }
            else
            {
                success = await repository.AddTodoItem(todoItem);
            }

            if (success)
            {
                return RedirectToPage("/TodoOverview");
            }
            else
            {
                return Page();
            }
        }
    }
}