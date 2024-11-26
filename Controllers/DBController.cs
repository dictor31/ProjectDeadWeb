using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using WebDead.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static WebDead.Model.TokenRole;


namespace WebDead.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBController : ControllerBase
    {
        readonly BD.BD dataBase;

        public DBController(BD.BD dataBase)
        {
            this.dataBase = dataBase;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetUsers")]
        public async Task<ActionResult<ObservableCollection<User>>> Get()
        {
            ObservableCollection<User> users = new(dataBase.Users);
            return Ok(users);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("CreateUser")]
        public async Task<ActionResult> Post(User user)
        {
            User find = await dataBase.Users.FirstOrDefaultAsync(s => s.Login == user.Login);
            if (find == null)
            {
                dataBase.Users.Add(user);
                await dataBase.SaveChangesAsync();
                return Ok("Пользователь создан");
            }
            return BadRequest("Пользователь существует");
        }
        [HttpPut("PutUser")]
        public async Task<ActionResult> Put(User user)
        {
            User find = await dataBase.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            find.Login = user.Login;
            find.Password = user.Password;
            find.Admin = user.Admin;
            find.Ban = user.Ban;
            find.LastLogin = user.LastLogin;

            await dataBase.SaveChangesAsync();
            return Ok(find);
        }
        [HttpGet("SearchUser")]
        public async Task<ActionResult> Search(string login)
        {
            User find = await dataBase.Users.FirstOrDefaultAsync(s => s.Login == login);
            if (find == null)
            {
                return NotFound("Пользователь не найден");
            }
            return Ok(find);
        }
    }
    
}

