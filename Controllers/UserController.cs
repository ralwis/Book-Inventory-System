using Book_Inventory_System.Dtos;
using Book_Inventory_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Book_Inventory_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DataContext dc;
        private readonly IConfiguration configuration;

        public UserController(DataContext dc, IConfiguration configuration)
        {
            this.dc = dc;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await Authenticate(loginReq.Email, loginReq.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var loginRes = new LoginResDto();
            loginRes.UserId = user.Id;
            loginRes.Email = user.Email;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterReqDto regReq)
        {
            if(await dc.Users.AnyAsync(x => x.Email == regReq.Email))
            {
                return BadRequest("User already exist");
            }

            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(regReq.Password));
            }

            User user = new User();
            user.Name = regReq.Name;
            user.Email = regReq.Email;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;

            dc.Users.Add(user);

            await dc.SaveChangesAsync();
            return StatusCode(201);
        }

        private async Task<User> Authenticate(string email, string passwordText)
        {
            var user = await dc.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || user.PasswordKey == null)
            {
                return null;
            }

            if(!MatchPasswordHash(passwordText, user.Password, user.PasswordKey))
            {
                return null;
            }

            return user;

        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                    {
                        return false;
                    }
                }

                return true;
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

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
