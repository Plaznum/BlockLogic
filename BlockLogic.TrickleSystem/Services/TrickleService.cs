using BlockLogic.BlockChain;
using BlockLogic.TrickleSystem.Backers;
using BlockLogic.TrickleSystem.Services.DatabaseService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BlockLogic.TrickleSystem.Services
{
    public class TrickleService
    {
        RepositoryFactory repoFactory;
        Database db = new Database();
        Blockchain blockchain;
        public TrickleService()
        {
            repoFactory = new RepositoryFactory();
            db = new Database();
            blockchain = new Blockchain();
        }

        public void AddBlock(Deposit depo)
        {
            Block b = new Block(DateTime.Now, null, $"Sender: {depo.BackerID}, Receiver: {depo.StartupID}, Amount: {depo.Amount}, Date: {depo.Date}");
            blockchain.AddBlock(b);
        }

        public void CreateDeposit(FundAccount fund)
        {
            var deposit = new Deposit
            {
                StartupID = fund.StartupID,
                BackerID = fund.BackerID,
                Date = DateTime.UtcNow,
                Amount = fund.TrickleAmount
            };

            try
            {
                using (var context = new BlockContext())
                {
                    context.Deposits.Add(deposit);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void RemoveMilestone(Milestone mile)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.Milestones.Remove(mile);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        public List<Milestone> GetMilestones()
        {
            var milestones = new List<Milestone>();
            try
            {
                using (var context = new BlockContext())
                {
                    milestones = context.Milestones.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return milestones;
        }

        public List<Milestone> GetMilestoneByStartupID(int id)
        {
            var milestones = new List<Milestone>();
            try
            {
                using (var context = new BlockContext())
                {
                    milestones = context.Milestones.Where(p => p.StartupID == id).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return milestones;
        }

        public void UpdateStartup(Startup start)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.Startups.Update(start);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateFundAccount(FundAccount fund)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.FundAccount.Update(fund);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateMilestone(Milestone mile)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.Milestones.Update(mile);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateBacker(Backer back)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.Backers.Update(back);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Startup GetStartup(int id)
        {
            var back = new Startup();
            try
            {
                using (var context = new BlockContext())
                {
                    back = context.Startups.Where(p => p.ID == id).Single();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return back;
        }

        public Backer GetBacker(int id)
        {
            var back = new Backer();
            try
            {
                using (var context = new BlockContext())
                {
                    back = context.Backers.Where(p => p.ID == id).Single();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return back;
        }

        public List<Backer> GetBackerByFunding(int id)
        {
            List<Backer> back = new List<Backer>();
            try
            {
                using (var context = new BlockContext())
                {
                    back = context.Backers.Where(p => p.Funding == id).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return back;
        }
        public List<FundAccount> GetFundAccounts()
        {
            var fundaccounts = new List<FundAccount>();
            try
            {
                using (var context = new BlockContext())
                {
                    fundaccounts = context.FundAccount.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return fundaccounts;
        }

        public List<Backer> GetAllBackers()
        {
            var backers = new List<Backer>();
            try
            {
                using (var context = new BlockContext())
                {
                    backers = context.Backers.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return backers;
        }

        public IEnumerable<Startup> GetAllStartups()
        {
            var startups = new List<Startup>();
            try
            {
                using (var context = new BlockContext())
                {
                    startups = context.Startups.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return startups;
        }

        public List<FundAccount> GetFundAccountsByBackerID(int id)
        {
            var fundaccounts = new List<FundAccount>();
            try
            {
                using (var context = new BlockContext())
                {
                    fundaccounts = context.FundAccount.Where(p => p.BackerID == id).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return fundaccounts;
        }
        public void InsertMilestone(Milestone mile)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.Milestones.Add(mile);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void InsertFundAccount(FundAccount fund)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.FundAccount.Add(fund);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void InsertStartup(Startup startup)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.Startups.Add(startup);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void InsertBacker(Backer backer)
        {
            try
            {
                using (var context = new BlockContext())
                {
                    context.Backers.Add(backer);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void InsertBackerBADVERSION(Backer newBacker)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(db.connectionString))
                {
                    connection.Open();
                    SqlCommand query = repoFactory.InsertBackerBADVERSION(newBacker, connection);
                    query.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendMilestoneCompletionEmail(Backer back, Startup start, Milestone mile)
        {
            try
            {
                string from = "frankdtest@gmail.com";
                string password = "testSESH420"; 

                Emailer emailer = new Emailer(from, password, back, start, mile);
                emailer.MilestoneCompleted();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void CompleteMilestone(Milestone mile)
        {
            mile.Fulfilled = true;
            try
            {
                using (var context = new BlockContext())
                {
                    context.Milestones.Update(mile);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void SendFailedMilestoneEmail(Backer b, Startup s, Milestone m)
        {
            try
            {
                string from = "frankdtest@gmail.com"; 
                string password = "testSESH420"; 

                Emailer emailer = new Emailer(from, password, b, s, m);
                emailer.MilestoneMissedDeadline();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UpdateChain(Backer back, Startup start, FundAccount fund, Blockchain block)
        {
            block.AddBlock(new Block(DateTime.Now, null, $"Sender: {start.Name}, Receiver: {back.Name} ,Amount: {fund.TrickleAmount}"));
            //Console.WriteLine(JsonConvert.SerializeObject(block, Formatting.Indented));
        }
    }
}
