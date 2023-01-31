using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractcingRelationshipEntity.Models.Entity
{
    /// <summary>
    /// Classe de produto
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Value { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
