using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Zad2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Type t = Type.GetTypeFromProgID("KSR20.COM3Klasa.1");
            object k = Activator.CreateInstance(t);
            t.InvokeMember("Test", System.Reflection.BindingFlags.InvokeMethod,
            null, k, new object[] { "Zad2" });
            Console.WriteLine(k);
        }
    }
}
