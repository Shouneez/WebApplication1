using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Database;
using WebApplication1.Dtoos;

namespace WebApplication1.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class LoginController : ControllerBase
        {
            private readonly LibraryApiDbContext _context;
            private readonly IHttpContextAccessor _contextAccessor;
            public LoginController(LibraryApiDbContext context)
            {
                _context = context;
            }


            [HttpPost]
            public async Task<IActionResult> Login(Login loginRequest)
            {
                try
                {

                    var user = await _context.User.FirstOrDefaultAsync(u =>
                        u.Email == loginRequest.Email && u.Password == loginRequest.Password);

                    if (user != null)
                    {

                        // return Ok("Login successful");
                        return Ok(user);
                    }
                    else
                    {
                        // Authentication failed
                        return Unauthorized("Invalid Email or password");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during the login process
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during login");
                }
            }
        }

    }

