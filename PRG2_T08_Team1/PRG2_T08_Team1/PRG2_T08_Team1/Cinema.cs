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
    class Cinema
    {
        // properties
        public string Name { get; set; }
        public int HallNo { get; set; }
        public int Capacity { get; set; }

        // constructors
        public Cinema() { }
        public Cinema(string n, int hno, int cap)
        {
            Name = n;
            HallNo = hno;
            Capacity = cap;
        }

        // methods
        public override string ToString()
        {
            return "Name: " + Name +
                "\tHallNo: " + HallNo +
                "\tCapacity: " + Capacity;
        }
    }
}
