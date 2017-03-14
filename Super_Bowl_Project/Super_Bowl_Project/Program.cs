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
            Console.WriteLine("Do you wish to save the output of this session to a file? [y/n]");
            var input = Console.ReadKey();

            bool saveOutput = false;
            
            TextWriter oldOut = Console.Out;

            if (input.Key == ConsoleKey.Y)
            {
                saveOutput = true;
            }

            
            try {
                
                if (saveOutput)
                {
                    try
                    {
                        using (var ostrm = new FileStream("output.txt", FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            using (var writer = new StreamWriter(ostrm))
                            {
                                Console.SetOut(writer);

                                Menu.HandleChoice();
                                Console.WriteLine("Beuhler?");
                            }
                        }
                        
                    }
                    catch (Exception e)
                    {
                        Console.SetOut(oldOut);
                        Console.WriteLine("Cannot open output.txt for writing");
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Menu.HandleChoice();
                }
            }
            catch(Exception ex) {
                Console.SetOut(oldOut);
                Console.WriteLine("An error was encountered.  Please try again.");
                Console.WriteLine("The Error Message is: " + ex.Message);
            }
            finally {
                Console.SetOut(oldOut);
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }

    }
     
}
