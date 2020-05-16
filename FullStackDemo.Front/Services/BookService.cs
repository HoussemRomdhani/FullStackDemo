using FullStackDemo.Front.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
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
        public async Task<IEnumerable<Book>> Get()
        {
            var backEndUrl = string.Format("{0}/{1}", _configuration["BackEndUrl"], "api/books");
            var httpResponse = await _httpClient.GetStringAsync(backEndUrl);
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(httpResponse);
        }
    }
}
