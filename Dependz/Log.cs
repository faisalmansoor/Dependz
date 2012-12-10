using System;

namespace Dependz
{
    class Log
    {
        public static void Error(string format, params object [] args)
        {
            Console.WriteLine(format, args);
        }
    }
}