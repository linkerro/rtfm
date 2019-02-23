using System;
using System.Linq;

namespace Examples
{
    partial class Program
    {
        private class ParallelFor
        {
            internal static void Do()
            {
                var dbEntries = Enumerable.Range(0, 100)
                    .Select(i => new
                    {
                        CustomerId = i,
                        name = $"CustomerName {i}",
                        CustomerEntries = Enumerable.Range(0, 1000)
                        .Select(j => new { EntryId = j, Entry = $"Customer {i}, Entry {j}" })
                        .ToList()
                    })
                    .ToList();
                var result = "";
                var report = "";

                var start = DateTime.Now;
                //foreach (var entry in dbEntries)
                //{
                //    foreach (var customerEntry in entry.CustomerEntries)
                //    {
                //        report += customerEntry + Environment.NewLine;
                //    }
                //    result += $"{entry.name} {report} {Environment.NewLine}";
                //}

                //dbEntries.AsParallel().ForAll(entry =>
                //{
                //    entry.CustomerEntries.AsParallel().ForAll(customerEntry =>
                //    {
                //        report += customerEntry + Environment.NewLine;
                //    });
                //    result += $"{entry.name} {report} {Environment.NewLine}";
                //});

                //result = dbEntries.AsParallel().Select(entry =>
                //  {
                //      var intermediateReport=entry.CustomerEntries.AsParallel()
                //          .Select(customerEntry => customerEntry + Environment.NewLine)
                //          .Aggregate("", (agg, next) => agg += next);
                //      return $"{entry.name} {intermediateReport} {Environment.NewLine}";
                //  })
                //.Aggregate("", (agg, next) => agg += next);

                var end = DateTime.Now;

                //Console.WriteLine($"Result: {result}");
                Console.WriteLine($"Duration: {end.Subtract(start)}");
                Console.ReadKey();
            }
        }
    }
}
