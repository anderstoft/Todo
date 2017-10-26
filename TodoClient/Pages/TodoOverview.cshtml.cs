using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoClient.Services;
using Todo.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TodoClient.Pages
{
    public class TodoOverviewModel : PageModel
    {
        private readonly IRepository repository;

        public ICollection<TodoItem> TodoItems { get; set; } 

        public TodoOverviewModel(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task OnGetAsync()
        {
            TodoItems = await repository.GetTodoItems();
        }

        public async Task<IActionResult> OnPostRemoveTodoItem(int id)
        {
            bool success = false;

            if (id > 0)
            {
                success = await repository.DeleteTodoItem(id);
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