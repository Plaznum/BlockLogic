using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using BlockLogic.TrickleSystem;
using BlockLogic.TrickleSystem.Backers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BlockLogic.TrickleSystem
{
    [TestFixture]
    public class TestStartup
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetDeposits1()
        {
            
            Startup TestObj = new Startup(); //object used for the testing
            List<Deposit> DepositList = TestObj.GetDeposits(); //method used on object
            List<Deposit> outputTest = new List<Deposit>(); //expected value of TotalDeposits
            Assert.AreEqual(DepositList, outputTest);

        }

        [Test]
        public void TestAddDeposit1()
        {

            Startup TestObj = new Startup(); //object used for the testing
            Deposit TestDep = new Deposit(); //object used for the testing
            TestObj.AddDeposit(TestDep); //method used on object
            List<Deposit> outputTest = new List<Deposit>() { TestDep }; //expected value of TotalDeposits
            Assert.AreEqual(TestObj.TotalDeposits, outputTest);

        }

        [Test]
        public void TestGetMilestones1()
        {

            Startup TestObj = new Startup(); //object used for the testing
            List<Milestone> MilestoneList = TestObj.GetMilestones(); //method used on object
            List<Milestone> outputTest = new List<Milestone>(); //expected value of TotalDeposits
            Assert.AreEqual(MilestoneList, outputTest);

        }

        [Test]
        public void TestAddMilestone1()
        {

            Startup TestObj = new Startup(); //object used for the testing
            DateTime date1 = new DateTime(2015, 12, 25);
            DateTime date2 = new DateTime(2016, 12, 25);
            Milestone TestMile = new Milestone(000, true, "Honda Motors", date1,  date2, false); //object used for the testing
            TestObj.AddMilestone(TestMile); //method used on object
            List<Milestone> outputTest = new List<Milestone>() { TestMile }; //expected value of Milestones
            Assert.AreEqual(TestObj.Milestones, outputTest);

        }

        [Test]
        public void TestAddMoneyReceived1()
        {

            Startup TestObj = new Startup(); //object used for the testing
            TestObj.AddMoneyReceived(300); //method used on object
            double outputTest = 300; //expected value of TotalRecieved
            Assert.AreEqual(TestObj.TotalReceived, outputTest);

        }

        [Test]
        public void TestAddMoneyPromised1()
        {

            Startup TestObj = new Startup(); //object used for the testing
            TestObj.AddMoneyPromised(300); //method used on object
            double outputTest = 300; //expected value of TotalPromised
            Assert.AreEqual(TestObj.TotalPromised, outputTest);

        }

        [Test]
        public void TestChangeCompetionDate1()
        {

            Startup TestObj = new Startup(); //object used for the testing
            DateTime date1 = new DateTime(2015, 12, 25); //object used for the testing
            TestObj.ChangeCompletionDate(date1); //method used on object
            DateTime date2 = new DateTime(2015, 12, 25); //expected value of CompletionDate
            Assert.AreEqual(TestObj.CompletionDate, date2);

        }

        [Test]
        public void TestChangeDescription1()
        {

            Startup TestObj = new Startup(); //object used for the testing
            String desc1 = "wow";
            String desc2 = "wow"; //objects used for the testing
            TestObj.ChangeDescription(desc1); //method used on object
            DateTime date2 = new DateTime(2015, 12, 25); //expected value of CompletionDate
            Assert.AreEqual(TestObj.Description, desc2);

        }

    }
    
}
