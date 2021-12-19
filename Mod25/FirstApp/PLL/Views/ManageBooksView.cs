using System;
using System.Collections.Generic;
using FirstApp.BLL.Models;
using FirstApp.BLL.Services;
using FirstApp.DAL.Entities;
using FirstApp.DAL.Repositories;
using AppContext = FirstApp.DAL.AppContext;

namespace FirstApp.PLL.Views
{
    public class ManageBooksView
    {
        private readonly BookRepository _bookRepository;
        private readonly BookService _bookService;
        public ManageBooksView(AppContext context)
        {
            _bookRepository = new(context);
            _bookService = new(context);
        }
        public void ManageBooks()
        {
            while (true)
            {
                Console.WriteLine("1 - Add a new Book");
                Console.WriteLine("2 - Update a Book");
                Console.WriteLine("3 - Delete a Book");
                Console.WriteLine("4 - Check Book existence");
                Console.WriteLine("5 - Check Book availability");
                Console.WriteLine("6 - Get books stats");
                Console.WriteLine("0 - Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        var bookEntity = CreateBookEntity();
                        _bookRepository.AddBook(bookEntity);
                        break;

                    case "2":
                        GetBooks(_bookService);
                        Console.WriteLine("Enter book's ID");
                        var id = GetInteger();
                        bookEntity = CreateBookEntity();
                        if (id != 0) bookEntity.Id = id;
                        _bookRepository.UpdateBook(bookEntity);
                        break;

                    case "3":
                        Console.WriteLine("Find book's Id in the list");
                        GetBooks(_bookService);
                        Console.WriteLine("Enter book's ID");
                        id = GetInteger();
                        if (id != 0) _bookRepository.DeleteById(id);
                        break;
                
                    case "4":
                        Console.Write("Enter author: ");
                        string author = Console.ReadLine();
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();
                        string flag = "is not";
                        var bookExists = _bookService.FindBook(author, title);
                        if (bookExists) flag = "is";
                        Console.WriteLine($"Book {flag} registered in our library");
                        break;            
                
                    case "5":
                        Console.Write("Enter author: ");
                        string bookAuthor = Console.ReadLine();
                        Console.Write("Enter title: ");
                        string bookTitle = Console.ReadLine();
                        string available = "is not";
                        var bookAvailable = _bookService.BookAvailable(bookAuthor, bookTitle);
                        if (bookAvailable) available = "is";
                        Console.WriteLine($"Book {available} available");
                        break;
                                
                    case "6":
                        GetBookStats();
                        break;
                
                    case "0":
                        return;
                }
            }
        }

        private void GetBookStats()
        {
            while (true)
            {
                Console.WriteLine("1 - Get all books");
                Console.WriteLine("2 - Get all books by title");
                Console.WriteLine("3 - Get all books by year");
                Console.WriteLine("4 - Get all books by genre and year range");
                Console.WriteLine("5 - Total number of books by author");
                Console.WriteLine("6 - Total number of  books by genre");
                Console.WriteLine("7 - Last added book");
                Console.WriteLine("0 - Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        GetBooks(_bookService);
                        break;

                    case "2":
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();
                        var booksByTitle =  _bookService.BooksByTitle(title);
                        PrintBooks(booksByTitle);
                        break;

                    case "3":
                        Console.Write("Enter year: ");
                        var year = GetInteger();
                        var booksByYear =  _bookService.BooksByYear(year);
                        PrintBooks(booksByYear);
                        break;
                
                    case "4":
                        Console.Write("Enter genre: ");
                        string genre = Console.ReadLine();
                        Console.Write("Enter minimum year: ");
                        int yearFrom = GetInteger();
                        Console.Write("Enter maximum year: ");
                        int yearTo = GetInteger();
                        var booksByGenreAndYearRange = _bookService.BooksByGenreAndYearRange(genre, yearFrom, yearTo);
                        PrintBooks(booksByGenreAndYearRange);
                        break;
                
                    case "5":
                        Console.Write("Enter author: ");
                        string author = Console.ReadLine();
                        var allBooksByAuthor = _bookService.BooksByAuthorTotal(author);
                        Console.WriteLine($"Total number of books by {author}: {allBooksByAuthor}");
                        break;
                    case "6":
                        Console.Write("Enter genre: ");
                        genre = Console.ReadLine();
                        var allBooksByGenre = _bookService.BooksByGenreTotal(genre);
                        Console.WriteLine($"Total number of books by {genre}: {allBooksByGenre}");
                        break;
                    case "7":
                        Console.WriteLine("Last added book: ");
                        var last = _bookService.BooksLastBook();
                        PrintBooks(new List<Book>
                        {
                            last
                        });
                        break;
                    case "0":
                        return;               
                }
            }
        }

        private static void PrintBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Author: {book.Author}, Title: {book.Title}");
            }
        }
        private int GetInteger()
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (NotFiniteNumberException e)
            {
                Console.WriteLine($"Cannot convert your input to Number: {e.Message}");
            }

            return id;
        }

        private static void GetBooks(BookService bookService)
        {
            var books = bookService.FindAll();
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Author: {book.Author}, Title: {book.Title}");
            }
        }

        private BookEntity CreateBookEntity()
        {
            int year = 0;
            Console.Write("Enter book's Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter book's Author: ");
            string author = Console.ReadLine();
            Console.Write("Enter book's Year: ");
            try
            {
                year = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Not a number! { e.Message }");
            }
            Console.Write("Select book's Genre: ");
            string genre = Console.ReadLine();

            return new BookEntity
            {
                Title = title,
                Author = author,
                Year = year,
                Genre = genre
            };
        }
    }
}