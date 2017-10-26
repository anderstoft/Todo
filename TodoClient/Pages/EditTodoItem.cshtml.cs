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
    public class EditTodoItemModel : PageModel
    {
        private readonly IRepository repository;

        [BindProperty]
        public TodoItem TodoItem { get; set; }

        public EditTodoItemModel(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task OnGetAsync(int id)
        {
            if (id == 0)
                return;

            var result = await repository.GetTodoItemAsync(id);

            TodoItem = result;
        }

        public async Task<IActionResult> OnPostSaveTodo(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool success = false;

            // Edit flow
            if (TodoItem.Id > 0)
            {
                success = await repository.UpdateTodoItem(todoItem);
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