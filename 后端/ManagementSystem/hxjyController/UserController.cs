using hxjyModel;
using hxjyModel.dbModels;
using hxjyServices;
using Microsoft.AspNetCore.Mvc;


namespace hxjyController
{
    /// <summary>
    /// 用户接口
    /// </summary>
    /// <returns></returns>
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        Adminservice adminservice = new Adminservice();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Admin> Login(string username)
        {
            return adminservice.Login(username);
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Admin getAdminInfoById(int id)
        {
            return adminservice.QueryAdmin(id);
        }
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Admin> getAllAdmin()
        {
            return adminservice.QueryAdmins();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        [HttpPost]
        public int addAdmin(Admin admin)
        {
            return adminservice.AddAdmin(admin);
        }

    }
}
