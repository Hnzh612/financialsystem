using hxjyModel.dbModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyServices
{
    public class ParamService
    {
        SqlSugarScope db = DBContent.GetDB();

        #region 销售参数操作

        ///<summary>
        /// 添加销售参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int AddParam(Params param)
        {
            // 返回数据库受影响的行数
            return db.Insertable(param).ExecuteCommand();
        }

        ///<summary>
        /// 根据主键删除品种
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int DeleteParam(int id)
        {
            // 返回数据库受影响的行数
            return db.Deleteable<Params>().In(id).ExecuteCommand();
        }

        ///<summary>
        /// 查询全部品种
        /// </summary>
        /// <returns>以列表形式返回</returns>
        public List<Params> QueryParams()
        {
            // 返回数据库受影响的行数
            return db.Queryable<Params>().ToList();
        }

        #endregion

    }
}
