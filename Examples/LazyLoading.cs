using Examples.OtherStuff;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    class LazyLoading
    {
        public static void Do()
        {
            using (var dbContext = new ExampleContext())
            {
                dbContext.Database.EnsureCreated();

                var persons = dbContext.Persons
                    .ToList();
                var personTotals = persons
                    .Select(p => new
                    {
                        Name = $"{p.FirstName} {p.LastName}",
                        TotalSold=p.Invoices.Sum(i=>i.ProductsToInvoices.Sum(pti=>pti.Quantity*pti.Product.Price))
                    })
                    .ToList();
            }
        }
    }
}
