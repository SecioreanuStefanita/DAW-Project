using System;
using Proiect.Models.Base;
using System.Text.Json.Serialization;
namespace Proiect.Models
{
    public class Orders: BaseEntity
    {
         public User User { get; set; }

        public Guid UserId { get; set; }

        public string Price { get; set; }

        public Products Products { get; set; }

        public Guid ProductsId { get; set; }

    }
}
