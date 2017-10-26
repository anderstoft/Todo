using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Model;

namespace TodoClient.Services
{
    public interface IRepository
    {
        Task <ICollection<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItemAsync(int id);
        Task<bool> UpdateTodoItem(TodoItem todoItem);
        Task<bool> AddTodoItem(TodoItem todoItem);
        Task<bool> DeleteTodoItem(int id);
    }
}
