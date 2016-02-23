using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ConnanceAccountVerification
{
    /// <summary> 
    /// Object representation of the Accounts database
    /// (Note: Assumption :- All null values in PayOffDate are converted to DateTime.Minvalue for ease of comparison)
    /// </summary>
    class AccountsDatabase
    {
        private DataTable accountDatabase;
        /// <summary>
        /// AccountDatabase </summary>
        /// <value>
        /// Value of AccountDatabase being assigned to property</value>
        /// <returns>Returns the AccountDatabase (datatable) associated with the AccountsDatabase object</returns>>
        public DataTable AccountDatabase
        {
            get { return accountDatabase; }
            set { accountDatabase = value; }
        }
        /// <summary>
        /// The AccountsDatabase constructor. It initializes the DataTable and sets the columns </summary>
        public AccountsDatabase()
        {
            accountDatabase = new DataTable();
            accountDatabase.Columns.Add(new DataColumn("AccountNumber", typeof(System.Int32)));
            accountDatabase.Columns.Add(new DataColumn("Status", typeof(System.String)));
            accountDatabase.Columns.Add(new DataColumn("AccountBalance", typeof(System.Double)));
            accountDatabase.Columns.Add(new DataColumn("PayOffDate", typeof(System.DateTime)));
        }
        /// <summary> 
        /// PopulateAccountDatabase function queries the created Accounts Database and
        /// populates the DaraTable i.e. accountDatabase associated with the object
        /// </summary>
        public void PopulateAccountDatabase()
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccountConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Account", connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Account", connection);
            try
            {
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    DataRow row;
                    while (dr.Read())
                    {
                        row = accountDatabase.NewRow();
                        row["AccountNumber"] = Int32.Parse(dr["AccountNumber"].ToString());
                        row["Status"] = dr["Status"].ToString();
                        row["AccountBalance"] = Double.Parse(dr["AccountBalance"].ToString());
                        DateTime dTime = dr["PayOffDate"].ToString().Equals(String.Empty) ? DateTime.MinValue : Convert.ToDateTime(dr["PayOffDate"]);
                        row["PayOffDate"] = dTime;
                        accountDatabase.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

