using System;
namespace BlockLogic.TrickleSystem
{
    public class Milestone
    {
        public int StartupID { get; set; }
        public bool Fulfilled { get; set; }
        public bool FulfilledCompleted { get; set; }
        public string Description { get; set; }
        public DateTime PromisedDate { get; set; }
        public DateTime StartDate { get; set; }
        public int ID { get; set; }

        public Milestone(int StartupID, bool Fulfilled, string Description, DateTime StartDate, DateTime PromisedDate, bool FulfilledCompleted)
        {
            this.StartupID = StartupID;
            this.Fulfilled = Fulfilled;
            this.Description = Description;
            this.StartDate = StartDate;
            this.PromisedDate = PromisedDate;
            this.FulfilledCompleted = FulfilledCompleted;
        }

        public void CompleteMilestone()
        {
            Fulfilled = true;
        }

        public void ChangePromisedDate(DateTime newDate)
        {
            PromisedDate = newDate;
        }

        public void ChangeDescription(string desc)
        {
            Description = desc;
        }
    }
}
