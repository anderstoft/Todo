using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TodoClient.Services
{
    public static class TodoHttpClient
    {
        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://todoapi20171026020512.azurewebsites.net");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
