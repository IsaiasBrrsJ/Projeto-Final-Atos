using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractcingRelationshipEntity.Models.Entity
{   
    /// <summary>
    /// Classe de endereco cliente
    /// </summary>
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = String.Empty;
        public string Number { get; set; } = String.Empty;
        public string City { get; set; }  = String.Empty;   
        public virtual Customer Customer { get; set; }
    }
}
