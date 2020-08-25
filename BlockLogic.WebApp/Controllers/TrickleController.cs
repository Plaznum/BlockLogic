using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockLogic.TrickleSystem;
using BlockLogic.TrickleSystem.Backers;
using BlockLogic.TrickleSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlockLogic.WebApp.Controllers
{
    [ApiController]
    [Route("blocklogic")]
    public class TrickleController : ControllerBase
    {
        TrickleService trickle;

        public TrickleController()
        {
            trickle = new TrickleService();
        }

        [HttpGet]
        [Route("GetAllBackers")]
        public IEnumerable<Backer> GetAllBackers()
        {
            var data = trickle.GetAllBackers();
            return data;
        }

        [HttpGet]
        [Route("GetAllMilestones")]
        public IEnumerable<Milestone> GetAllMilestones()
        {
            var data = trickle.GetMilestones();
            return data;
        }

        [HttpGet]
        [Route("GetAllStartups")]
        public IEnumerable<Startup> GetAllStartups()
        {
            var data = trickle.GetAllStartups();
            return data;
        }

        [HttpGet]
        [Route("GetAllFundAccounts")]
        public IEnumerable<FundAccount> GetAllFundAccounts()
        {
            var data = trickle.GetFundAccounts();
            return data;
        }

        [HttpGet]
        [Route("GetBackerByFundingID")]
        public IEnumerable<Backer> GetBackerByFundingID(int n)
        {
            var data = trickle.GetBackerByFunding(n);
            return data;
        }

        [HttpGet]
        [Route("GetFundAccountsByBackerID")]
        public IEnumerable<FundAccount> GetFundAccountsByBackerID(int n)
        {
            var data = trickle.GetFundAccountsByBackerID(n);
            return data;
        }

        [HttpGet]
        [Route("GetMilestoneByStartupID")]
        public IEnumerable<Milestone> GetMilestoneByStartupID(int n)
        {
            var data = trickle.GetMilestoneByStartupID(n);
            return data;
        }

        //[HttpPost]
        //[Route("InsertFundAccount")]
        //public void InsertFundAccount()
        //{

        //}
    }
}
