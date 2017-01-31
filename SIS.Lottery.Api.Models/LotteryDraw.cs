using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Lottery.Api.Models
{
    public class LotteryDraw
    {
        public object _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DrawDate { get; set; }
        public int TotalPrimaryNumbers { get; set; }
        public Tuple<int,int> RangePrimaryNumbers { get; set; }
        public int TotalSecondaryNumbers { get; set; }
        public Tuple<int,int> RangeSecondaryNumbers { get; set; }
        public int[] WinningPrimaryNumbers { get; set; }
        public int[] WinningSecondaryNumbers { get; set; }        
    }
}
