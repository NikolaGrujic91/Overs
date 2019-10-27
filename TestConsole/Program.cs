using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataCollector;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var overs = new OversDetector.OversDetector();
            overs.Run();
            overs.PrintCandidates();

            Console.ReadLine();
        }
    }
}
