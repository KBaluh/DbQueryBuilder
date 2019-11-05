using System;

namespace ConnectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConnectionApp app = new ConnectionApp();
            app.Connect();

            Console.ReadLine();
        }
    }
}
