using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Payments.Dtos;
using WebAPI_Payments.Interfaces;
using WebAPI_Payments.Models;

namespace WebAPI_Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork uow, IMapper mapper, IConfiguration configuration)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await uow.UserRepository.FindUser(id);
               
                UserDto returnUser = new UserDto();
                mapper.Map(user, returnUser);

                return Ok(returnUser);
            }
            catch
            {
                return BadRequest("User with this id does not exist");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.Email, loginReq.Password);
            if (user == null)
                return BadRequest("User with this datas doesn't exist");

            var loginRes = new LoginResDto();
            loginRes.Id = user.Id;
            loginRes.Email = loginReq.Email;
            loginRes.Token = CreateJWT(user);

            return Ok(loginRes);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto newUser)
        {
            if (await uow.UserRepository.UserAlreadyExists(newUser.Email))
                return BadRequest("User already exists, please try something else.");
            if (CheckFieldsAreEmpty(newUser))
                return BadRequest("All fields must be filled");

            uow.UserRepository.Register(newUser.Name, newUser.Lastname, newUser.PhoneNumber, newUser.Email, newUser.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto updatedUser)
        {
            try
            {
                var userFromDb = await uow.UserRepository.FindUser(id);

                mapper.Map(updatedUser, userFromDb);

                await uow.SaveAsync();
                return StatusCode(200);
            }
            catch
            {
                return BadRequest("User with this id does not exist");
            }
        }

        [HttpPut("verify/{id}")]
        public async Task<IActionResult> Verify(int id)
        {
            try
            {
                var userFromDb = await uow.UserRepository.FindUser(id);
                userFromDb.IsVerified = true;

                uow.TransactionsRepository.AddTransaction(id, 1, DateTime.Now.Date, TransactionType.WITHDRAWAL);

                await uow.SaveAsync();
                return StatusCode(200);
            }
            catch
            {
                return BadRequest("User with this id does not exist");
            }
        }

        [HttpPut("updateBudget/{id}")]
        public async Task<IActionResult> UpdateBudget(int id, double amount)
        {
            try
            {
                var userFromDb = await uow.UserRepository.FindUser(id);
                userFromDb.Budget += amount;

                uow.TransactionsRepository.AddTransaction(id, amount, DateTime.Now.Date, TransactionType.DEPOSIT);

                await uow.SaveAsync();
                return StatusCode(200);
            }
            catch
            {
                return BadRequest("User with this id does not exist");
            }
        }

        [HttpPut("onlineTransfer")]
        public async Task<IActionResult> TransferToOnlineAccount(string email, double amount, int id)
        {
            try
            {
                var sender = await uow.UserRepository.FindUser(id);
                var userFromDb = await uow.UserRepository.FindUserByEmail(email);
                if (sender.Budget < amount)
                    return BadRequest("You don't have enough money to execute this transaction");

                sender.Budget -= amount;
                uow.TransactionsRepository.AddTransaction(id, amount, DateTime.Now.Date, TransactionType.WITHDRAWAL);
                
                userFromDb.Budget += amount;
                uow.TransactionsRepository.AddTransaction(userFromDb.Id, amount, DateTime.Now.Date, TransactionType.DEPOSIT);
            }
            catch
            {
                return BadRequest("User does not exist");
            }

            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpPut("bankTransfer")]
        public async Task<IActionResult> TransferToBankAccount(double amount, int id)
        {
            try
            {
                var sender = await uow.UserRepository.FindUser(id);
                if (sender.Budget < amount)
                    return BadRequest("You don't have enough money to execute this transaction");

                sender.Budget -= amount;

                uow.TransactionsRepository.AddTransaction(id, amount, DateTime.Now, TransactionType.WITHDRAWAL);

                await uow.SaveAsync();
                return StatusCode(200);
            }
            catch
            {
                return BadRequest("User with this id does not exist");
            }
        }

        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        private bool CheckFieldsAreEmpty(RegisterDto user)
        {
            return user.Name.Length == 0 ||
                   user.Lastname.Length == 0 ||
                   user.Email.Length == 0 ||
                   user.PhoneNumber.ToString().Length == 0 ||
                   user.Password.Length == 0;
        }
    }
}
