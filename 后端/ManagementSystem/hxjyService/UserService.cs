using hxjyModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyServices
{
    public class Userservice
    {
        SqlSugarScope db = DBContent.GetDB();

        ///<summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<User> Login(string username)
        {
            // 返回数据库受影响的行数
            return db.Queryable<User>().Where(it => it.username == username).ToList();
        }

        ///<summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddUser(User user)
        {
            // 返回数据库受影响的行数
            return db.Insertable(user).ExecuteCommand();
        }

        ///<summary>
        /// 根据主键删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUser(int id)
        {
            // 返回数据库受影响的行数
            return db.Deleteable<User>().In(id).ExecuteCommand();
        }

        ///<summary>
        /// 查询全部用户
        /// </summary>
        /// <returns>以列表形式返回</returns>
        public List<User> QueryUsers()
        {
            // 返回数据库受影响的行数
            return db.Queryable<User>().ToList();
        }

        ///<summary>
        /// 根据ID查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User QueryUser(int id)
        {
            // 返回数据库受影响的行数
            return db.Queryable<User>().InSingle(id);
        }

        ///<summary>
        /// 更新用户
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int UpdateUser(User user)
        {
            // 返回数据库受影响的行数
            return db.Updateable(user).ExecuteCommand();
        }
    }
}
