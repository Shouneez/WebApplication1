using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System;
using WebApplication1.Database;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dtoos;

namespace WebApplication1.Controllers
{
    [Route("/Register")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly LibraryApiDbContext _context;   // to get data from db context

        public RegistersController(LibraryApiDbContext context)
        {
            _context = context;
        }


        [Route("api/GetRegister")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            var data = await _context.User.ToListAsync();
            return Ok(data);
        }

        [Route("api/CreateRegister")]
        [HttpPost]

        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            string phonePattern = @"^\d{11}$";


            if (createUserDto.Username == null)
            {
                return BadRequest("The userName is empty");
            }
            if (createUserDto.Email == null)
            {
                return BadRequest("The Email is empty");
            }
            else if (!Regex.IsMatch(createUserDto.Email, emailPattern))
            {
                return BadRequest("Invalid email format");
            }

            if (createUserDto.PhoneNumber == null)
            {
                return BadRequest("The Phone numer  is empty");
            }
            else if (!Regex.IsMatch(createUserDto.PhoneNumber, phonePattern))
            {
                return BadRequest("Invalid phone number format");
            }
            if (createUserDto.Password == null)
            {
                return BadRequest("The Password is empty");
            }

            var user = new Models.User
            {

                Username = createUserDto.Username,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                PhoneNumber = createUserDto.PhoneNumber,
                UserType = createUserDto.UserType,
            };
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);


        }
    }

}
