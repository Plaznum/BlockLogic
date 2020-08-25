using System;
using System.Collections.Generic;
using System.Text;

namespace BlockLogic.TrickleSystem.Backers
{
    public class FundAccount
    {
        public int ID { get; set; }
        public string TrickleType { get; set; } // this will need to be changed to 2 objects, one for Milestone and one for Standard, to keep track of how much $ is being trickled per milestone (for Milestone Trickle) and per day for Standard Trickle
        public int StartupID { get; set; }
        public int BackerID { get; set; }
        public double TotalMoney { get; set; }
        public double TrickleAmount { get; set; }
        public bool hasCapped { get; set; }
        public double TrickleCap { get; set; }
        public double TotalDonated { get; set; }
        public double Remainder { get; set; }

        public FundAccount(int ID, string TrickleType, double TrickleAmount, int StartupID, int BackerID, double TotalMoney, int TrickleCap, double TotalDonated, double Remainder, bool hasCapped)
        {
            this.TrickleType = TrickleType; // Milestone or Standard
            this.StartupID = StartupID;
            this.BackerID = BackerID;
            this.TrickleAmount = TrickleAmount;
            this.ID = ID;
            this.TotalMoney = TotalMoney;
            this.TrickleCap = TrickleCap;
            this.TotalDonated = TotalDonated;
            this.Remainder = Remainder;
            this.hasCapped = hasCapped;
        }
        public FundAccount()
        {
            StartupID = 0;
            BackerID = 0;
            TrickleAmount = new double();
            TrickleType = "";
        }

        public string GetTrickleType()
        {
            return TrickleType;
        }
    }
}
