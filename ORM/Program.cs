using System;
using System.Collections.Generic;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomORM newOrm = new CustomORM();

            //list of queries take from assignment text, added extra for documentation
            List<string> OrmStrs = new List<string>(){
                "Customer",
                "Customer.name",
                "Product.name",
                "(Customer|name='Joe')",
                "(Product|name='Product3')",
                "(Product|price > 200 and price < 600)",
                "(Customer|name='Joe').Order",
                "(Customer|name='Joe').Order.OrderLine",
                "(Order| total > 14000).Customer"
            };


            foreach (var Ostr in OrmStrs)
            {
                System.Console.WriteLine("------------------------------------------------------");
                System.Console.WriteLine($"Testing ORM query : {Ostr}");
                var res = newOrm.ExecuteQuery(Ostr);
                System.Console.WriteLine("----------------------RESULT--------------------------");
                foreach (var item in res)
                {
                    System.Console.WriteLine(item);
                }
                
            }
            





        }
    }
}
