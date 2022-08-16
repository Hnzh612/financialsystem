using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyModel.dbModels
{
    [SugarTable("sales_order")]
    public class SalesOrder
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public int cid { get; set; }
        public long date { get; set; }
        public string variety { get; set; }
        public string material { get; set; }
        public string city { get; set; }
        public string specifications { get; set; }
        public double weight { get; set; }
        public decimal unitprice { get; set; }
        public decimal amount { get; set; }

    }

    [SugarTable("sales_connect")]
    public class SalesConnect
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public long date { get; set; }
        public int sid { get; set; }
        public string sname { get; set; }
        public string sphone { get; set; }
        public string sfax { get; set; }
        public string scompany { get; set; }
        public int rid { get; set; }
        public string rname { get; set; }
        public string rphone { get; set; }
        public string rfax { get; set; }
        public string rcompany { get; set; }
    }

}
