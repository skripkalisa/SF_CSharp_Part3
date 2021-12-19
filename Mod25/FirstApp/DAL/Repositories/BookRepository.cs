using FirstApp.BLL.Models;
using System.Linq;
using FirstApp.DAL.Entities;

namespace FirstApp.DAL.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly AppContext _db;

        public BookRepository(AppContext context)
        {
            _db = context;
        }

        public int AddBook(BookEntity bookEntity)
        {
            var book = new Book
            {
                Title = bookEntity.Title,
                Author = bookEntity.Author,
                Year = bookEntity.Year,
                Genre = bookEntity.Genre
            };
        
            _db.Books.Add(book);
            return _db.SaveChanges();
        }
                
        public Book FindById(int id)
        {
            var book = _db.Books.Where(book=> book.Id == id).ToList();
            return book.FirstOrDefault();
        }
        
        public int UpdateBook(BookEntity bookEntity)
        {

            var book = FindById(bookEntity.Id);
            if (bookEntity.Author != null)
                book.Author = bookEntity.Author;
          
            if (bookEntity.Title != null)
                book.Title = bookEntity.Title;

            if (bookEntity.Year != 0)
                book.Year = bookEntity.Year;

            if (bookEntity.Genre != null)
                book.Genre = bookEntity.Genre;
            
            return _db.SaveChanges();
        }
        

        
        public int DeleteById(int id)
        {
            var book = FindById(id);
            _db.Remove(book);
            return _db.SaveChanges();
        }
    }
            
    public interface IBookRepository
    {
        int AddBook(BookEntity bookEntity);
        Book FindById(int id);
        int UpdateBook(BookEntity bookEntity);
        int DeleteById(int id);
    }
    
}