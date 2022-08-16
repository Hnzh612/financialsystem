using hxjyModel.dbModels;
using hxjyModel.apiModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyServices
{
    /// <summary>
    /// 业务发生台账
    /// </summary>
    public class BusinessledgercsService
    {
        SqlSugarScope db = DBContent.GetDB();


        /// <summary>
        /// 获取所有时间业务总和列表
        /// </summary>
        /// <returns></returns>
        public CallBackModels GetAllSum(int page)
        {
            CallBackModels callBack = new CallBackModels();
            int total = db.Ado.GetInt("select count(1) from business_ledger_sum");
            callBack.message = total.ToString();
            callBack.data = db.Queryable<Businessledgercssum>().ToPageList(page, 10).OrderBy(it => it.date).ToList();
            return callBack;
        }

        /// <summary>
        /// 新建某月业务总和
        /// </summary>
        /// <param name="businessledgercssum"></param>
        /// <returns></returns>
        public int AddSum(Businessledgercssum businessledgercssum)
        {
            return db.Insertable(businessledgercssum).ExecuteCommand();
        }

        /// <summary>
        /// 删除当月业务总和
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CallBackModels DelSum(int id)
        {
            CallBackModels callBack = new CallBackModels();
            int a = db.Deleteable<Businessledgercssum>().In(id).ExecuteCommand();
            int b = db.Deleteable<Businessledgercs>().Where(it => it.sid == id).ExecuteCommand();
            callBack.successed = true;
            if (a > 0 && b > 0)
            {
                callBack.message = "已将所有关联数据删除";
            }
            else if (a > 0 && b <= 0)
            {
                callBack.message = "已将此业务总和删除";
            }
            else
            {
                callBack.message = "您要删除的数据不存在";
            }
            return callBack;
        }
        /// <summary>
        /// 更新当月业务总和
        /// </summary>
        /// <param name="businessledgercssum"></param>
        /// <returns></returns>
        public int UpdateSum(Businessledgercssum businessledgercssum)
        {
            int c = db.Ado.GetInt($"select id from business_ledger_sum where id={businessledgercssum.id}");
            if (c > 0)
            {
                string updatesql = "";
                if (businessledgercssum.date <= 0)
                {
                    updatesql = $"update business_ledger_sum set incoinsum={businessledgercssum.incoinsum},outcoinsum={businessledgercssum.outcoinsum},surplus={businessledgercssum.surplus}  ";
                }
                else
                {
                    updatesql = $"update business_ledger_sum set date={businessledgercssum.date},remark='{businessledgercssum.remark}'";
                }
                updatesql += $" where id={businessledgercssum.id}";
                db.Ado.ExecuteCommand(updatesql);
                return 2;
            }
            else
            {
                string insertsql = " insert business_ledger_sum (date,remark) ";
                insertsql += $"values({businessledgercssum.date},'{businessledgercssum.remark}')";
                return db.Ado.ExecuteCommand(insertsql);
            }
        }

        /// <summary>
        /// 根据sid获取相应的业务发生栏目
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public List<Businessledgercs> GetAllLedgercsBySid(int sid)
        {
            return db.Queryable<Businessledgercs>().Where(it => it.sid == sid).ToList();
        }

        /// <summary>
        /// 添加业务发生栏目
        /// </summary>
        /// <param name="businessledgercs"></param>
        /// <returns></returns>
        public int AddLedgercs(Businessledgercs businessledgercs)
        {
            return db.Insertable(businessledgercs).ExecuteCommand();
        }

        /// <summary>
        ///  删除业务发生栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelLedgercs(int id)
        {
            return db.Deleteable<Businessledgercs>().In(id).ExecuteCommand();
        }
    }
}
