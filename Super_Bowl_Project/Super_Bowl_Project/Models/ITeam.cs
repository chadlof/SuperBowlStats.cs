using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project.Models
{
    public interface ITeam
    {
        string Name { get; set; }
        string Quarterback { get; set; }
        string Coach { get; set; }
        string PointsScored { get; set; }
    }
}
