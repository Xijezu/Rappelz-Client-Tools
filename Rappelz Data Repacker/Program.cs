using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rappelz_Data_Repacker
{
    class Program
    {
        static void Main(string[] args)
        {
            bool debug = false;
            XClientManager manager = new XClientManager();
            Console.WriteLine("Would you like to debug? Y/N");
            debug = Console.ReadKey(false).Key == ConsoleKey.Y ? true : false;
            if (debug)
            {
                Console.WriteLine("Debugging...");
            }
            Console.WriteLine("Reading old data.000...");
            manager.Open("F:\\Games\\DarknessFight\\data.000");

            foreach(CSDATA_INDEX indexew in manager.m_lClient.ToList())
            {
                if(manager.Patch("F:\\Games\\DarknessFight", indexew) == -1 && debug)
                {
                    Console.WriteLine("Filtered: {0}", indexew.szName);
                }
            }
            Console.WriteLine("Writing data.000...");
            for(int i = 1; i < 9; i++)
            {
                System.IO.File.Move(manager.tempDataName[i], "F:\\Games\\DarknessFight\\new\\data.00" + i);
            }
            manager.Save("F:\\Games\\DarknessFight\\new\\data.000");
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
