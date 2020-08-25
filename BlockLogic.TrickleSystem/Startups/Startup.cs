using BlockLogic.TrickleSystem;
using BlockLogic.TrickleSystem.Backers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BlockLogic.TrickleSystem
{
    public class Startup
    {
        public int ID { get; set; }
        [NotMapped]
        public List<Milestone> Milestones { get; set; }
        public string Name { get; set; }
        public double TotalPromised { get; set; }
        public double TotalReceived { get; set; }
        public string Description { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public List<Deposit> TotalDeposits { get; set; }
        [NotMapped]
        public List<FundAccount> BackedBy { get; set; }
        public string BTCAddress { get; set; }

        // add another member variable of FundAccounts so the startup can keep track of their backers and related information

        public Startup(int ID, List<Milestone> Milestones, string Name, float TotalPromised, string Description, string StartDate, DateTime CompletionDate, List<Deposit> TotalDeposits, string Email)
        {
            this.Milestones = Milestones;
            this.Name = Name;
            this.TotalPromised = TotalPromised;
            this.Description = Description;
            this.CompletionDate = CompletionDate;
            this.TotalDeposits = TotalDeposits;
            this.ID = ID;
            this.Email = Email;
        }

        public Startup(List<FundAccount> BackedBy, List<Milestone> Milestones, int ID, string Name, double TotalPromised, double TotalReceived, int StartupID, string Description, DateTime StartDate, DateTime CompletionDate, List<Deposit> TotalDeposits, string Email)
        {
            Milestones = new List<Milestone>();
            Name = "";
            TotalPromised = new double();
            TotalReceived = new double();
            Description = "";
            CompletionDate = DateTime.Now;
            Email = "";
            TotalDeposits = new List<Deposit>();
            BackedBy = new List<FundAccount>();
            ID = 0;
        }

        public Startup(int ID)
        {
            this.ID = ID;
        }

        public Startup()
        {
            Milestones = new List<Milestone>();
            Name = "";
            TotalPromised = new double();
            TotalReceived = new double();
            Description = "";
            CompletionDate = DateTime.Now;
            Email = "";
            TotalDeposits = new List<Deposit>();
            BackedBy = new List<FundAccount>();
            ID = 0;
        }

        public List<Deposit> GetDeposits()
        {
            return TotalDeposits;
        }

        public void AddDeposit(Deposit x)
        {
            TotalDeposits.Add(x);
        }
        public List<Milestone> GetMilestones()
        {
            return Milestones;
        }

        public void AddMilestone(Milestone mile)
        {
            Milestones.Add(mile);
        }

        public void AddMoneyReceived(float donation)
        {
            TotalReceived += donation;
        }

        public void AddMoneyPromised(float promised)
        {
            TotalPromised += promised;
        }

        public void ChangeCompletionDate(DateTime newDate)
        {
            CompletionDate = newDate;
        }

        public void ChangeDescription(string desc)
        {
            Description = desc;
        }
    }
}
