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
    public class ParamController : ControllerBase
    {
        ParamService paramService = new ParamService();

        #region 品种控制器

        /// <summary>
        /// 获取全部品种
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Params> getAllParams()
        {
            return paramService.QueryParams();
        }

        /// <summary>
        /// 根据id删除参数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public int deleteParamById(int id)
        {
            return paramService.DeleteParam(id);
        }


        /// <summary>
        /// 添加品种
        /// </summary>
        /// <param name="paramvariet"></param>
        /// <returns></returns>
        [HttpPost]
        public int addParam(Params param)
        {
            return paramService.AddParam(param);
        }

        #endregion
    }
}
