using System;

namespace generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Creator c1 = new Creator();
            c1.BuildClassDefinitions("specfile.txt");
        }
    }
}
