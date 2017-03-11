using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project.Models
{
    public class Team : ITeam
    {
        public string Quarterback { get; set; }
        public string Coach { get; set; }
        public bool DidWin { get; set; }
        public string PointsScored { get; set; }
    }
}
