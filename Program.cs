using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication_P725_LINQtoSQL27_2
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwindDataContext northWindDataContext = new NorthwindDataContext();
            var totalResults =
                from c in northWindDataContext.Customers
                select new
                {
                    Country = c.Country,
                    Sales =
                     c.Orders.Sum(o => o.Order_Details.Sum(od => od.Quantity *

                        od.UnitPrice))
                }
                   ;
            Console.WriteLine(totalResults);
            Console.ReadKey();


            var groupResults =
                           from c in totalResults
                           group c by c.Country into cg
                           select new { TotalSales = cg.Sum(c => c.Sales), Country = cg.Key }
                       ;
            Console.WriteLine(groupResults);
            Console.ReadKey();


            var orderedResults =
                from cg in groupResults
                orderby cg.TotalSales descending
                select cg
             ;
            Console.WriteLine(orderedResults);
            Console.ReadKey();


            Console.WriteLine("Country\t\tTotal Sales\n--------\t------------1111111111111111111111111111111111111111");
            foreach (var item in totalResults)
            {
                Console.WriteLine("{0,-15}{1,12}", item.Country, item.Sales.ToString

("C2"));
            }

            Console.WriteLine("Country\t\tTotal Sales\n--------\t------------2222222222222222222222222222222222222");
            foreach (var item in groupResults)
            {
                Console.WriteLine("{0,-15}{1,12}", item.Country, item.TotalSales.ToString

("C2"));
            }

            Console.WriteLine("Country\t\tTotal Sales\n--------\t------------333333333333333333333333");
            foreach (var item in orderedResults)
            {
                Console.WriteLine("{0,-15}{1,12}", item.Country, item.TotalSales.ToString

("C2"));
            }
            Console.Write("Program finished, press Enter/Return to continue:");
            Console.ReadLine();
        }
    }
}
