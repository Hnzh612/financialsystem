using hxjyModel;
using hxjyModel.apiModels;
using hxjyModel.dbModels;
using hxjyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hxjyController
{
    /// <summary>
    /// 参数接口
    /// </summary>
    /// <returns></returns>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SalesController:ControllerBase
    {
        SalesService salesservice = new SalesService();
        #region 销售订单控制器

        /// <summary>
        /// 获取全部订单
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sname"></param>
        /// <param name="scompany"></param>
        /// <param name="rname"></param>
        /// <param name="rcompany"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public CallBackModels getAllSalesConnect(int page, string? sname, string? scompany ,string? rname , string ?rcompany,int rid=0)
        {
            return salesservice.QuerySalesConnect(page,sname,scompany,rname,rcompany,rid);
        }

        /// <summary>
        /// 根据id删除订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public int deleteSalesConnectById(int id)
        {
            return salesservice.DeleteSalesConnect(id);
        }


        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="salesconnect"></param>
        /// <returns></returns>
        [HttpPost]
        public int addSalesConnect(SalesConnect salesconnect)
        {
            return salesservice.AddSalesConnect(salesconnect);
        }

        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="salesconnect"></param>
        /// <returns></returns>
        [HttpPost]
        public int updateSalesConnect(SalesConnect salesconnect)
        {
            return salesservice.UpdateConnect(salesconnect);
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="salesorder"></param>
        /// <returns></returns>
        [HttpPost]
        public int addSalesOrder(SalesOrder salesorder)
        {
            return salesservice.AddSalesOrder(salesorder);
        }

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public int deleteSalesOrder(int id)
        {
            return salesservice.DeleteOrder(id);
        }

        /// <summary>
        /// 获取所有栏目
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<SalesOrder> SelectSalesOrders( int cid )
        {
            return salesservice.QueryOrderByCid(cid);
        }
        #endregion
    }
}
