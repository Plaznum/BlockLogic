using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace BlockLogic.TrickleSystem.Backers
{
    class TestBacker
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestAddFundAccount()
        {
            Backer TestObj = new Backer();
            TestObj.AddFundAccount(300);
            double outputTest = 300;
            Assert.AreEqual(TestObj.Funding, outputTest);

        }

        [Test] // Testing for GetFundAccount
        public void TestGetFundAccount()
        {
            Backer TestObj = new Backer();
            int TestInt = TestObj.GetFundingAccount();
            int output = 0;
            Assert.AreEqual(TestInt, output);
        }

        [Test] // Testing for AddEmailAddress
        public void AddEmailAddress()
        {
            Backer TestObj = new Backer();
            TestObj.AddEmailAddress("Coronaxd69@lmao.com");
            String output = "Coronaxd69@lmao.com";
            Assert.AreEqual(TestObj.Email, output);

        }

        [Test] // Testing for GetEmailAddress
        public void GetEmailAddress()
        {
            Backer TestObj = new Backer();
            String TestString = TestObj.GetEmailAddress();
            String output = "Coronaxd69@lmao.com";
            Assert.AreEqual(TestString, output);
        }


    }
}

