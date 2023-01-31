using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractcingRelationshipEntity.Models.Entity
{
    /// <summary>
    /// Classe de cliente
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public  virtual Address Address { get; set; }
        public int AddressId { get; set; }
    }
}
