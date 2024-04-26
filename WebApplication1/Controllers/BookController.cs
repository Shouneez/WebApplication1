using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Database;
using WebApplication1.Dtoos;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryApiDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public BookController(LibraryApiDbContext context)
        {
            _context = context;
        }

        [Route("api/AddBook")]
        [HttpPost]
        public async Task<IActionResult> AddBook(CreateBookDtoo createBookDtoo)
        {

            if (string.IsNullOrWhiteSpace(createBookDtoo.Title))
            {
                return BadRequest("Title cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(createBookDtoo.Author))
            {
                return BadRequest("Author cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(createBookDtoo.ISBN))
            {
                return BadRequest("ISBN cannot be empty");
            }

            if (createBookDtoo.RackNumber <= 0)
            {
                return BadRequest("RackNumber cannot be empty");
            }

            if (createBookDtoo.AvailableCopies <= 0)
            {
                return BadRequest("AvailableCopies must be greater than 0");
            }

            if (createBookDtoo.TotalCopies <= 0)
            {
                return BadRequest("TotalCopies must be greater than 0");
            }

            var book = new Models.Book()
            {
                title = createBookDtoo.Title,
                author = createBookDtoo.Author,
                isbn = createBookDtoo.ISBN,
                rackNumber = createBookDtoo.RackNumber,
                availableCopies = createBookDtoo.AvailableCopies,
                totalCopies = createBookDtoo.TotalCopies
            };
            _context.Add(book);
            await _context.SaveChangesAsync();


            return Ok(book);
        }

        [Route("api/DeleteBook")]
        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {

            var book = await _context.Book.FirstOrDefaultAsync(b => b.bookId == id);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return Ok(book);


        }
        [Route("api/UpdateBook")]
        [HttpPost]
        public async Task<IActionResult> UpdateBook(int id, Models.Book bookDetails)
        {

            if (string.IsNullOrWhiteSpace(bookDetails.title))
            {
                return BadRequest("Title cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(bookDetails.author))
            {
                return BadRequest("Author cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(bookDetails.isbn))
            {
                return BadRequest("ISBN cannot be empty");
            }

            if (bookDetails.rackNumber<= 0)
            {
                return BadRequest("RackNumber cannot be empty");
            }

            if (bookDetails.availableCopies <= 0)
            {
                return BadRequest("AvailableCopies must be greater than 0");
            }

            if (bookDetails.totalCopies <= 0)
            {
                return BadRequest("TotalCopies must be greater than 0");
            }
            var book = await _context.Book.FirstOrDefaultAsync(b => b.bookId == id);
            if (book == null)
            {

                return NotFound("Book not found");
            }
            book.title = bookDetails.title;
            book.author = bookDetails.author;
            book.isbn = bookDetails.isbn;
            book.rackNumber = bookDetails.rackNumber;
            book.availableCopies = bookDetails.availableCopies;
            book.totalCopies = bookDetails.totalCopies;

            await _context.SaveChangesAsync();
            return Ok(book);
        }
        [Route("api/ViewBook")]
        [HttpGet]
        public async Task<IActionResult> ViewBook()
        {

            var book = await _context.Book.ToListAsync();
            return Ok(book);
        }
        [Route("api/Repor")]
        [HttpGet]
        public async Task<IActionResult> Report()
        {
            var book = await _context.Book.ToListAsync();
            var borrowbook = await _context.BorrowBook.ToListAsync();
            var user = await _context.User.ToListAsync();

            var bookCount = book.Count;
            var borrowbookCount = borrowbook.Count;
            var userCount = user.Count;

            var methods = new
            {
                // bookData = book,
                // borrowbookData = borrowbook,
                // userData = user,
                bookCount,
                borrowbookCount,
                userCount
            };

            return Ok(methods);
        }


    }
}
