using System;
using Proiect.Models.Base;
using System.Text.Json.Serialization;
namespace Proiect.Models
{
    public class Products: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Price { get; set; }

        public ICollection<Rentings> Rentings { get; set; }

        public ICollection<Orders> Orders { get; set; }

    }
}
