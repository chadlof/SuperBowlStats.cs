using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Super_Bowl_Project.Models;
using Super_Bowl_Project.Utilities;


namespace Super_Bowl_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWriter originalOut = Console.Out;

            Console.WriteLine("Do you wish to save the output of this session to a file? [y/n]");
            var input = Console.ReadKey();

            bool saveOutput = false;

            if (input.Key == ConsoleKey.Y)
            {
                saveOutput = true;
            }

            try {
                Menu.HandleChoice(saveOutput);                
            }
            catch(Exception ex) {
                Console.SetOut(originalOut);
                Console.WriteLine("An error was encountered.  Please try again.");
                Console.WriteLine("The Error Message is: " + ex.Message);
            }
            finally {
                Console.SetOut(originalOut);
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }

    }
     
}
