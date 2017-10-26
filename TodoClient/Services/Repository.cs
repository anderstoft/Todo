using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Todo.Model;

namespace TodoClient.Services
{
    public class Repository : IRepository
    {
        public async Task<bool> AddTodoItem(TodoItem todoItem)
        {
            var serializedItemToCreate = JsonConvert.SerializeObject(todoItem);

            var client = TodoHttpClient.GetClient();

            var response = await client.PostAsync($"/api/todo/",
                    new StringContent(serializedItemToCreate,
                    Encoding.Unicode,
                    "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTodoItem(int id)
        {
            var client = TodoHttpClient.GetClient();

            var response = await client.DeleteAsync($"/api/todo/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<TodoItem> GetTodoItemAsync(int id)
        {
            var client = TodoHttpClient.GetClient();

            var response = await client.GetAsync($"/api/todo/{id}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var todoItem = JsonConvert.DeserializeObject<TodoItem>(content);
                return todoItem;
            }

            return null;
        }

        public async Task<ICollection<TodoItem>> GetTodoItems()
        {
            var client = TodoHttpClient.GetClient();

            var response = await client.GetAsync($"/api/todo/");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var todoItems = JsonConvert.DeserializeObject<ICollection<TodoItem>>(content);
                return todoItems;
            }

            return null;
        }

        public async Task<bool> UpdateTodoItem(TodoItem todoItem)
        {
            var serializedItemToCreate = JsonConvert.SerializeObject(todoItem);

            var client = TodoHttpClient.GetClient();

            var response = await client.PutAsync($"/api/todo/{todoItem.Id}",
                    new StringContent(serializedItemToCreate,
                    Encoding.Unicode,
                    "application/json"));

            return response.IsSuccessStatusCode;
        }
    }
}
