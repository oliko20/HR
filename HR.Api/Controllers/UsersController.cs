using System.Threading.Tasks;
using HR.Api.Contracts;
using HR.Api.Db;
using Microsoft.AspNetCore.Mvc;

namespace HR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDao _userDao;

        public UsersController(UserDao userDao)
        {
            _userDao = userDao;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserDto model)
        {
            var userByPersonalId = await _userDao.GetByPersonalIdAsync(model.PersonalId);
            if (userByPersonalId != null)
            {
                return Conflict($"User with personalId {model.PersonalId} already exists");
            }

            var userByEmail = await _userDao.GetByEmailAsync(model.Email);
            if (userByEmail != null)
            {
                return Conflict($"User with Email {model.Email} already exists");
            }

            return Ok(await _userDao.CreateAsync(model.GetUser(Cripto.Encript(model.Password))));
        }

        [HttpPost ("Authenticate")]
        public async Task<bool> Authenticate([FromBody] AuthenticateDto itemDto)
        {
            var user = await _userDao.GetByEmailAsync(itemDto.Email);
            if (user is null)
                return false;
            return user.Password == Cripto.Encript(itemDto.Password);
        }
    }
}