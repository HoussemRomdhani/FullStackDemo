using FullStackDemo.Front.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace FullStackDemo.Front.Services
{
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
             throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<bool> CreateAsync(Book book)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/books/");
            var bookAsJson = JsonSerializer.Serialize(book);
            request.Content = new StringContent(bookAsJson, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, Book book)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "api/books/" + id);
            var bookAsJson = JsonSerializer.Serialize(book);
            request.Content = new StringContent(bookAsJson, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            return response.IsSuccessStatusCode;

        }

        public async Task<IEnumerable<Book>> GetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/books/");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(responseStream);
                }
            }
            else
                return new List<Book>();
        }

        public async Task<Book> GetAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/books/" + id);

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<Book>(responseStream);
                }
            }
            else
                return null;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/books/" + id);

           var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}
