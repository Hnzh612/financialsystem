using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyModel.dbModels
{
    [SugarTable("invoicing_ledger")]
    public class Invoicing_Ledger
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public long date { get; set; }
        public string bcompany { get; set; }
        public string variety { get; set; }
        public string material { get; set; }
        public string city { get; set; }
        public string specifications { get; set; }
        public double inweight { get; set; }
        public decimal inunitprice { get; set; }
        public decimal inamount { get; set; }
        public double outweight { get; set; }
        public decimal outunitprice { get; set; }
        public decimal outamount { get; set; }
        public double leftweight { get; set; }
        public decimal leftamount { get; set; }
        public int sid { get; set; }
    }
    [SugarTable("invoicing_ledger_sum")]
    public class Invoicing_Ledger_Sum
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public long date { get; set; }
        public double inweightsum { get; set; }
        public decimal inamountsum { get; set; }
        public double outweightsum { get; set; }
        public decimal outamountsum { get; set; }
        public double leftweightsum { get; set; }
        public decimal leftamountsum { get; set; }
        public string remark { get; set; }
    }
}
