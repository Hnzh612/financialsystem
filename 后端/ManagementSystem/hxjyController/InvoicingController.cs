using hxjyModel.dbModels;
using hxjyModel.apiModels;
using hxjyServices;
using SqlSugar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace hxjyController
{
    /// <summary>
    /// 业务发生台账
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class InvoicingController:ControllerBase
    {
        InvoicingService invoicingservice = new InvoicingService();

        /// <summary>
        /// 获取所有时间进销存总和列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CallBackModels GetAllSum(int page)
        {
            return invoicingservice.GetAllSum(page);
        }

        /// <summary>
        /// 新建某月进销存总和
        /// </summary>
        /// <param name="invoicing_Ledger_Sum"></param>
        /// <returns></returns>
        [HttpPost]
        public int AddSum(Invoicing_Ledger_Sum invoicing_Ledger_Sum)
        {
            return invoicingservice.AddSum(invoicing_Ledger_Sum);
        }

        /// <summary>
        /// 删除当月进销存总和以及所连接的栏目
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        [HttpGet]
        public CallBackModels DelSum(int sid)
        {
            return invoicingservice.DelSum(sid);
        }

        /// <summary>
        /// 更新当月进销存总和
        /// </summary>
        /// <param name="invoicing_Ledger_Sum"></param>
        /// <returns></returns>
        [HttpPost]
        public int UpdateSum(Invoicing_Ledger_Sum invoicing_Ledger_Sum)
        {
            return invoicingservice.UpdateSum(invoicing_Ledger_Sum);
        }

        /// <summary>
        /// 根据sid获取相应的进销存栏目
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Invoicing_Ledger> GetAllLedgercsBySid(int sid)
        {
            return invoicingservice.GetAllLedgercsBySid(sid);
        }

        /// <summary>
        /// 添加进销存栏目
        /// </summary>
        /// <param name="invoicing_Ledger"></param>
        /// <returns></returns>
        [HttpPost]
        public int AddLedgercs(Invoicing_Ledger invoicing_Ledger)
        {
            return invoicingservice.AddLedgercs(invoicing_Ledger);
        }

        /// <summary>
        /// 删除进销存栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public int DelLedgercs(int id)
        {
            return invoicingservice.DelLedgercs(id);
        }
    }
}
