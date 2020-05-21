using FullStackDemo.Front.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Front.Services
{
    public class BookService : IBookService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public BookService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task CreateAsync(Book book)
        {
            var backEndUrl = string.Format("{0}/{1}", _configuration["BackEndUrl"], "api/books");
            var bookAsJson = JsonConvert.SerializeObject(book);
            var stringContent = new StringContent(bookAsJson, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(backEndUrl, stringContent)
                            .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
        }

        public async Task UpdateAsync(int id, Book book)
        {
            var backEndUrl = string.Format("{0}/{1}/{2}", _configuration["BackEndUrl"], "api/books", id);
            var bookAsJson = JsonConvert.SerializeObject(book);
            var stringContent = new StringContent(bookAsJson, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(backEndUrl, stringContent)
                            .ContinueWith((putTask) => putTask.Result.EnsureSuccessStatusCode());
        }

        public async Task<IEnumerable<Book>> GetAsync()
        {
            var backEndUrl = string.Format("{0}/{1}", _configuration["BackEndUrl"], "api/books");
            var httpResponse = await _httpClient.GetStringAsync(backEndUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(httpResponse);
        }

        public async Task<Book> GetAsync(int id)
        {
            var backEndUrl = string.Format("{0}/{1}/{2}", _configuration["BackEndUrl"], "api/books", id);
            var httpResponse = await _httpClient.GetStringAsync(backEndUrl);
            return JsonConvert.DeserializeObject<Book>(httpResponse);
        }

        public async Task RemoveAsync(int id)
        {
            var backEndUrl = string.Format("{0}/{1}/{2}", _configuration["BackEndUrl"], "api/books", id);
            var httpResponse = await _httpClient.DeleteAsync(backEndUrl)
                                                .ContinueWith((deleteTask) => deleteTask.Result.EnsureSuccessStatusCode());
        }
    }
}
