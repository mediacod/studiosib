namespace MediaStudio.Controllers
{
    using DBContext.Models;
    using MediaStudio.Service.Models.Input;
    using MediaStudio.Service.Services.Audit;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получает пользователя по текущему JWT.
        /// </summary>
        /// <returns>Пользователь.</returns>
        [HttpGet]
        public User Get()
        {
           return _userService.GetUser(User.Identity.Name);
        }

        /// <summary>
        /// Обновляет пользователя.
        /// </summary>
        /// <returns>Обновленный пользователь.</returns>
        [HttpPut]
        public User Put()
        {
            return _userService.GetUser(User.Identity.Name);
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="user">Новый пользователь.</param>
        /// <returns>Идентификатор пользователя</returns>
        [HttpPost]
        public int Post(InputUser user)
        {
            return _userService.PostUser(user);
        }
    }
}
