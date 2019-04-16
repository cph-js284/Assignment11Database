using System;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomORM newOrm = new CustomORM();
            var res = newOrm.Cquery("hej");
            foreach (var item in res)
            {
                System.Console.WriteLine($"{item.Customer_id}-{item.name}");
                    foreach (var listeitem in item.orders)
                    {
                        System.Console.WriteLine($"{listeitem.Order_id}-{listeitem.date}-{listeitem.total}");
                    }
            }
        }
    }
}
