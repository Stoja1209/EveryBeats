using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EveryBeats.Backend.Models
{
    public class GenreSalesStats
    {
        public string GenreName { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}