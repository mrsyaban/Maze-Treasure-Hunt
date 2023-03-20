using System;
using Utility;

namespace main
{
    class main
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your filename: ");

            string fileName = Console.ReadLine();

            string path = "test/" + fileName;

            Matrix util = new Matrix();
            util.parserFile(path);

            util.printMatrix();

        }
    }
}
