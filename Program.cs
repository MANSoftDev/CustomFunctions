using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlFunctions funcs = new XmlFunctions();

            funcs.FindAddressForType("home");
            funcs.FindAddressForTypeWithVariable("home");
        }
    }
}
