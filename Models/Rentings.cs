using System;
using System.Collections.Generic;
using System.Linq;
using Proiect.Models.Base;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Rentings: BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }


        public Guid ProductsId { get; set; }
        public Products Products { get; set; }

        public string? DateStart { get; set; }
        public string? DateFinished { get; set; }
        public string TotalPrice { get; set; }
    }
}
