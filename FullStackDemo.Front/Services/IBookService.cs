using FullStackDemo.Front.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStackDemo.Front.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAsync();
        Task<Book> GetAsync(int id);
        Task CreateAsync(Book book);
        Task UpdateAsync(int id, Book book);
        Task RemoveAsync(int id);
    }
}
