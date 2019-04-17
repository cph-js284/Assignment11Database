using System;

namespace generator
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName = "Generated";
            Creator c1 = new Creator();
            c1.BuildClassDefinitions("specfile.txt", FileName);
            Console.ReadLine();
        }
    }
}
