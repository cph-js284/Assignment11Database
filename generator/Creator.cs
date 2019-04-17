using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace generator
{
    public class Creator
    {
        string TableName,SchemaName,EntireObj;

    #region BuildClassDef
        public void BuildClassDefinitions(string filename, string outputFileName){
            using (var fs = new FileStream(filename,FileMode.Open,FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    EntireObj = sr.ReadToEnd();
                }
            }
            Splitter(outputFileName);
            System.Console.WriteLine($"File created : {outputFileName}");
        }


        private void Splitter(string outFileName){
            var DsrObj = JsonConvert.DeserializeObject<dynamic>(EntireObj);
            SchemaName = Convert.ToString(DsrObj["schemaName"]);
            string ClassFileName = outFileName + ".cs";
            string SQLFileName = outFileName + ".sql";
            JArray items = (JArray)DsrObj["entities"];
            int ASize = items.Count;

            //Create database
            SQLFileBuilder(SQLFileName, SchemaName, "database");

            for (int i = ASize-1; i >= 0; i--)
            {
                string tmpstr = Convert.ToString(DsrObj["entities"][i]);
                System.Console.WriteLine("--------Discovery-----------------");
                var tmpstrClean = scrubber(tmpstr,"1").Split("{");
                System.Console.WriteLine("ClassName : " + tmpstrClean[1]);
                //create class
                ClassFileBuilder(ClassFileName, scrubber(tmpstrClean[1],"2"), "class");
                //create table
                SQLFileBuilder(SQLFileName,scrubber(tmpstrClean[1],"2"), "table");
                var data = tmpstrClean[2].Split(",");

                foreach (var item in data)
                {
                    var subdata = item.Split(":");
                    System.Console.WriteLine($"Name : {subdata[0]} - Type : {subdata[1]}");
                    if(subdata[1].Contains("*")){
                        var tmplistType = subdata[1].Split("*");
                        ClassFileBuilder(ClassFileName, scrubber(subdata[0],"2"), "list",tmplistType[1].Replace("\"",""));
                        SQLFileBuilder(SQLFileName, attribType:"onetomany", KeyRef:tmplistType[1].Replace("\"",""));
                    }else{
                        ClassFileBuilder(ClassFileName, scrubber(subdata[0],"2"),scrubber(subdata[1],"2"));
                        SQLFileBuilder(SQLFileName,scrubber(subdata[0],"2"),scrubber(subdata[1],"2"));
                    }
                }
                ClassFileBuilder(ClassFileName, attribType:"close");
                SQLFileBuilder(SQLFileName, attribType:"close");
            }
        }

        private void ClassFileBuilder(string FileName, string attribName="none", string attribType="none", string ListType="none"){
            using (var fs = new FileStream(FileName, FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    string strres="";
                    switch (attribType)
                    {   
                        case "class":
                            strres = "\npublic class "+attribName+" " + "{\n public int " + attribName + "_id {get; set; }";
                            sw.WriteLine(strres);
                            break;

                        case "String":
                            strres = "public string "+attribName+" " + "{get; set; }";
                            sw.WriteLine(strres);
                            break;

                        case "Number":
                            strres = "public int "+attribName+" " + "{get; set; }";
                            sw.WriteLine(strres);
                            break;
                        
                        case "list":
                            strres = "public System.Collections.Generic.List<" + ListType +"> "+attribName+" " + "{get; set; }";
                            sw.WriteLine(strres);
                            break;

                        case "close":
                            sw.WriteLine("}\n");
                            break;
                        default:
                            strres = "public "+ attribType + " " + attribName + "{get; set; }";
                            sw.WriteLine(strres);
                            break;
                    }
                }
            }
        }

        private string scrubber(string dirty, string type){
            string res = "";

            switch (type)
            {
                case "1":
                    res = dirty.Replace(" ","").Replace("\t","").Replace("\n","").Replace("}}","");
                    break;
                case "2":
                    res = dirty.Replace("\"","").Replace(":","");
                    break;
                default:
                    break;
            }
            return res;
        }
    #endregion
    
    #region BuildSQL

        private void SQLFileBuilder(string FileName, string attribName="none", string attribType="none", string KeyRef="none"){
            string strres="";
            
            using (var fs = new FileStream(FileName, FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    switch (attribType)
                    {
                        case "database":
                            strres = "DROP DATABASE IF EXISTS " + attribName+";\n"+ "CREATE DATABASE " + attribName + ";\n" + "USE " + attribName +";" ;
                            sw.WriteLine(strres);
                            break;

                        case "table":
                            strres = "\nCREATE TABLE " + attribName+"_tbl" + "(\n" + attribName+"_id INT NOT NULL AUTO_INCREMENT" + "\n" + ",PRIMARY KEY(" + attribName + "_id)";
                            TableName=attribName;
                            sw.WriteLine(strres);
                            break;
                        case "String":
                            strres = ","+ attribName + " " + "VARCHAR(100)";
                            sw.WriteLine(strres);
                            break;
                        case "Number":
                            strres =","+  attribName + " " + "INT";
                            sw.WriteLine(strres);
                            break;
                        case "onetomany":
                            strres =","+ KeyRef + "_id INT";
                            sw.WriteLine(strres);
                            break;

                        case "close":
                            strres = ");\n";
                            sw.WriteLine(strres);
                            break;

                        default:
                            strres =","+ attribName + "_id INT"; 
                            sw.WriteLine(strres);
                            break;
                    }
                }
            }            
        }

    #endregion
    }
}