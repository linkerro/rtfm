﻿// <auto-generated />
using Examples.OtherStuff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Examples.Migrations
{
    [DbContext(typeof(ExampleContext))]
    partial class ExampleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Examples.OtherStuff.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByUserId");

                    b.Property<string>("CustomerName");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Examples.OtherStuff.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Examples.OtherStuff.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Examples.OtherStuff.ProductsToInvoices", b =>
                {
                    b.Property<int>("InvoiceId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("InvoiceId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsToInvoices");
                });

            modelBuilder.Entity("Examples.OtherStuff.Invoice", b =>
                {
                    b.HasOne("Examples.OtherStuff.Person", "CreatedByUser")
                        .WithMany("Invoices")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Examples.OtherStuff.ProductsToInvoices", b =>
                {
                    b.HasOne("Examples.OtherStuff.Invoice", "Invoice")
                        .WithMany("ProductsToInvoices")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Examples.OtherStuff.Product", "Product")
                        .WithMany("ProductsToInvoices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
