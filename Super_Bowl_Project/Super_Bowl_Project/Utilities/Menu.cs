using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project.Utilities
{
    public static class Menu
    {
        public static void HandleChoice()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1. Display CSV File Contents");
            Console.WriteLine("2. Show Winners");
            Console.WriteLine("3. Show 5 most attended superbowls");
            Console.WriteLine("4. Show superbowls hosted by state");
            Console.WriteLine("5. Players with most MVPs");
            Console.WriteLine("6. Trivia");
            Console.WriteLine("7. All the things!");
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    DisplayContents();
                    break;

                case ConsoleKey.D2:
                    ShowWinners();
                    break;

                case ConsoleKey.D3:
                    MostAttended();
                    break;

                case ConsoleKey.D4:
                    StatesWithMostSuperbowls();
                    break;

                case ConsoleKey.D5:
                    PlayersWithMostMVPs();
                    break;

                case ConsoleKey.D6:
                    Trivia();
                    break;

                case ConsoleKey.D7:
                    DisplayContents();
                    ShowWinners();
                    MostAttended();
                    StatesWithMostSuperbowls();
                    PlayersWithMostMVPs();
                    Trivia();
                    break;

                default:
                    break;
            }
        }

        public static void DisplayContents()
        {
            Console.WriteLine();
            var superbowls = DataAccessLayer.Superbowls;
            foreach (var superbowl in superbowls)
            {
                Console.WriteLine(String.Join(", ", superbowl.OriginalValues));
            }
        }

        public static void ShowWinners()
        {
            Console.WriteLine();
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

        public static void MostAttended()
        {
            Console.WriteLine();
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

        public static void StatesWithMostSuperbowls()
        {
            Console.WriteLine();
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

        public static void PlayersWithMostMVPs()
        {
            Console.WriteLine();
            Console.WriteLine("Most MVPs");
            Console.WriteLine("Player Name | Team Name | Losing Team Name");
            Console.WriteLine("======================================================================");
            Console.WriteLine();

            // Trim list down to just MVPs and their count if greater than 2
            var mvps = DataAccessLayer.Superbowls
                .GroupBy(x => x.Mvp, (key, g) => new { Mvp = key, Entries = g.ToList() })
                .Where(x => x.Entries.Count() >= 2)
                .OrderByDescending(x => x.Entries.Count());


            foreach (var entry in mvps)
            {
                //Get count of superbowls held in this city + state + stadium combination
                var superbowls = DataAccessLayer.Superbowls.Where(x => x.Mvp == entry.Mvp);

                foreach (var superbowl in superbowls)
                {
                    var line = String.Format("{0} | {1} | {2}", entry.Mvp, superbowl.WinningTeam.Name, superbowl.LosingTeam.Name);
                    Console.WriteLine(line);
                }
            }
        }

        public static void Trivia()
        {
            Console.WriteLine();
            var losingestCoach = DataAccessLayer.Superbowls
                .GroupBy(x => x.LosingTeam.Coach, (key, g) => new { Name = key, Entries = g.ToList() })
                .OrderByDescending(x => x.Entries.Count())
                .First();

            Console.WriteLine("Which coach lost the most super bowls? -> {0}, {1}", losingestCoach.Name, losingestCoach.Entries.Count);
            Console.WriteLine();

            var winningestCoach = DataAccessLayer.Superbowls
                .GroupBy(x => x.WinningTeam.Coach, (key, g) => new { Name = key, Entries = g.ToList() })
                .OrderByDescending(x => x.Entries.Count())
                .First();

            Console.WriteLine("Which coach won the most super bowls? -> {0}, {1}", winningestCoach.Name, winningestCoach.Entries.Count);
            Console.WriteLine();

            var losingestTeam = DataAccessLayer.Superbowls
                .GroupBy(x => x.LosingTeam.Name, (key, g) => new { Name = key, Entries = g.ToList() })
                .OrderByDescending(x => x.Entries.Count())
                .First();

            Console.WriteLine("Which team lost the most super bowls? -> {0}, {1}", losingestTeam.Name, losingestTeam.Entries.Count);
            Console.WriteLine();

            var winningestTeam = DataAccessLayer.Superbowls
                .GroupBy(x => x.WinningTeam.Name, (key, g) => new { Name = key, Entries = g.ToList() })
                .OrderByDescending(x => x.Entries.Count())
                .First();

            Console.WriteLine("Which team won the most super bowls? -> {0}, {1}", winningestTeam.Name, winningestTeam.Entries.Count);
            Console.WriteLine();

            var greatestSpread = DataAccessLayer.Superbowls
                .Select(x => new { Numeral = x.Numeral, Spread = x.WinningTeam.PointsScored - x.LosingTeam.PointsScored })
                .OrderByDescending(x => x.Spread)
                .First();

            Console.WriteLine("Which Super bowl had the greatest point difference? -> {0}, {1}", greatestSpread.Numeral, greatestSpread.Spread);
            Console.WriteLine();

            var averageAttendance = DataAccessLayer.Superbowls.Average(x => x.Attendance);

            Console.WriteLine("What has been the average attendance of all super bowls? -> {0}", averageAttendance);
            Console.WriteLine();

        }
    }
}
