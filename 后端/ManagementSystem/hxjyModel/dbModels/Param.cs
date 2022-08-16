using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyModel.dbModels
{
    /// <summary>
    /// 销售参数
    /// </summary>
    [SugarTable("sales_params")]
    public class Params
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
    }
}
