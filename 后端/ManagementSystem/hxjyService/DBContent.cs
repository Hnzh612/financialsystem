using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyServices
{
    static public class DBContent
    {
        ///<summary>
        /// 数据库连接字符
        /// </summary>
        static SqlSugarScope db = new SqlSugarScope(new ConnectionConfig() 
        { 
            ConnectionString = "server=localhost;Database=hxjy;Uid=root;Pwd=123456;Port=3306;Allow User Variables=True;CharSet=UTF8mb4; SslMode=None", 
            DbType = DbType.MySql, IsAutoCloseConnection = true 
        }, db =>
        {
            //(A)全局生效配置点//调试SQL事件，可以删掉
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);//输出sql,查看执行sql
            };
        });
        public static SqlSugarScope GetDB()
        {
            return db;
        }
    }
}
