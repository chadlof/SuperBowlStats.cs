using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project.Models
{
    interface ITeam
    {
        string Quarterback { get; set; }
        string Coach { get; set; }
        bool DidWin { get; set; }
        string PointsScored { get; set; }
    }
}
