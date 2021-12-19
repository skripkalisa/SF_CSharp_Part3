using System;
using System.Collections.Generic;
using System.Linq;
using FirstApp.BLL.Models;
using FirstApp.DAL.Entities;
using AppContext = FirstApp.DAL.AppContext;

namespace FirstApp.BLL.Services
{
    public class BookService
    {
        private readonly AppContext _db;

        public BookService(AppContext context)
        {
            _db = context;
        }

        public IEnumerable<Book> FindAll()
        {
            var books = _db.Books.ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
            return books;
        }
        public bool FindBook(string author, string title)
        {
            var flag = _db.Books.All(book => book.Author.Contains(author) && book.Title == title);
            return flag;
        }

        public BookEntity GetBookEntityId(BookEntity bookEntity)
        {
            var book = _db.Books.FirstOrDefault(book => book.Author == bookEntity.Author && book.Title == bookEntity.Title);
            if (book != null) bookEntity.Id = book.Id;
            return bookEntity;
        }

        public bool BookAvailable(string author, string title)
        {
            var book = _db.Books.FirstOrDefault(book => book.Author.Contains(author) && book.Title == title);
            return book is { Available: true };
        }
        public IEnumerable<Book> BooksByTitle(string title)
        {
            Console.WriteLine("BooksByTitle");
            var books = _db.Books.OrderBy(book=> book.Title == title).ToList();
            return books;
        }   
        
        public List<Book> BooksByYear(int year)
        {
            Console.WriteLine("BooksByYear");
            var books = _db.Books.OrderByDescending(book=> book.Year == year).ToList();
            return books;
        }
                
        public List<Book> BooksByGenreAndYearRange(string genre, int yearFrom, int yearTo)
        {
            Console.WriteLine("BooksByGenreAndYearRange");
            var books = _db.Books.Where(book=> book.Genre.ToString() == genre && book.Year > yearFrom && book.Year < yearTo).ToList();
            return books;
        }
        
        public int BooksByAuthorTotal(string author)
        {
            Console.WriteLine("BooksByAuthorTotal");
            var books = _db.Books.Where(book=> book.Author == author).ToList();
            return books.Count;
        }  
        
        public int BooksByGenreTotal(string genre)
        {
            Console.WriteLine("BooksByGenreTotal");
            var books = _db.Books.Where(book=> book.Genre == genre).ToList();
            return books.Count;
        }

        public int BooksByUserTotal(int userId)
        {
            Console.WriteLine("BooksByUserTotal");
            var books = _db.Books.Where(book=> book.UserId == userId).ToList();
            return books.Count;
        }  
        
        public Book BooksLastBook()
        {
            Console.WriteLine("BooksLastBook");
            var book = _db.Books.ToList().LastOrDefault();
            return book;
        }
    }
}