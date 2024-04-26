using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Database;

namespace WebApplication1.Controllers
{

    [Route("api/BorrowBook")]
    [ApiController]
    public class BorrowBookController : ControllerBase
    {
        private readonly LibraryApiDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public BorrowBookController(LibraryApiDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("api/BorrowBook")]
        public async Task<IActionResult> BorrowBook(int userId, int bookId)
        {


            var book = await _context.Book.FirstOrDefaultAsync(b => b.bookId == bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            if (book.availableCopies <= 0)
            {
                return BadRequest("Book is not available for borrowing because no available copies");
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }


            var borrowBook = new Models.BorrowBook()
            {
                BookID = bookId,
                UserID = userId,
                BorrowDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7),
                Status = "Pending"
            };
            _context.Add(borrowBook);
            _context.SaveChanges();
            return Ok(borrowBook);

        }

        [HttpPost]
        [Route("checkBorroeStatus")]
        public async Task<IActionResult> checkcheckBorroeStatus(int borrowID, int bookId, bool check)
        {

            var borrowBook = await _context.BorrowBook.FirstOrDefaultAsync(b => b.BorrowID == borrowID);
            var book = await _context.Book.FirstOrDefaultAsync(b => b.bookId == bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            else
            {
                if (borrowBook == null)
                {
                    return NotFound("BorrowBook not found");
                }
                else
                {

                    if (check == true)
                    {
                        borrowBook.Status = "Borrowed";
                        book.availableCopies--;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        borrowBook.Status = "rejected";
                        return BadRequest("You Cant Borrow this book");

                    }
                }
            }
            return Ok();

        }

    }
}