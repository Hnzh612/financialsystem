using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyModel
{
    [SugarTable("user")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar", Length = 50)]
        public string username { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar", Length = 50)]
        public string password { get; set; }
        [SugarColumn(ColumnDataType = "int", Length = 50)]
        public int type { get; set; }
    }
}
