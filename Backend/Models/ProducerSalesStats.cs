using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EveryBeats.Backend.Models
{
    public class ProducerSalesStats
    {
        public int ProducerID { get; set; }
        public string ArtistName { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}