using Super_Bowl_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project.Utilities
{
    public static class DataAccessLayer
    {
        private static readonly string filename = "Super_Bowl_Project_Fixed.csv";

        private static List<ISuperbowl> _superbowls;

        public static List<ISuperbowl> Superbowls
        {
            get
            {
                //Make sure we only parse the data file once per application run
                if (_superbowls == null)
                {
                    _superbowls = new List<ISuperbowl>();
                    //if we haven't parsed the data file yet, do that and save it to memory
                    var listOfValues = _getLines().Skip(1);
                    foreach (var line in listOfValues)
                    {
                        _superbowls.Add(Superbowl.Create(line));
                    }
                }

                //return the in-memory data
                return _superbowls;
            }
        }

        private static List<string[]> _getLines()
        {
            var lines = new List<string[]>();
            try
            {
                var contents = File.ReadAllText(filename).Replace("\r","").Split(new [] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                lines = contents.Select(line => line.Split(',').ToArray()).ToList();

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }

            return lines;
        }
    }
}
