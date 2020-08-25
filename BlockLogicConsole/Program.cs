using System;
using System.Collections.Generic;
using BlockLogic.BlockChain;
using BlockLogic.TrickleSystem;
using BlockLogic.Scheduler;
using Newtonsoft.Json;
using BlockLogic.TrickleSystem.Backers;
using BlockLogic.TrickleSystem.Services.DatabaseService;
using System.Data.SqlClient;
using BlockLogic.TrickleSystem.PaymentProcessing;
namespace BlockLogicConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TrickleTasks trickle = new TrickleTasks();
            trickle.DailyTasks();

            //TransactionBTC d = new TransactionBTC();
            //d.TestTransactionInfo();

            //TestCurrencyConverter("USD", 5);
        }

        public static void TestCurrencyConverter(string fiat, double amount)
        {
            CurrencyConverter currency = new CurrencyConverter();
            decimal output = currency.FiatToBTC($"{fiat}", amount);
            Console.WriteLine(output);
        }
        public static void TestScheduler()
        {
            Scheduler trickleScheduler = new Scheduler();
            trickleScheduler.BeginScheduler();
            Console.ReadLine();
        }

        public static void TestBlockchain()
        {
            Blockchain BlockCoin = new Blockchain();
            BlockCoin.AddBlock(new Block(DateTime.Now, null, "{sender:intay,receiver:alex,amount:10}"));
            BlockCoin.AddBlock(new Block(DateTime.Now, null, "{sender:intay,receiver:crunchyroll,amount:5}"));
            BlockCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Backer,receiver:StartupCompany,amount:100}"));
            Console.WriteLine(JsonConvert.SerializeObject(BlockCoin, Formatting.Indented));
        }

        public static Startup TestDeposits(Startup myStartup)
        {
            DateTime newDepositDate = new DateTime();
            newDepositDate = DateTime.Now;
            //myStartup.AddDeposit(new Deposit(10, new Backer("fred"), newDepositDate));
            //myStartup.AddDeposit(new Deposit(150, new Backer("frank"),newDepositDate));

            return myStartup;
        }
    }
}
