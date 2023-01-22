//============================================================
// Student Number : S10222177, S10218985
// Student Name : Goh Tian Le Matthew, Keene Ng
// Module Group : T08
//============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T08_Team1
{
    class Screening : IComparable<Screening>
    {
        // properties
        public int ScreeningNo { get; set; }
        public DateTime ScreeningDateTime { get; set; }
        public string ScreeningType { get; set; }
        public int SeatsRemaining { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }

        // constructors
        public Screening() { }
        public Screening(int sno, DateTime sdt, string st, int sr, Cinema c, Movie m)
        {
            ScreeningNo = sno;
            ScreeningDateTime = sdt;
            ScreeningType = st;
            SeatsRemaining = sr;
            Cinema = c;
            Movie = m;
        }

        // methods
        public override string ToString()
        {
            return "ScreeningNo: " + ScreeningNo +
                "\tScreeningDateTime: " + ScreeningDateTime +
                "\tScreenType: " + ScreeningType +
                "\tSeatsRemaning: " + SeatsRemaining +
                "\tCinema: " + Cinema +
                "\tMovie: " + Movie;
        }

        // implement the method (in the interface)
        public int CompareTo(Screening s) // sort by remaining seats
        {
            if (SeatsRemaining > s.SeatsRemaining)
                return -1;
            else if (SeatsRemaining == s.SeatsRemaining)
                return 0;
            else
                return 1;
        }

    }
}
