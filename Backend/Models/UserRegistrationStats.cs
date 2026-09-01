using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EveryBeats.Backend.Models
{
    public class UserRegistrationStats
    {
        public DateTime RegistrationDate { get; set; }
        public int UserCount { get; set; }
    }
}