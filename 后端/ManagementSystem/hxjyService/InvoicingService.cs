using hxjyModel.apiModels;
using hxjyModel.dbModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyServices
{
    /// <summary>
    /// 进销存台账
    /// </summary>
    public class InvoicingService
    {
        SqlSugarScope db = DBContent.GetDB();

        /// <summary>
        /// 获取所有时间业务总和列表
        /// </summary>
        /// <returns></returns>
        public CallBackModels GetAllSum(int page)
        {
            CallBackModels callBack = new CallBackModels();
            int total = db.Ado.GetInt("select count(1) from invoicing_ledger_sum");
            callBack.message = total.ToString();
            callBack.data = db.Queryable<Invoicing_Ledger_Sum>().ToPageList(page, 10).OrderBy(it => it.date).ToList();
            return callBack;
        }

        /// <summary>
        /// 新建某月业务总和
        /// </summary>
        /// <param name="invoicing_ledger_sum"></param>
        /// <returns></returns>
        public int AddSum(Invoicing_Ledger_Sum invoicing_ledger_sum)
        {
            return db.Insertable(invoicing_ledger_sum).ExecuteCommand();
        }

        /// <summary>
        /// 删除当月业务总和
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CallBackModels DelSum(int id)
        {
            CallBackModels callBack = new CallBackModels();
            int a = db.Deleteable<Invoicing_Ledger_Sum>().In(id).ExecuteCommand();
            int b = db.Deleteable<Invoicing_Ledger>().Where(it => it.sid == id).ExecuteCommand();
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
        /// <param name="invoicing_ledger_sum"></param>
        /// <returns></returns>
        public int UpdateSum(Invoicing_Ledger_Sum invoicing_ledger_sum)
        {
            int c = db.Ado.GetInt($"select id from invoicing_ledger_sum where id={invoicing_ledger_sum.id}");
            if (c > 0)
            {
                string updatesql = "";
                if (invoicing_ledger_sum.date <= 0)
                {
                    updatesql = $"update invoicing_ledger_sum set inweightsum={invoicing_ledger_sum.inweightsum},inamountsum={invoicing_ledger_sum.inamountsum}, ";
                    updatesql += $"outweightsum={invoicing_ledger_sum.outweightsum} ,outamountsum={invoicing_ledger_sum.outamountsum},";
                    updatesql += $"leftweightsum={invoicing_ledger_sum.leftweightsum},leftamountsum={invoicing_ledger_sum.leftamountsum}";
                }
                else
                {
                    updatesql = $"update invoicing_ledger_sum set date={invoicing_ledger_sum.date},remark='{invoicing_ledger_sum.remark}'";
                }
                updatesql += $" where id={invoicing_ledger_sum.id}";
                db.Ado.ExecuteCommand(updatesql);
                return 2;
            }
            else
            {
                string insertsql = " insert invoicing_ledger_sum (date,remark) ";
                insertsql += $"values({invoicing_ledger_sum.date},'{invoicing_ledger_sum.remark}')";
                return db.Ado.ExecuteCommand(insertsql);
            }
        }

        /// <summary>
        /// 根据sid获取相应的业务发生栏目
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public List<Invoicing_Ledger> GetAllLedgercsBySid(int sid)
        {
            return db.Queryable<Invoicing_Ledger>().Where(it => it.sid == sid).ToList();
        }

        /// <summary>
        /// 添加业务发生栏目
        /// </summary>
        /// <param name="invoicing_ledger"></param>
        /// <returns></returns>
        public int AddLedgercs(Invoicing_Ledger invoicing_ledger)
        {
            return db.Insertable(invoicing_ledger).ExecuteCommand();
        }

        /// <summary>
        ///  删除业务发生栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelLedgercs(int id)
        {
            return db.Deleteable<Invoicing_Ledger>().In(id).ExecuteCommand();
        }
    }
}
