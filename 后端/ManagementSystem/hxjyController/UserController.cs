using hxjyModel;
using hxjyModel.dbModels;
using hxjyModel.apiModels;
using hxjyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CallBackModels Login(string username, string password)
        {
            CallBackModels callback = new CallBackModels();
            List<Admin> admin = adminservice.Login(username);
            string pwd = admin[0].password;
            if (pwd == password)
            {
                // 1. 定义需要使用到的Claims
                var claims = new[]
                {
                new Claim("Id", $"{admin[0].id}"),
                new Claim("Name", $"{admin[0].username}")
                };

                // 2. 从 appsettings.json 中读取SecretKey
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

                // 3. 选择加密算法
                var algorithm = SecurityAlgorithms.HmacSha256;

                // 4. 生成Credentials
                var signingCredentials = new SigningCredentials(secretKey, algorithm);

                // 5. 从 appsettings.json 中读取Expires
                var expires = Convert.ToDouble(_configuration["JWT:Expires"]);

                // 6. 根据以上，生成token
                var token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],     //Issuer
                    _configuration["JWT:Audience"],   //Audience
                    claims,                          //Claims,
                    DateTime.Now,                    //notBefore
                    DateTime.Now.AddDays(expires),   //expires
                    signingCredentials               //Credentials
                );

                // 7. 将token变为string
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                callback.successed = true;
                callback.data = admin;
                callback.message = jwtToken;
            }
            else
            {
                callback.successed = false;
                callback.message = "不存在该用户";
            }
            return callback;
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
