using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ORM.Models;

namespace ORM
{
    public class CustomORM
    {
        public List<Customer> Cquery(string query){
            List<Customer> res = new List<Customer>();
            //that's right password placed directly in the source-code D8
            string CS = "Server=172.17.0.2;Port=3306;Database=MicroShop;Uid=root;Pwd=test1234;";
            using (var conn = new MySqlConnection(CS))
            {
                conn.Open();
                using (var command = new MySqlCommand("select * from Customer_tbl left join Order_tbl on Customer_tbl.Order_id = Order_tbl.Order_id",conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(
                                new Customer(){
                                    Customer_id = reader.GetInt32("Customer_id"),
                                    name = reader.GetString("name"),
                                    orders = new List<Order>(){
                                                new Order(){
                                                    Order_id=reader.GetInt32("Order_id"),
                                                    date = reader.GetString("date"),
                                                    total = reader.GetInt32("total")
                                            }
                                    }    
                                }
                            );
                        }
                    }
                }
            }

            return res;
        }
    }
}