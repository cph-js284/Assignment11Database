using System;

namespace generator
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName = "myFile";
            Creator c1 = new Creator();
            c1.BuildClassDefinitions("specfile.txt", FileName);
        }
    }
}
