using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Model;

namespace TodoClient.Views.Shared.Components.EditTodoItem
{
    public class EditTodoItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TodoItem todoItem)
        {
            return View(todoItem);
        }
    }
}
