using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebDead.Model;

namespace WebDead.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        readonly BD.BD dataBase;

        public JwtController(BD.BD dataBase)
        {
            this.dataBase = dataBase;
        }

        [HttpGet("login")]
        public ActionResult<TokenRole> Login(string login, string password)
        {
            // Ищем пользователя и вытягиваем всё, что необходимо положить в полезную нагрузку для токена
            User? user = dataBase.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user is null)
                return Unauthorized();

            string role = user.Admin ? "admin" : "user";
            int id = user.Id;

            // Создаём полезную нагрузку для токена
            var claims = new List<Claim> {
                //Кладём Id (если нужно)
                new Claim(ClaimValueTypes.Integer32, id.ToString()),
                //Кладём роль
                new Claim(ClaimTypes.Role, role)
            };

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    //кладём полезную нагрузку
                    claims: claims,
                    //устанавливаем время жизни токена 2 минуты
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new TokenRole
            {
                Token = token,
                Role = role
            });
        }
        public class AuthOptions
        {
            public const string ISSUER = "MyAuthServer"; // издатель токена
            public const string AUDIENCE = "MyAuthClient"; // потребитель токена
            const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
