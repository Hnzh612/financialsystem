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
    public class BusinessledgercsController:ControllerBase
    {
        BusinessledgercsService businessledgercsService = new BusinessledgercsService();

        /// <summary>
        /// 获取所有时间业务总和列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CallBackModels GetAllSum(int page)
        {
            return businessledgercsService.GetAllSum(page);
        }

        /// <summary>
        /// 新建某月业务总和
        /// </summary>
        /// <param name="businessledgercssum"></param>
        /// <returns></returns>
        [HttpPost]
        public int AddSum(Businessledgercssum businessledgercssum)
        {
            return businessledgercsService.AddSum(businessledgercssum);
        }

        /// <summary>
        /// 删除当月业务总和以及所连接的栏目
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        [HttpGet]
                [Authorize]
        public CallBackModels DelSum(int sid)
        {
            return businessledgercsService.DelSum(sid);
        }

        /// <summary>
        /// 更新当月业务总和
        /// </summary>
        /// <param name="businessledgercssum"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public int UpdateSum(Businessledgercssum businessledgercssum)
        {
            return businessledgercsService.UpdateSum(businessledgercssum);
        }

        /// <summary>
        /// 根据sid获取相应的业务发生栏目
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public List<Businessledgercs> GetAllLedgercsBySid(int sid)
        {
            return businessledgercsService.GetAllLedgercsBySid(sid);
        }

        /// <summary>
        /// 添加业务发生栏目
        /// </summary>
        /// <param name="businessledgercs"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public int AddLedgercs(Businessledgercs businessledgercs)
        {
           return businessledgercsService.AddLedgercs(businessledgercs);
        }

        /// <summary>
        /// 删除业务发生栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public int DelLedgercs(int id)
        {
            return businessledgercsService.DelLedgercs(id);
        }
    }
}
