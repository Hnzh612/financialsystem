using hxjyModel.dbModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyServices
{
    public class Adminservice
    {
        SqlSugarScope db = DBContent.GetDB();

        ///<summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Admin> Login(string username)
        {
            // 返回数据库受影响的行数
            return db.Queryable<Admin>().Where(it => it.username == username).ToList();
        }

        ///<summary>
        /// 添加用户
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int AddAdmin(Admin admin)
        {
            // 返回数据库受影响的行数
            return db.Insertable(admin).ExecuteCommand();
        }

        ///<summary>
        /// 根据主键删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteAdmin(int id)
        {
            // 返回数据库受影响的行数
            return db.Deleteable<Admin>().In(id).ExecuteCommand();
        }

        ///<summary>
        /// 查询全部用户
        /// </summary>
        /// <returns>以列表形式返回</returns>
        public List<Admin> QueryAdmins()
        {
            // 返回数据库受影响的行数
            return db.Queryable<Admin>().ToList();
        }

        ///<summary>
        /// 根据ID查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Admin QueryAdmin(int id)
        {
            // 返回数据库受影响的行数
            return db.Queryable<Admin>().InSingle(id);
        }

        ///<summary>
        /// 更新用户
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int UpdateAdmin(Admin admin)
        {
            // 返回数据库受影响的行数
            return db.Updateable(admin).ExecuteCommand();
        }
    }
}
