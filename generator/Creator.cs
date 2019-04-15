using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace generator
{
    public class Creator
    {
        string EntireObj;
        public void BuildClassDefinitions(string filename){
            using (var fs = new FileStream(filename,FileMode.Open,FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    EntireObj = sr.ReadToEnd();
                }
            }
            Splitter();
        }


        private void Splitter(){
            var DsrObj = JsonConvert.DeserializeObject<dynamic>(EntireObj);

            JArray items = (JArray)DsrObj["entities"];
            int ASize = items.Count;
            System.Console.WriteLine(ASize);

            for (int i = 0; i < ASize; i++)
            {

                string tmpstr = Convert.ToString(DsrObj["entities"][i]);
                System.Console.WriteLine("-------------------------");


                var tmpstrClean = tmpstr.Replace(" ","").Replace("\t","").Replace("\r\n","").Replace("}}","").Split("{");

                System.Console.WriteLine("klassenavn : " + tmpstrClean[1]);

                var data = tmpstrClean[2].Split(",");

                foreach (var item in data)
                {
                    var subdata = item.Split(":");
                    System.Console.WriteLine($"navn : {subdata[0]} - type : {subdata[1]}");
                }


            }






        }

    }
}