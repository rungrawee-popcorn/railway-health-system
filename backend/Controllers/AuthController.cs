using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend.Data;
using Dapper;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DbConnectionFactory _db;

        public AuthController(DbConnectionFactory db)
        {
            _db = db;
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using var connection = _db.CreateConnection();

            var sql = @"INSERT INTO users (username, passwordhash)
                        VALUES (@Username, @PasswordHash)
                        RETURNING id;";

            var userId = connection.ExecuteScalar<int>(sql, new
            {
                Username = username,
                PasswordHash = hashedPassword
            });

            return Ok(new
            {
                Id = userId,
                Username = username
            });
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            using var connection = _db.CreateConnection();

            var sql = "SELECT * FROM users WHERE username = @Username";

            var user = connection.QueryFirstOrDefault<User>(sql, new
            {
                Username = username
            });

            if (user == null)
            {
                return Unauthorized("User not found");
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValid)
            {
                return Unauthorized("Invalid password");
            }

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token = token
            });
        }
    
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok("You are authenticated");
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_1234567890")
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }   
}