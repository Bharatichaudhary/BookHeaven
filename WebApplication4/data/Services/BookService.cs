using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.data.Services
{
    public class BookService : IBookServices
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(BookModel book)
        {
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Book.FirstOrDefaultAsync(n => n.BookId == id);
             _context.Book.Remove(result);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<BookModel>> GetAllAsync()
        {
            var result = await _context.Book.ToListAsync();
            return result;
        }
        public async Task<IEnumerable<BookModel>> GetByGenreAsync(Genre genre)
        {
            var result = await _context.Book.Where(n => n.Genre == genre).ToListAsync(); 
            return result;
        }

        public async Task<BookModel> GetByIdAsync(int id)
        {
            var result = await _context.Book.FirstOrDefaultAsync(n=>n.BookId ==id);
            return result;
        }
      
        public async Task<UserModel> GetByEmailAsync(string email)
        {
            var result = await _context.User.FirstOrDefaultAsync(n => n.Email == email);
            return result;
        }
        public async Task <BookModel> UpdateAsync(int id, BookModel newbook)
        {
            _context.Update(newbook);
            await _context.SaveChangesAsync();
            return newbook;
        }
        public async Task AddUserAsync(UserModel user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

    }
}
