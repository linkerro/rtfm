using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examples.OtherStuff
{
    public class ExampleContext : DbContext
    {
        public DbSet<Person> Persons {get;set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductsToInvoices>()
                .HasKey(pti => new { pti.InvoiceId, pti.ProductId });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Data Source=DESKTOP-NV5HV4T;Database=ExampleDatabase;Persist Security Info=True;User ID=sa;Password=asdqwe123")
                .UseLazyLoadingProxies();
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }

    public class ProductsToInvoices
    {
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Invoice Invoice { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual ICollection<ProductsToInvoices> ProductsToInvoices { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int CreatedByUserId { get; set; }
        public virtual Person CreatedByUser { get; set; }
        public virtual ICollection<ProductsToInvoices> ProductsToInvoices { get; set; }
    }

}
