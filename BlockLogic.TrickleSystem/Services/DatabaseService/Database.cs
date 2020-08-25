using System;
using System.Collections.Generic;
using System.Text;
using NPoco;
using System.Data.SqlClient;

namespace BlockLogic.TrickleSystem.Services.DatabaseService
{
    public class Database
    {
        public string connectionString = "Server=blocklogic-db.cqpaiiiys7hb.us-east-2.rds.amazonaws.com;Database=BLOCKLOGICDB;Uid=admin;Pwd=7703647230;";
        public Database()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "blocklogic-db";   // change BlockLogicServer to whatever you named your database!!!
            builder.UserID = "admin";              // sa is the default login for sql server, you should be prompted with info on setting a pw in the config settings in sql server upon installation
            builder.Password = "7703647230";      // update this to your pw
            builder.InitialCatalog = "blocklogicTesting";
            connectionString = builder.ConnectionString;
        }
    }
}
