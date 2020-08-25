using BlockLogic.BlockChain;
using BlockLogic.TrickleSystem.Backers;
using BlockLogic.TrickleSystem.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BlockLogic.TrickleSystem.PaymentProcessing;

namespace BlockLogic.TrickleSystem
{
    public class TrickleTasks
    {
        private TrickleService trickle;
        private Blockchain block;
        private CurrencyConverter currency;
        private TransactionBTC transaction;
        public TrickleTasks()
        {
            trickle = new TrickleService();
            block = new Blockchain();
            currency = new CurrencyConverter();
            transaction = new TransactionBTC();
        }

        public void DailyTasks()
        {
            TrickleService trickle = new TrickleService();
            var fundAccountList = trickle.GetFundAccounts();

            foreach (var fund in fundAccountList)
            {
                var startup = trickle.GetStartup(fund.StartupID);
                var backer = trickle.GetBacker(fund.BackerID);

                switch(fund.GetTrickleType())
                {
                    case "Standard":
                        Trickle(fund, startup, backer);
                        break;
                    case "Milestone":
                        MilestoneTrickle(fund, startup, backer);                        
                        break;
                    case "Percentage":
                        PercentageTrickle(fund, startup, backer);
                        break;
                    default:
                        break;
                }
                CheckMilestones(backer, startup);
            }
        }

        public void PercentageTrickle(FundAccount fund, Startup start, Backer backer)
        {
            if (fund.hasCapped == false &
                fund.TotalDonated <= (fund.TrickleCap - fund.TrickleAmount))
            {
                Trickle(fund, start, backer);
            }
            CheckMilestones(fund);
        }

        public void CheckMilestones(FundAccount fund)
        {
            var milestoneList = trickle.GetMilestoneByStartupID(fund.StartupID);
            foreach (Milestone mile in milestoneList)
            {
                if (mile.Fulfilled == true & mile.FulfilledCompleted == false)
                {
                    fund.TotalDonated += fund.Remainder;
                    fund.Remainder = 0;
                    fund.hasCapped = true;
                    trickle.UpdateFundAccount(fund);
                }
            }
        }
        public void MilestoneTrickle(FundAccount fund, Startup startup, Backer backer)
        {
            var milestoneList = trickle.GetMilestoneByStartupID(startup.ID);
            foreach (Milestone mile in milestoneList)
            {
                if (mile.Fulfilled == true & mile.FulfilledCompleted == false)
                {
                    Trickle(fund, startup, backer);
                    mile.FulfilledCompleted = true;

                }
            }
        }
        public void Trickle(FundAccount fund, Startup startup, Backer backer)
        {
            if (fund.TotalDonated >= fund.TrickleCap)
            {
                fund.hasCapped = true;
                trickle.UpdateFundAccount(fund);
            }

            if (fund.TotalMoney >= fund.TrickleAmount && fund.hasCapped == false)
            {
                decimal bitcoin = currency.FiatToBTC("USD", fund.TrickleAmount);
                TransactionBTC.SendTransaction(bitcoin, startup.BTCAddress);
                startup.TotalReceived += fund.TrickleAmount;
                fund.TotalDonated += fund.TrickleAmount;
                backer.TotalMoney -= fund.TrickleAmount;
                fund.TotalMoney -= fund.TrickleAmount;
                UpdateDatabase(backer, startup, fund);
            }
        }

        public void UpdateDatabase(Backer backer, Startup startup, FundAccount fund)
        {
            trickle.UpdateFundAccount(fund);
            trickle.UpdateBacker(backer);
            trickle.UpdateStartup(startup);
            trickle.CreateDeposit(fund);
            trickle.UpdateChain(backer, startup, fund, block);
        }
        //public void StandardTrickle()
        //{
        //    TrickleService trickle = new TrickleService();
        //    var fundAccountList = trickle.GetFundAccounts();

        //    foreach (var fund in fundAccountList)
        //    {
        //        var startup = trickle.GetStartup(fund.StartupID);
        //        var backer = trickle.GetBacker(fund.BackerID);

        //        if (backer.TotalMoney >= fund.TrickleAmount)
        //        {
        //            startup.TotalReceived += fund.TrickleAmount;
        //            backer.TotalMoney -= fund.TrickleAmount;
        //            fund.TotalMoney -= fund.TrickleAmount;
        //        }
        //        trickle.UpdateBacker(backer);
        //        trickle.UpdateStartup(startup);
        //        trickle.CreateDeposit(fund);
        //        trickle.UpdateChain(backer, startup, fund, block);
        //        CheckMilestones(backer,startup);


        //        // add logic for depositing money into a bitcoin wallet
        //        // add an else statement to send an email to the backer saying he's pledged all the money in his account
        //        // add logic to create a deposit object linked with the transaction 

        //    }
        //}
        public void CheckMilestones(Backer backer, Startup startup)
        {
            var milestoneList = trickle.GetMilestoneByStartupID(startup.ID);
            foreach (Milestone mile in milestoneList)
            {
                var back = trickle.GetBackerByFunding(mile.StartupID);

                if (mile.Fulfilled == true & mile.FulfilledCompleted == false)
                {
                    mile.FulfilledCompleted = true;
                    trickle.UpdateMilestone(mile);
                    foreach (Backer b in back)
                    {
                        trickle.SendMilestoneCompletionEmail(b, startup, mile);
                    }
                }
                if (mile.PromisedDate < DateTime.UtcNow & mile.Fulfilled == false)
                {
                    foreach (Backer b in back)
                        trickle.SendFailedMilestoneEmail(b, startup, mile);
                }
            }
        }
    }
}
