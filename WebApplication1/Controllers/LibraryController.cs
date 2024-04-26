using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Library")]
    public class LibraryController : Controller
    {
        private readonly LibraryApiDbContext dbContext;
        public LibraryController(LibraryApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult>  GetAllBooks()
        {
            return Ok( await dbContext.Book.ToListAsync());

        }
        [HttpGet]
        [Route("{bookId:int}")]

        public async Task<IActionResult> GetBook([FromRoute] int bookId)
          {
            var book = await dbContext.Book.FindAsync(bookId);
            if (book == null)
              {
                  return NotFound();
              }
            return Ok(book);
          }

        [HttpPost]
        public async  Task <IActionResult> AddBook(Book newBook)
        {
            var book = new Book()
            {

                bookId = newBook.bookId,
                title = newBook.title,
                author = newBook.author,
                isbn = newBook.isbn,
                imageLink = newBook.imageLink,
                rackNumber = newBook.rackNumber,
                availableCopies = newBook.availableCopies,
                totalCopies = newBook.totalCopies
            };
            await dbContext.Book.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return Ok(book);
        }
        [HttpPut]
        [Route("{bookId:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int bookId, Book updateBook)
        {
            var book = await dbContext.Book.FindAsync(bookId);
            if (book != null)
            {
                book.bookId = updateBook.bookId;
                book.title = updateBook.title;
                book.author = updateBook.author;
                book.isbn = updateBook.isbn;
                book.imageLink = updateBook.imageLink;
                book.rackNumber = updateBook.rackNumber;
                book.availableCopies = updateBook.availableCopies;
                book.totalCopies = updateBook.totalCopies;
                await  dbContext.SaveChangesAsync();
                return Ok(book);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{bookId:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int bookId)
        {
            var book = await dbContext.Book.FindAsync(bookId);
            if (book != null)
            {
                dbContext.Remove(book);
               await dbContext.SaveChangesAsync();
                return Ok(book);
            }
            return NotFound();
        }
    }
}
