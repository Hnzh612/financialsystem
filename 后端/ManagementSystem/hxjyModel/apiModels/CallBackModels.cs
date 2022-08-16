using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyModel.apiModels
{
    /// <summary>
    /// 接口返回数据通用格式
    /// </summary>
    public class CallBackModels
    {
        public bool successed { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
