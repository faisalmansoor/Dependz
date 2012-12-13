using System;
using Library2;

namespace Library1
{
    public class Lib1
    {
        public static void Use()
        {
            Console.WriteLine("Library.Lib1.Use");
            Lib2.Use();
        }
    }
}
