using System;
using System.Collections.Generic;
using System.Text;

namespace BlockLogic.TrickleSystem
{
    public class Deposit
    {
        public double Amount { get; set; }
        public int BackerID { get; set; }
        public int StartupID { get; set; }
        public DateTime Date { get; set; }
        public int ID { get; set; }

        public Deposit(double Amount, Backer From, DateTime Date)
        {
            this.Amount = Amount;
            this.BackerID = BackerID;
            this.Date = Date;
        }

        public Deposit()
        {
            Amount = new double();
            BackerID = 0;
            StartupID = 0;
            ID = 0;
            Date = DateTime.UtcNow;
        }
    }
}
