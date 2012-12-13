using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MainApp.Main called.");
            Library1.Lib1.Use();

            new NativeLibWrapper.NativeLibWrapper().UseNativeLib();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
