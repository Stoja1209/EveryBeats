using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EveryBeats.Backend.Models
{
    public class Invoice
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
    public class InvoiceItem
    {
        public string BeatTitle { get; set; }
        public string LicenseType { get; set; }
        public decimal PricePaid { get; set; }
    }
}