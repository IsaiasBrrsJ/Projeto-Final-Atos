using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractcingRelationshipEntity.Models.Entity
{   
    /// <summary>
    /// Classe de fornecedor
    /// </summary>
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CNPJ { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
