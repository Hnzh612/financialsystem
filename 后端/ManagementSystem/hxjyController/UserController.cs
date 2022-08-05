using hxjyModel;
using hxjyServices;
using Microsoft.AspNetCore.Mvc;


namespace hxjyController
{
    /// <summary>
    /// 用户接口
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        Userservice userservice = new Userservice();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public List<User> Login(string username)
        {
            return userservice.Login(username);
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public User getUserInfoById(int id)
        {
            return userservice.QueryUser(id);
        }
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<User> getAllUser()
        {
            return userservice.QueryUsers();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public int addUser(User user)
        {
            return userservice.AddUser(user);
        }

    }
}
