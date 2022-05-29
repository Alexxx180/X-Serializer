using System;
using Processors;

namespace XSerializer
{
    internal class Program
    {
        static Program()
        {
            _serializer = new Json();
        }

        internal static void Main(string[] args)
        {
            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }

        private static Serializer _serializer;
    }
}