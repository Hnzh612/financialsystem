using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hxjyModel.dbModels
{
    [SugarTable("customer")]
    public class Customer
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public string cname { get; set; }
        public string phone { get; set; }
        public string company { get; set; }
        public string fax { get; set; }
    }
}

