using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project.Models
{
    interface ISuperbowl
    {
        DateTime Date { get; set; }
        int Number { get; set; }
        int Attendance { get; set; }
        IEnumerable<ITeam> Teams { get; set;}
        string LosingQb { get; set; }
        string LosingCoach { get; set; }
        string Loser { get; set; }
        string LosingPts { get; set; }
        string Mvp { get; set; }
        string Stadium { get; set; }
        string City { get; set; }
        string State { get; set; }
    }
}
