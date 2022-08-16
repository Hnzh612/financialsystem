using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyModel.dbModels
{
    [SugarTable("business_ledger")]
    public class Businessledgercs
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public long date { get; set; }
        public decimal incoin { get; set; }
        public decimal outcoin { get; set; }
        public decimal surplus { get; set; }
        public string remark { get; set; }
        public string buyer { get; set; }
        public int sid { get; set; }
    }

    [SugarTable("business_ledger_sum")]
    public class Businessledgercssum
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public long date { get; set; }
        public decimal incoinsum { get; set; }
        public decimal outcoinsum { get; set; }
        public decimal surplus { get; set; }
        public string remark { get; set; }
    }
}
