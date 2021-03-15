using System;

namespace TestApp
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++) 
            {
                Foo();
            }
        }

        static void Foo()
        {
            
            Console.WriteLine($"{random.Next(0,4)}");
        }
    }
}
