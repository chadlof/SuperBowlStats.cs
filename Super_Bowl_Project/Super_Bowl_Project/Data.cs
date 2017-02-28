using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Bowl_Project
{
    //public class Data
    //{
    //    string date;
    //    string superBowl;
    //    Int32 attendance;
    //    string winningQb;
    //    string winningCoach;
    //    string winner;
    //    Int32 winningPts;
    //    string losingQb;
    //    string losingCoach;
    //    string loser;
    //    Int32 losingPts;
    //    string mvp;
    //    string stadium;
    //    string city;
    //    string state;

    //    //constructter
    //    public Data(string date, string superBowl, Int32 attendance, string winningQb, string winningCoach, string winner, Int32 winningPts,
    //                               string losingQb, string losingCoach, string loser, Int32 losingPts, string mvp, string stadium, string city, string state)
    //    {
    //        this.date = date;
    //        this.superBowl = superBowl;
    //        this.attendance = attendance;
    //        this.winningQb = winningQb;
    //        this.winningCoach = winningCoach;
    //        this.winner = winner;
    //        this.winningPts = winningPts;
    //        this.losingQb = losingQb;
    //        this.losingCoach = losingCoach;
    //        this.loser = loser;
    //        this.losingPts = losingPts;
    //        this.mvp = mvp;
    //        this.stadium = stadium;
    //        this.city = city;
    //        this.state = state;
    //    }
    //    public string getDate()
    //    {
    //        return date;
    //    }
    //    public string getSuperBowl()
    //    {
    //        return superBowl;
    //    }
    //    public Int32 getAttendance()
    //    {
    //        return attendance;
    //    }
    //    public string getWinngQb()
    //    {
    //        return winningQb;
    //    }
    //    public string getWinningCoach()
    //    {
    //        return winningCoach;
    //    }
    //    public string getWinner()
    //    {
    //        return winner;
    //    }
    //    public Int32 getWinningPts()
    //    {
    //        return winningPts;
    //    }
    //    public string getLosinggQb()
    //    {
    //        return losingQb;
    //    }
    //    public string getLosingCoach()
    //    {
    //        return losingCoach;
    //    }
    //    public string getLoser()
    //    {
    //        return loser;
    //    }
    //    public Int32 getLosingPts()
    //    {
    //        return losingPts;
    //    }
    //    public string getMvp()
    //    {
    //        return mvp;
    //    }
    //    public string getStadium()
    //    {
    //        return stadium;
    //    }
    //    public string getCity()
    //    {
    //        return city;
    //    }
    //    public string getState()
    //    {
    //        return state;
    //    }


    //}// end of data class

    public class Data 
    {
        public string date;
        public string superBowl;
        public string attendance;
        public string winningQb;
        public string winningCoach;
        public string winner;
        public string winningPts;
        public string losingQb;
        public string losingCoach;
        public string loser;
        public string losingPts;
        public string mvp;
        public string stadium;
        public string city;
        public string state;

        //constructter
        public Data(string date, string superBowl, string attendance, string winningQb, string winningCoach, string winner, string winningPts,
                                   string losingQb, string losingCoach, string loser, string losingPts, string mvp, string stadium, string city, string state)
        {
            this.date = date;
            this.superBowl = superBowl;
            this.attendance = attendance;
            this.winningQb = winningQb;
            this.winningCoach = winningCoach;
            this.winner = winner;
            this.winningPts = winningPts;
            this.losingQb = losingQb;
            this.losingCoach = losingCoach;
            this.loser = loser;
            this.losingPts = losingPts;
            this.mvp = mvp;
            this.stadium = stadium;
            this.city = city;
            this.state = state;
        }

      
        

        public string getDate()
        {
            return date;
        }
        public string getSuperBowl()
        {
            return superBowl;
        }
        public string getAttendance()
        {
            return attendance;
        }
        public string getWinngQb()
        {
            return winningQb;
        }
        public string getWinningCoach()
        {
            return winningCoach;
        }
        public string getWinner()
        {
            return winner;
        }
        public string getWinningPts()
        {
            return winningPts;
        }
        public string getLosinggQb()
        {
            return losingQb;
        }
        public string getLosingCoach()
        {
            return losingCoach;
        }
        public string getLoser()
        {
            return loser;
        }
        public string getLosingPts()
        {
            return losingPts;
        }
        public string getMvp()
        {
            return mvp;
        }
        public string getStadium()
        {
            return stadium;
        }
        public string getCity()
        {
            return city;
        }
        public string getState()
        {
            return state;
        }

        public int CompareTo(Data other)
        {
            return this.state.CompareTo(other.state);

        }
        public class SortByState : IComparer<Data>
        {
            public int Compare(Data x, Data y)
            {
                return String.Compare(x.state, y.state);
            }
            //public static int StateValue(string state)
            //{
            //    if (state == "California") return 2;
            //    else if (state == "Florida") return 4;
            //    else if (state == "Louisiana") return 6;
            //    else return 8;
            //}

        }
        public class SortByMvp : IComparer<Data>
        {
            public int Compare(Data x, Data y)
            {
                return String.Compare(x.mvp, y.mvp);
            }
        }

        }// end of data class
}
