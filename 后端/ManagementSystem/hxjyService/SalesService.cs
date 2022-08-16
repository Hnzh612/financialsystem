using hxjyModel.dbModels;
using hxjyModel.apiModels;
using SqlSugar;

namespace hxjyServices
{
    public class SalesService
    {
        SqlSugarScope db = DBContent.GetDB();

        #region 销售台账相关操作

        ///<summary>
        /// 查询全部订单
        /// </summary>
        /// <returns>以列表形式返回</returns>
        public CallBackModels QuerySalesConnect(int page, string sname, string scompany, string rname, string rcompany,int rid)
        {
            CallBackModels callbackmodels = new CallBackModels();
            string querysql = $"select * from sales_connect where 1=1 ";
            if (rid > 0)
            {
                querysql += $" and rid='{rid}'";
            }
            if (!string.IsNullOrEmpty(sname)) {
                querysql += $" and sname='{sname}'";
            }
            if (!string.IsNullOrEmpty(scompany))
            {
                querysql += $" and scompany='{scompany}'";
            }
            if (!string.IsNullOrEmpty(rname))
            {
                querysql += $" and rname='{rname}'";
            }
            if (!string.IsNullOrEmpty(rcompany))
            {
                querysql += $" and rcompany='{rcompany}'";
            }
            querysql += $" limit { (page - 1) * 10},10";
            int total = db.Ado.GetInt( "select count(1) from sales_connect");
            // 返回数据库受影响的行数
            List<SalesConnect> salesConnect =  db.Ado.SqlQuery<SalesConnect>(querysql);
            callbackmodels.data = salesConnect;
            callbackmodels.message = total.ToString();
            return callbackmodels;
        }

        ///<summary>
        /// 添加订单
        /// </summary>
        /// <param name="salesconnect"></param>
        /// <returns></returns>
        public int AddSalesConnect(SalesConnect salesconnect)
        {
            string querysql = $" select id from customer where company='{salesconnect.rcompany}'";
            int c = db.Ado.ExecuteCommand(querysql);
            if (c < 0)
            {
                string insertsql = $" insert into customer (cname,phone,company,fax) values('{salesconnect.rname}','{salesconnect.rphone}','{salesconnect.rcompany}','{salesconnect.rfax}')";
                db.Ado.ExecuteCommand(insertsql);
                salesconnect.rid = db.Ado.GetInt(querysql);
            }
            // 返回数据库受影响的行数
            return db.Insertable(salesconnect).ExecuteCommand();
        }

        ///<summary>
        /// 根据主键删除订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteSalesConnect(int id)
        {
            // 返回数据库受影响的行数
            return db.Deleteable<SalesConnect>().In(id).ExecuteCommand();
        }


        ///<summary>
        /// 更新订单
        /// </summary>
        /// <param name="salesconnect"></param>
        /// <returns></returns>
        public int UpdateConnect(SalesConnect salesconnect)
        {

            // 返回数据库受影响的行数
            return db.Updateable(salesconnect).ExecuteCommand();
        }

        ///<summary>
        /// 添加栏目
        /// </summary>
        /// <param name="salesorder"></param>
        /// <returns></returns>
        public int AddSalesOrder(SalesOrder salesorder)
        {
            // 返回数据库受影响的行数
            return db.Insertable(salesorder).ExecuteCommand();
        }

        ///<summary>
        /// 根据主键删除栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteOrder(int id)
        {
            // 返回数据库受影响的行数
            return db.Deleteable<SalesOrder>().In(id).ExecuteCommand();
        }

        ///<summary>
        /// 查询全部栏目
        /// </summary>
        /// <param name="cid"></param>
        /// <returns>以列表形式返回</returns>
        public List<SalesOrder> QueryOrderByCid(int cid)
        {
            // 返回数据库受影响的行数
            return db.Queryable<SalesOrder>().Where(it=>it.cid == cid).OrderBy(it=>it.date,OrderByType.Asc).ToList();
        }
        #endregion
    }
}
