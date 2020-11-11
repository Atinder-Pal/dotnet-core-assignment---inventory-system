using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystemDay2.Models
{
    public class InventoryContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connection =
                    "server=localhost;" +
                    "port = 3306;" +
                    "user = root;" +
                    "database = mvc_inventory;";

                string version = "10.4.14-MariaDB";

                optionsBuilder.UseMySql(connection, x => x.ServerVersion(version));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasData(                         
                      new Product()
                      {
                          ID = -5,
                          Name = "IPhone",
                          Quantity = 200
                      },
                      new Product()
                      {
                          ID = -6,
                          Name = "IPad",
                          Quantity = 500
                      },
                      new Product()
                      {
                          ID = -7,
                          Name = "Heater",
                          Quantity = 50
                      },
                      new Product()
                      {
                          ID = -8,
                          Name = "Fan",
                          Quantity = 200
                      },
                      new Product()
                      {
                          ID = -9,
                          Name = "Water Bottle",
                          Quantity = 200
                      },
                      new Product()
                      {
                          ID = -10,
                          Name = "Chair",
                          Quantity = 200
                      },
                      new Product()
                      {
                          ID = -11,
                          Name = "Pen",
                          Quantity = 200,
                          Discontinued = true
                      }
                );

            });
        }
    }
}
