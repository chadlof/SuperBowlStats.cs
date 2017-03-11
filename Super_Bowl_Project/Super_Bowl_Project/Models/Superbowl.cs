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
        public int Number { get; set; }
        public int Attendance { get; set; }
        public IEnumerable<ITeam> Teams { get; set; }
        public string Mvp { get; set; }
        public string Stadium { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
