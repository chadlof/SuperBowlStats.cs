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

                                handleChoice();
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
                    handleChoice();
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

        public static void handleChoice()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1. Display CSV File Contents");
            Console.WriteLine("2. Show Winners");
            Console.WriteLine("3. Show 5 most attended superbowls");
            Console.WriteLine("4. Show superbowls hosted by state");
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    displayContents();
                    break;

                case ConsoleKey.D2:
                    showWinners();
                    break;

                case ConsoleKey.D3:
                    mostAttended();
                    break;

                case ConsoleKey.D4:
                    statesWithMostSuperbowls();
                    break;

                default:
                    break;
            }
        }

        public static void displayContents()
        {
            var superbowls = DataAccessLayer.Superbowls;
            foreach (var superbowl in superbowls)
            {
                Console.WriteLine(String.Join(", ", superbowl.OriginalValues));
            }
        }

        public static void showWinners()
        {
            Console.WriteLine("Superbowl Winners");
            Console.WriteLine("Superbowl | Team Name | Year | Quarterback | Coach | MVP Name | Spread");
            Console.WriteLine("======================================================================");
            Console.WriteLine();
            var superbowls = DataAccessLayer.Superbowls;
            foreach (var superbowl in superbowls)
            {
                var winner = superbowl.WinningTeam;
                var year = superbowl.Date.Year;
                var spread = winner.PointsScored - superbowl.LosingTeam.PointsScored;
                var line = String.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6}", superbowl.Numeral, winner.Name, year, winner.Quarterback, winner.Coach, superbowl.Mvp, spread);
                Console.WriteLine(line);
            }
        }

        public static void mostAttended()
        {
            Console.WriteLine("Top 5 most attended Superbowl Winners");
            Console.WriteLine("Year | Winning Team | Losing Team | City | State | Stadium");
            Console.WriteLine("======================================================================");
            Console.WriteLine();
            var superbowls = DataAccessLayer.Superbowls.OrderByDescending(x => x.Attendance).Take(5);
            foreach (var superbowl in superbowls)
            {
                var winner = superbowl.WinningTeam;
                var loser = superbowl.LosingTeam;
                var year = superbowl.Date.Year;
                var line = String.Format("{0} | {1} | {2} | {3} | {4} | {5}", year, winner.Name, loser.Name, superbowl.City, superbowl.State, superbowl.Stadium);
                Console.WriteLine(line);
            }
        }

        public static void statesWithMostSuperbowls()
        {
            Console.WriteLine("Most Superbowls By City, State, and Stadium");
            Console.WriteLine("City | State | Stadium | # of Superbowls");
            Console.WriteLine("======================================================================");
            Console.WriteLine();

            // Trim list down to just State
            var superbowlStates = DataAccessLayer.Superbowls.Select(x => new { State = x.State });

            // Eliminate duplicate states
            superbowlStates = superbowlStates.Distinct();

            foreach (var entry in superbowlStates)
            {
                //Get count of superbowls held in this city + state + stadium combination
                var superbowls = DataAccessLayer.Superbowls.Where(x => x.State == entry.State);

                // Get first superbowl so we can grab the city and stadium
                var market = superbowls.Select(x => new { City = x.City, Stadium = x.Stadium }).First();

                var line = String.Format("{0} | {1} | {2} | {3}", market.City, entry.State, market.Stadium, superbowls.Count());
                Console.WriteLine(line);
            }
        }
       


        //public static void diplayFile()
        //{
        //    //DECLARATIONS
        //    string[] data;
        //    char DELIM = ',';
        //    string line;
        //    //const string FILE = "C:/Users/lofchaa/Documents/+Spring2017/Advanced Programming/Projects/Project2/Super_Bowl_Project/Super_Bowl_Project_Fixed.csv";
        //    FileStream input = new FileStream("Super_Bowl_Project_Fixed.csv", FileMode.Open, FileAccess.Read);
        //    StreamReader file = new StreamReader(input);
        //    var superbowls = new List<Superbowl>();
        //    line = file.ReadLine();




        //    bool flag = false;
        //    while (!flag)
        //    {
        //        try
        //        {
        //            Console.WriteLine("\tEnter the file path you would like the diplayFile() method written to.");
        //            string file2 = Console.ReadLine();
        //            //C:/Users/lofchaa/Documents/+Spring2017/Advanced Programming/Projects/Project2/APProject2Test.txt
        //            var Output = new FileStream(file2, FileMode.Create, FileAccess.Write);
        //            var Writer = new StreamWriter(Output);







        //            Writer.WriteLine("\n                                                                     **************************************     ");
        //            Writer.WriteLine("                                                                     **  Super Bowl Stats chart upto LI  **      ");
        //            Writer.WriteLine("                                                                     **************************************     \n");

        //        file.ReadLine(); //Skip first line - CSV data headers
        //        while (line != null)


        //        {
        //            data = line.Split(DELIM);

                
        //            aData.Add(new Data(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7],
        //                                data[8], data[9], data[10], data[11], data[12], data[13], data[14]));


        //            Writer.WriteLine("{0,-12} {1,-7} {2,-12} {3,-28} {4,-18} {5,-25} {6,-15} {7,-30} {8,-20} {9,-20} {10,-20} {11,-27} {12,-30} {13,-20} {14,-20}",
        //                                data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14]);
             

        //            line = file.ReadLine();
        //        }

           
        //            flag = true;
        //            Writer.Close();
        //        }//end try
        //        //catch (Exception e)
        //        //{
        //        //    Console.WriteLine(e.Message);
        //        //}
        //        catch (IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //            Console.WriteLine();
        //        }
        //        catch (ArgumentException e)
        //        {
        //            Console.WriteLine(e.Message);
        //            Console.WriteLine();
        //        }

        //    }//end while
        //    Console.WriteLine("\n\tThe method has been written to your file!");

        //    file.Close();
          
        //}//end of displayFile

        //public static void superBowlWinners()
        //{
        //    //DECLARATIONS
        //    string[] data;
        //    char DELIM = ',';
        //    string line;
        //    const string FILE = "Super_Bowl_Project_Fixed.csv";
        //    FileStream input = new FileStream(FILE, FileMode.Open, FileAccess.Read);
        //    StreamReader file = new StreamReader(input);
        //    List<Data> aData = new List<Data>();

        //    bool flag = false;
        //    while (!flag)
        //    {
        //        try
        //        {
        //            Console.WriteLine("\n\tEnter the file path you would like the superBowlWinners() method written to.");
        //            string file2 = Console.ReadLine();
        //            //string file2 = "C:/Users/lofchaa/Documents/+Spring2017/Advanced Programming/Projects/Project2/APProject2Test.txt";
        //            FileStream output = new FileStream(file2, FileMode.Create, FileAccess.Write);
        //            StreamWriter write = new StreamWriter(output);


        //            write.WriteLine("\n                                                                       *******************************    ");
        //            write.WriteLine("                                                                       **  Super Bowl Winner Stats  **      ");
        //            write.WriteLine("                                                                       *******************************     \n");
        //            write.WriteLine("\t{0,-25} {1,-20} {2,-30} {3,-28} {4,-30} {5,-25}\n","Team", "Year","Winning QB","Winning Coach","MVP","Point Diffrence");
        //            file.ReadLine();
        //            line = file.ReadLine();


        //            while (line != null)


        //            {
        //                data = line.Split(DELIM);


        //                aData.Add(new Data(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7],
        //                                    data[8], data[9], data[10], data[11], data[12], data[13], data[14]));
        //                write.WriteLine("\t{0,-25} {1,-20} {2,-30} {3,-28} {4,-30} {5,-25}", data[5], data[0], data[3], data[5], data[11], Convert.ToString(Convert.ToInt32(data[6]) - Convert.ToInt32(data[10])));

        //                line = file.ReadLine();
        //            }
        //            flag = true;
        //            write.Close();
        //        }//end try
        //        catch (IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        catch (ArgumentException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //    }//end while
        //    file.Close();
        //    Console.WriteLine("\n\tThe method has been written to your file!");
        //}//end of supedrBowlWinners

        //public static void top5attendance()
        //{
        //    //DECLARATIONS
        //    string[] data;
        //    char DELIM = ',';
        //    //int count = 1;
        //    string line;
        //    const string FILE = "Super_Bowl_Project_Fixed.csv";
        //    FileStream input = new FileStream(FILE, FileMode.Open, FileAccess.Read);
        //    StreamReader file = new StreamReader(input);
        //    List<Data> aData = new List<Data>();

        //    bool flag = false;
        //    while (!flag)
        //    {
        //        try
        //        {
        //            Console.WriteLine("\n\tEnter the file path you would like the top5attendance() method written to.");
        //            string file2 = Console.ReadLine();
        //            //string file2 = "C:/Users/lofchaa/Documents/+Spring2017/Advanced Programming/Projects/Project2/APProject2Test.txt";
        //            FileStream output = new FileStream(file2, FileMode.Create, FileAccess.Write);
        //            StreamWriter write = new StreamWriter(output);

        //            write.WriteLine("\n                                                                   ****************************    ");
        //            write.WriteLine("                                                                   **  Top Five Attendances  **      ");
        //            write.WriteLine("                                                                   ****************************     \n");



        //            write.WriteLine("\t{0,-10} {1,-25} {2,-25} {3,-25} {4,-25} {5,-25}\n", "Year", "Winning Team", "losing Team", "City", "State", "Stadium");



        //    file.ReadLine();
        //    line = file.ReadLine();


        //    while (line != null)


        //    {
        //        data = line.Split(DELIM);


               
        //        aData.Add(new Data(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7],
        //                            data[8], data[9], data[10], data[11], data[12], data[13], data[14]));

        //        if (Convert.ToInt32(data[02]) > 100000)
        //        {
        //            write.WriteLine("\t{0,-10} {1,-25} {2,-25} {3,-25} {4,-25} {5,-25}", data[1], data[5], data[9], data[13], data[14], data[12]);
        //        }
        //        line = file.ReadLine();
        //    }
        //            flag = true;
        //            write.Close();
        //        }//end try
        //        catch (IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        catch (ArgumentException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //    }//end while
        //    file.Close();
        //    Console.WriteLine("\n\tThe method has been written to your file!");
        //}//end of top5attendance
        //public static void mostSuperBowls()
        //{
        //    //DECLARATIONS
           
        //    string[] data;
        //    char DELIM = ',';
        //    string line;
        //    const string FILE = "Super_Bowl_Project_Fixed.csv";
        //    FileStream input = new FileStream(FILE, FileMode.Open, FileAccess.Read);
        //    StreamReader file = new StreamReader(input);
        //    List<Data> aData = new List<Data>();

        //    bool flag = false;
        //    while (!flag)
        //    {
        //        try
        //        {
        //            Console.WriteLine("\n\tEnter the file path you would like the mostSuperBowls() method written to.");
        //            string file2 = Console.ReadLine();
        //            //string file2 = "C:/Users/lofchaa/Documents/+Spring2017/Advanced Programming/Projects/Project2/APProject2Test.txt";
        //            FileStream output = new FileStream(file2, FileMode.Create, FileAccess.Write);
        //            StreamWriter write = new StreamWriter(output);


        //            write.WriteLine("\n                                                                     ************************    ");
        //            write.WriteLine("                                                                     **  Most Super Bowls  **      ");
        //            write.WriteLine("                                                                     ************************     \n");




        //    file.ReadLine();
        //    line = file.ReadLine();


        //    while (line != null)


        //    {
        //        data = line.Split(DELIM);
        //        aData.Add(new Data(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7],
        //                            data[8], data[9], data[10], data[11], data[12], data[13], data[14]));

                
               
        //        line = file.ReadLine();
        //    }

        //            //aData.Sort(new Data.SortByState());//Sorts by state
        //            //Console.WriteLine("\t{0,-25} {1,-25} {2,-25}\n", "City", "State", "Stadium");
        //            //foreach (Data x in aData)
        //            //{

        //            //        Console.WriteLine("\t{0,-25} {1,-25} {2,-25}", x.getCity(), x.getState(), x.getStadium());

        //            //}

        //    write.WriteLine("\t\tMost Super Bowls");
        //    write.WriteLine("\t{0,-25} {1,-25} {2,-25}\n", "City", "State", "Stadium");
        //    foreach (Data x in aData)
        //    {
        //        if (x.getState() == "Florida")
        //        {
        //           write.WriteLine("\t{0,-25} {1,-25} {2,-25}", x.city, x.getState(), x.getStadium());
        //        }
        //    }

        //    write.WriteLine("\n\t\t2nd Most Super Bowls");
        //    write.WriteLine("\t{0,-25} {1,-25} {2,-25}\n", "City", "State", "Stadium");
        //    foreach (Data x in aData)
        //    {
        //        if (x.getState() == "California")
        //        {
        //          write.WriteLine("\t{0,-25} {1,-25} {2,-25}", x.getCity(), x.getState(), x.getStadium());
        //        }
        //    }

        //    write.WriteLine("\n\t\t3rd Most Super Bowls");
        //    write.WriteLine("\t{0,-25} {1,-25} {2,-25}\n", "City", "State", "Stadium");
        //    foreach (Data x in aData)
        //    {
        //        if (x.getState() == "Louisiana")
        //        {
        //           write.WriteLine("\t{0,-25} {1,-25} {2,-25}", x.getCity(), x.getState(), x.getStadium());
        //        }
        //    }

        //    write.WriteLine("\n\t\t4th Most Super Bowls");
        //    write.WriteLine("\t{0,-25} {1,-25} {2,-25}\n", "City", "State", "Stadium");
        //    foreach (Data x in aData)
        //    {
        //        if (x.getState() == "Texas")
        //        {
        //                    write.WriteLine("\t{0,-25} {1,-25} {2,-25}", x.getCity(), x.getState(), x.getStadium());
        //        }
        //    }

        //    write.WriteLine("\n\t\t5th Most Super Bowls");
        //    write.WriteLine("\t{0,-25} {1,-25} {2,-25}\n", "City", "State", "Stadium");
        //    foreach (Data x in aData)
        //    {
        //        if (x.getState() == "Arizona")
        //        {
        //          write.WriteLine("\t{0,-25} {1,-25} {2,-25}", x.getCity(), x.getState(), x.getStadium());
        //        }
        //    }

        //            flag = true;
        //            write.Close();
        //        }//end try
        //        catch (IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        catch (ArgumentException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //    }//end while
        //    file.Close();
        //    Console.WriteLine("\n\tThe method has been written to your file!");
        //}//end of mostSuperBowls
        //public static void twoMVP()
        //{
        //    //DECLARATIONS

        //    string[] data;
        //    char DELIM = ',';
        //    string line;
        //    const string FILE = "Super_Bowl_Project_Fixed.csv";
        //    FileStream input = new FileStream(FILE, FileMode.Open, FileAccess.Read);
        //    StreamReader file = new StreamReader(input);
        //    List<Data> aData = new List<Data>();

        //    bool flag = false;
        //    while (!flag)
        //    {
        //        try
        //        {
        //            Console.WriteLine("\n\tEnter the file path you would like the twoMVP() method written to.");
        //            string file2 = Console.ReadLine();
        //            //string file2 = "C:/Users/lofchaa/Documents/+Spring2017/Advanced Programming/Projects/Project2/APProject2Test.txt";
        //            FileStream output = new FileStream(file2, FileMode.Create, FileAccess.Write);
        //            StreamWriter write = new StreamWriter(output);


        //            write.WriteLine("\n                                                                   *************************    ");
        //            write.WriteLine("                                                                   **  Two or More MVP's  **      ");
        //            write.WriteLine("                                                                   *************************     \n");


      



        //    file.ReadLine();
        //    line = file.ReadLine();


        //    while (line != null)


        //    {
        //        data = line.Split(DELIM);
        //        aData.Add(new Data(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7],
        //                            data[8], data[9], data[10], data[11], data[12], data[13], data[14]));

        //        line = file.ReadLine();
        //    }
        //    Dictionary<string, int> numMvp = new Dictionary<string, int>();
        //    foreach (Data x in aData)
        //    {
        //        if (numMvp.ContainsKey(x.mvp))
        //        {
        //            numMvp[x.mvp]++;
        //        }
        //        else
        //        {
        //            numMvp[x.mvp] = 1;
        //        }
        //    }
        //    write.WriteLine("\t{0,-25} {1,-25} {2,-25}\n", "Name", "Winning Team", "Losing Team");
        //    aData.Sort(new Data.SortByMvp());//Sorts by Mvp
        //    foreach (KeyValuePair<string, int> mvp in numMvp.OrderByDescending(x => x.Value))
        //    {
        //        foreach (Data x in aData)
        //        {
        //            if(mvp.Value > 2 && mvp.Key == x.getMvp())


        //                write.WriteLine("\t{0,-25} {1,-25} {2,-25}", x.getMvp(), x.getWinner(), x.getLoser());

        //        }
        //    }
        //            flag = true;
        //            write.Close();
        //        }//end try
        //        catch (IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        catch (ArgumentException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //    }//end while
        //    file.Close();
        //    Console.WriteLine("\n\tThe method has been written to your file!");
        //}//end of twoMVP
        //public static void multiQeury()
        //{
        //    //DECLARATIONS

        //    string[] data;
        //    char DELIM = ',';
        //    string line;
        //    const string FILE = "Super_Bowl_Project_Fixed.csv";
        //    FileStream input = new FileStream(FILE, FileMode.Open, FileAccess.Read);
        //    StreamReader file = new StreamReader(input);
        //    List<Data> aData = new List<Data>();

        //    bool flag = false;
        //    while (!flag)
        //    {
        //        try
        //        {
        //            Console.WriteLine("\n\tEnter the file path you would like the multiQeury() method written to.");
        //            string file2 = Console.ReadLine();
        //            //string file2 = "C:/Users/lofchaa/Documents/+Spring2017/Advanced Programming/Projects/Project2/APProject2Test.txt";
        //            FileStream output = new FileStream(file2, FileMode.Create, FileAccess.Write);
        //            StreamWriter write = new StreamWriter(output);


                    
        //            write.WriteLine("\n                                                                   ***********************    ");
        //            write.WriteLine("                                                                   **  Final Jeopardy!  **      ");
        //            write.WriteLine("                                                                   ***********************     \n");





        //            file.ReadLine();
        //    line = file.ReadLine();


        //    while (line != null)


        //    {
        //        data = line.Split(DELIM);
        //        aData.Add(new Data(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7],
        //                            data[8], data[9], data[10], data[11], data[12], data[13], data[14]));

        //        line = file.ReadLine();
        //    }

        //    write.WriteLine("\tWhich coach lost the most super bowls?");
        //            write.WriteLine();
        //            //Create New List of only coaches and the number of times they lost a superbowl
        //            Dictionary<string, int> loseCoach = new Dictionary<string, int>();

        //    //Iterate over the source data list
        //    foreach (Data x in aData)
        //    {
        //        //Make sure we haven't already added the coach to the new dictionary
        //        if (loseCoach.ContainsKey(x.losingCoach) == false)
        //        {
        //            //Get the total number of losses for this coach
        //            int numberOfLosses = aData.Count(y => y.losingCoach == x.losingCoach);
        //            //Add the coach and their total loss count to the dictionary
        //            loseCoach.Add(x.losingCoach, numberOfLosses);
        //        }
        //    }

        //    //Find the name of the coach with the largest number of losses
        //    var biggestLoserName = loseCoach.Max(x => x.Key);
        //    //Find the number of loses the coach has
        //    var biggestLoserNum = loseCoach.Max(x => x.Value);

        //    //Write the coaches name out to the console
        //    write.WriteLine("\n\t--{0} has lose the most Super bowls with losing {1} times", biggestLoserName,biggestLoserNum);
        //            write.WriteLine();
        //            write.WriteLine();

        //            write.WriteLine("\n\n\tWhich coach won the most super bowls?");
        //            write.WriteLine();
        //            Dictionary<string, int> winCoach = new Dictionary<string, int>();
        //    foreach (Data x in aData)
        //    { 
        //    if (winCoach.ContainsKey(x.winningCoach) == false)
        //        {
        //            int numWins = aData.Count(y => y.winningCoach == x.winningCoach);
        //            winCoach.Add(x.winningCoach, numWins);
        //        }

        //    }
        //    var biggestWinName = winCoach.Max(x => x.Key);
        //    var biggestWinNum = winCoach.Max(x => x.Value);

        //    write.WriteLine("\n\t--{0} has won the most Super bowls with winning {1} times", biggestWinName,biggestWinNum);
        //            write.WriteLine();
        //            write.WriteLine();

        //            write.WriteLine("\n\n\tWhich team won the most super bowls?");
        //            write.WriteLine();
        //            Dictionary<string, int> winTeam = new Dictionary<string, int>();
        //    foreach (Data x in aData)
        //    {
        //        if (winTeam.ContainsKey(x.winner) == false)
        //        {
        //            int numWins = aData.Count(y => y.winner == x.winner);
        //            winTeam.Add(x.winner, numWins);
        //        }

        //    }
        //    var biggestWinName2 = winTeam.Max(x => x.Key);
        //    var biggestWinNum2 = winTeam.Max(x => x.Value);
        //    write.WriteLine("\n\t--{0} have won the most Super bowls with winning {1} times", biggestWinName2, biggestWinNum2);
        //            write.WriteLine();
        //            write.WriteLine();

        //            write.WriteLine("\n\n\tWhich team lost the most super bowls?");
        //            write.WriteLine();
        //            Dictionary<string, int> loseTeam = new Dictionary<string, int>();
        //    foreach (Data x in aData)
        //    {
        //        if (loseTeam.ContainsKey(x.loser) == false)
        //        {
        //            int numberOfLosses = aData.Count(y => y.loser == x.loser);
        //            loseTeam.Add(x.loser, numberOfLosses);
        //        }
        //    }

        //    var biggestLoserName2 = loseTeam.Max(x => x.Key);
        //    var biggestLoserNum2 = loseTeam.Max(x => x.Value);
        //    write.WriteLine("\n\t--{0} have lose the most Super bowls with losing {1} times",biggestLoserName2,biggestLoserNum2);
        //            write.WriteLine();
        //            write.WriteLine();

        //            write.WriteLine("\n\n\tWhich Super bowl had the greatest point difference?");
        //            write.WriteLine();
        //            Int32 ptDiff = 0;
        //    Int32 high = 0;
        //    foreach (Data x in aData)
        //    {
        //        ptDiff = Convert.ToInt32(x.getWinningPts()) - Convert.ToInt32(x.getLosingPts());
        //        if (ptDiff > high)
        //        {
        //            high = ptDiff;
        //        }
        //    }

        //    foreach (Data x in aData)
        //    {
        //        ptDiff = Convert.ToInt32(x.getWinningPts()) - Convert.ToInt32(x.getLosingPts());
        //        if (ptDiff == high)
        //        {
        //             write.WriteLine("\n\t--Super Bowl {0} had the largest point difference. The winning points are: {1} and the losing points are: {2} with a difference of: {3} points", x.getSuperBowl(), x.getWinningPts(), x.getLosingPts(), ptDiff);
        //                    write.WriteLine();
        //                    write.WriteLine();
        //                }

        //    }


        //    write.WriteLine("\n\n\tWhat has been the average attendance of all super bowls?");
        //            Console.WriteLine();
        //    Int32 totalAttendance = 0;
        //    int count = 0;
        //             foreach (Data x in aData)
        //    {
        //        totalAttendance =  totalAttendance + Convert.ToInt32(x.getAttendance());
        //        count++;
        //    }
        //    write.WriteLine("\n\t--Number of super bowls is: {0} With a Total attendance of: {1:N} And a average of: {2:N}",count,totalAttendance, totalAttendance/count);
        //            write.WriteLine();
        //            write.WriteLine();

        //            flag = true;
        //            write.Close();
        //        }//end try
        //        catch (IOException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //        catch (ArgumentException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //    }//end while
        //    file.Close();
        //    Console.WriteLine("\n\tThe method has been written to your file!");
        //}//end of multiQuery


    }//end of program class
     
}//end of namespace
