using WebApplication4.Models;

namespace WebApplication4.data.Services
{
    public interface IBookServices
    {
        Task<IEnumerable<BookModel>> GetAllAsync();
       Task< BookModel> GetByIdAsync(int id);
        Task<IEnumerable<BookModel>> GetByGenreAsync(Genre genre);

        Task AddAsync(BookModel book);
        Task <BookModel> UpdateAsync(int id,BookModel newbook);
        Task DeleteAsync(int id);
        Task AddUserAsync(UserModel user);
        Task<UserModel> GetByEmailAsync(string email);
    }
}
