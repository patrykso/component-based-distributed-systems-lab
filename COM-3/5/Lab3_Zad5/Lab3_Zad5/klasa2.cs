﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Zad5
{
    public class klasa2
    {

    }

    [Guid("F59DA79E-29BB-476C-BFF4-2E9C0ADFDD4D"),
    ComVisible(true),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IKlasa2
    {
        void Test(string v);

    }

    [Guid("F08FB011-E87D-472E-9886-659C2559FB10"),
    ComVisible(true),
    ClassInterface(ClassInterfaceType.None),
    ProgId("KSR20.COM3Klasa.2")]
    public class Klasa2 : IKlasa2
    {
        public Klasa2() { }
        public void Test(string v)
        {
            Console.WriteLine(v);
        }
    }
}
