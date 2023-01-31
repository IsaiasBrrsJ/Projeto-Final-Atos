using Microsoft.EntityFrameworkCore;
using PractcingRelationshipEntity.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractcingRelationshipEntity.Models
{
    public  class DataConteXT : DbContext
    { 
        public DbSet<Address>  Address { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Provider> Providers { get; set; }
        public DataConteXT() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionBase = "Server=localhost;Database=MyCustomerBase;Trusted_Connection=True;TrustServerCertificate=True;language=Portuguese";
            optionsBuilder.UseSqlServer(connectionBase);
        }

    }
}
