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
    class Order
    {
        // properties
        public int OrderNo { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public List<Ticket> TicketList { get; set; }

        // contructors
        public Order() { }
        public Order(int ono, DateTime odt)
        {
            OrderNo = ono;
            OrderDateTime = odt;
        }

        // methods
        public void AddTicket(Ticket ticket)
        {
            TicketList.Add(ticket);
        }
        public override string ToString()
        {
            return "OrderNo: " + OrderNo +
                "\tOrderDateTime: " + OrderDateTime +
                "\tAmount: " + Amount +
                "\tStatus: " + Status;
        }
    }
}
