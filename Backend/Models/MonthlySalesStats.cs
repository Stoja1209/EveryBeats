using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EveryBeats.Backend.Models
{
    public class MonthlySalesStats
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}