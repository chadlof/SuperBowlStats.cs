using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project.Models
{
    public class Superbowl : ISuperbowl
    {
        public DateTime Date { get; set; }
        public string Numeral { get; set; }
        public int Attendance { get; set; }
        public ITeam WinningTeam { get; set; }
        public ITeam LosingTeam { get; set; }
        public string Mvp { get; set; }
        public string Stadium { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string[] OriginalValues { get; private set; }

        public static ISuperbowl Create(string[] values)
        {
            var superbowl = new Superbowl
            {
                Date = DateTime.Parse(values[0]),
                Numeral = values[1],
                Attendance = Int32.Parse(values[2]),
                WinningTeam = new Team {
                    Quarterback = values[3],
                    Coach = values[4],
                    Name = values[5],
                    PointsScored = Int32.Parse(values[6]),
                },
                LosingTeam = new Team {
                    Quarterback = values[7],
                    Coach = values[8],
                    Name = values[9],
                    PointsScored = Int32.Parse(values[10]),
                },
                Mvp = values[11],
                Stadium = values[12],
                City = values[13],
                State = values[14],
                OriginalValues = values,
            };

            return superbowl;
        }
    }
}
