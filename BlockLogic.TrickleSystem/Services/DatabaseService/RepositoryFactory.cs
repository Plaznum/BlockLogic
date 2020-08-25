using System;
using System.Collections.Generic;
using System.Text;
using NPoco;
using System.Data.SqlClient;

namespace BlockLogic.TrickleSystem.Services.DatabaseService
{
    public class RepositoryFactory
    {
        public SqlCommand InsertBackerBADVERSION(Backer newBacker, SqlConnection connection)
        {
            using (SqlCommand sql = new SqlCommand("Insert into backers Values(@ID, @Funding, @Name, @Email)", connection))
            {
                sql.Parameters.Add(new SqlParameter("ID", newBacker.ID));
                sql.Parameters.Add(new SqlParameter("Funding", newBacker.Funding));
                sql.Parameters.Add(new SqlParameter("Name", newBacker.Name));
                sql.Parameters.Add(new SqlParameter("Email", newBacker.Email));
                return sql;
            }
        }
    }
}
