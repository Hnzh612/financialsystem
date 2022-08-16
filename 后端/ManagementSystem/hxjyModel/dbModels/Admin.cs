using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyModel.dbModels
{
    [SugarTable("admin")]
    public class Admin
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int type { get; set; }
    }
}
