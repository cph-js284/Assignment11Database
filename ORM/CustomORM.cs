using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using ORM.Models;

namespace ORM
{
    public class CustomORM
    {

        //that's right password placed directly in the source-code D8
        string CS = "Server=172.17.0.2;Port=3306;Database=MicroShop;Uid=root;Pwd=test1234;";


        private string CreateSqlQuery(string ORMquery){
            string sqlres="";

            if((!ORMquery.Contains("|")) && (!ORMquery.Contains(".")) ) {
                sqlres = "SELECT * FROM " + ORMquery +"_tbl";
                System.Console.WriteLine("Created : " + sqlres);
                return sqlres;
            }
            if((ORMquery.Contains("|")) && (!ORMquery.Contains(".")) ) {
                var tableName = ORMquery.Split("|")[0].Replace("(","");
                var wherepart = ORMquery.Split("|")[1].Replace(")","");
                sqlres="SELECT * FROM " + tableName+"_tbl where "+wherepart;
                System.Console.WriteLine("Created : " + sqlres);
                return sqlres;
            }

            if((!ORMquery.Contains("|")) && (ORMquery.Contains(".")) ) {
                var indata = ORMquery.Split(".");
                var tableName = indata[0]+"_tbl";
                var columns = "";
                for (int i = 1; i < indata.Length; i++)
                {
                    columns += indata[i] +",";
                }
                //remove the last ","
                columns = columns.Remove(columns.Length-1);
                sqlres = "SELECT " + columns +" FROM " + tableName;
                System.Console.WriteLine("Created : " + sqlres);
                return sqlres;
            }

            if((ORMquery.Contains("|")) && (ORMquery.Contains(".")) ) {
                var indata = ORMquery.Split(".");
                var tableName = indata[0].Split("|")[0].Replace("(","");
                var wherepart = indata[0].Split("|")[1].Replace(")","");
                var joinpart="";
                
                List<string> joinList = new List<string>();
                joinList.Add(tableName);

                for (int i = 1; i < indata.Length; i++)
                {
                    joinList.Add(indata[i]);
                }
                
                for (int i = 0; i < joinList.Count-1; i++)
                {
                    joinpart += " left join " + joinList[i+1]+"_tbl on " + joinList[i]+"_tbl."+joinList[i+1]+"_id = "+ joinList[i+1]+"_tbl."+joinList[i+1]+"_id";
                }

                sqlres = "SELECT * FROM " + tableName +"_tbl " + joinpart + " where " +wherepart;
                System.Console.WriteLine("Created : " + sqlres);
                return sqlres;
            }

            return sqlres;
        }

        public List<object> ExecuteQuery(string query){
            List<object> res = new List<object>();
            using (var conn = new MySqlConnection(CS))
            {
                conn.Open();
                using (var command = new MySqlCommand(CreateSqlQuery(query),conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                               
                                res.Add(reader.GetName(i).ToString() + " : " + reader.GetValue(i).ToString());
                            }
                        }
                    }
                }
                
            }
            return res ;
        }
    }
}