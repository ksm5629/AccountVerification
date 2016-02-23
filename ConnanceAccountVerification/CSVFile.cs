using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace ConnanceAccountVerification
{
    /// <summary> 
    /// Object representation of a CSVFile in DataTable Format
    /// (Note: Assumption :- All null values in PayOffDate are converted to DateTime.Minvalue for ease of comparison)
    /// </summary>
    class CSVFile : File
    {
        private DataTable dt;
        private List<Account> listOfAccounts;

        /// <summary>
        /// CSVDataTable </summary>
        /// <value>
        /// Value of CSVDataTable being assigned to property</value>
        /// <returns>Returns the CSVDataTable (datatable) associated with the CSVFile object</returns>
        public DataTable CSVDataTable
        {
            get { return dt; }
            set { dt = value; }
        }
        /// <summary>
        /// The CSVFile constructor. It initializes the DataTable and List of Account objects </summary>
        public CSVFile()
        {
            dt = new DataTable();
            listOfAccounts = new List<Account>();
        }
        /// <summary> 
        /// ParseFile function is used to parse the CSV file
        /// and populate the DataTable
        /// </summary>
        /// <param name="filePath">The String representation of the Path to the file</param>
        public void ParseFile(String filePath)
        {
            int flag = 0;
            String fileLine;
            Account account;
            StreamReader fileReader = new StreamReader(filePath);
            while ((fileLine = fileReader.ReadLine()) != null)
            {  
               fileLine = fileLine.Replace("\"", string.Empty); 
              
                if (flag == 0)
                {
                    String[] list = fileLine.Split(',');
                    dt.Columns.Add(new DataColumn(list[0].ToString(), typeof(System.Int32)));
                    dt.Columns.Add(new DataColumn(list[1].ToString(), typeof(System.String)));
                    dt.Columns.Add(new DataColumn(list[2].ToString(), typeof(System.Double)));
                    dt.Columns.Add(new DataColumn(list[3].ToString(), typeof(System.DateTime)));
                    flag++;
                }
                else {
                    String[] list = fileLine.Split(',');
                    account = new Account();
                    account.AccountNumber = Int32.Parse(list[0]);
                    account.Status = list[1];
                    account.Balance = Double.Parse(list[2]);
                    DateTime dTime = list[3].ToString() == " " ? DateTime.MinValue : Convert.ToDateTime(list[3].ToString());
                    account.PayOffDate = dTime;
                    listOfAccounts.Add(account);
                }
            }
            ImportToDataTable();
        }
        /// <summary> 
        /// ImportToDataTable function is used to parse the List of
        ///  account objects and populate the DataTable
        /// </summary>
        private void ImportToDataTable()
        {
            DataRow row;
            foreach (Account account in listOfAccounts)
            {
                row = dt.NewRow();
                row[0] = account.AccountNumber;
                row[1] = account.Status;
                row[2] = account.Balance;
                row[3] = account.PayOffDate;
                dt.Rows.Add(row);
            }
        }
    }
}
