using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project.Models
{
    public interface ISuperbowl
    {
        DateTime Date { get; set; }
        string Numeral { get; set; }
        int Attendance { get; set; }
        ITeam WinningTeam { get; set; }
        ITeam LosingTeam { get; set; }
        string Mvp { get; set; }
        string Stadium { get; set; }
        string City { get; set; }
        string State { get; set; }
        string[] OriginalValues { get; }
    }
}
