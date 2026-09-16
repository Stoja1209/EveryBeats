using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EveryBeats.Backend.Models
{
    public class ProductSalesCount
    {
        public string BeatTitle { get; set; }
        public int SalesCount { get; set; }
        public int OnHand { get; set; }
    }
}
