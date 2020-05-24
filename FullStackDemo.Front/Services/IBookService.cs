using FullStackDemo.Front.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStackDemo.Front.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAsync();
        Task<Book> GetAsync(int id);
        Task<bool> CreateAsync(Book book);
        Task<bool> UpdateAsync(int id, Book book);
        Task<bool> RemoveAsync(int id);
    }
}
