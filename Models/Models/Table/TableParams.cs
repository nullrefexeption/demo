using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Table
{
    public class TableParams
    {
        public List<TableFilterItem> Filters { get; set; }

        public int First { get; set; }

        public string GlobalFilter { get; set; }

        public int Rows { get; set; } = 10;

        public string SortField { get; set; }

        public int SortOrder { get; set; }

        public override string ToString()
        {
            return
                $"{First} {GlobalFilter} {Rows} {SortField} {SortOrder} {(Filters.Count > 0 ? Filters.Select(x => $"{x.Field} : {x.Value}").Aggregate((a, b) => a + ", " + b) : "")}";
        }
    }
}
