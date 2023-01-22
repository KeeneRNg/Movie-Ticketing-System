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
    abstract class Ticket
    {
        // property
        public Screening Screening { get; set; }

        // constructors(to be used in subclasses)
        public Ticket()
        {

        }

        public Ticket(Screening s)
        {
            Screening = s;
        }

        // methods
        public abstract double CalculatePrice();

        public override string ToString()
        {
            return "Screening: " + Screening;
        }
    }
}
