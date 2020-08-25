using BlockLogic.TrickleSystem.Backers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace BlockLogic.TrickleSystem.Services.DatabaseService
{
    public class BlockContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Backer> Backers { get; set; }
        public DbSet<Startup> Startups { get; set; }
        public DbSet<FundAccount> FundAccount { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql("Server=blocklogic-db.cqpaiiiys7hb.us-east-2.rds.amazonaws.com;Database=BLOCKLOGICDB;User=admin;Password=7703647230;AllowZeroDateTime=True;", mySqlOptions => mySqlOptions
                    .ServerVersion(new Version(8, 0, 18), ServerType.MySql));
    }
}
