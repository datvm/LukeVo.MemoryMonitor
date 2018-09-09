using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.MemoryMonitor
{
    public class Program
    {

        public static void Main(string[] args)
        {
            using (var memoryLogger= new MemoryLogger())
            {
                memoryLogger.Start();
                Console.WriteLine("Service started! Press ENTER to exit...");
                Console.ReadLine();

                Console.WriteLine("Service ending...");
                memoryLogger.Stop();
            }

            
        }

    }
}
