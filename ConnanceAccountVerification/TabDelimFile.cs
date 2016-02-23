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
    /// Object representation of a TabDelimFile in DataTable Format
    /// (Note: Assumption :- All null values in PayOffDate are converted to DateTime.Minvalue for ease of comparison) 
    /// </summary>
    class TabDelimFile : File
    {

        private DataTable dataTable;
        private List<Account> listOfAccounts;

        /// <summary>
        /// CSVDataTable </summary>
        /// <value>
        /// Value of TDFDataTable being assigned to property</value>
        /// <returns>Returns the TDFDataTable (datatable) associated with the TabDelimFile object</returns>

        public DataTable TDFDataTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
        
        /// <summary> 
        /// RemoveTimeFromDate function is used to remove the HH:MM:SS from String
        /// </summary>
        /// <param name="str">The String representation of the DateTime object</param>
        /// <returns>Returns the String version of the DateTime object after stripping of the time.</returns>

        private String RemoveTimeFromDate(String str)
        {
            return (str.Split(' ')[0]).Trim();
        }
        
        /// <summary> 
        /// RemoveQuotes function is used to remove the quotes from String and trim it.
        /// </summary>
        /// <param name="tempString">The String representation of the string</param>
        /// <returns>Returns the String version after stripping of the quotes.</returns>

        private String RemoveQuotes(String tempString)
        {
            tempString = tempString.Replace("\"", string.Empty);
            tempString = tempString.Trim();
            return tempString;
        }
        
        /// <summary>
        /// The TabDelimFile constructor. It initializes the DataTable, it's columns and List of Account objects </summary>

        public TabDelimFile()
        {
            dataTable = new DataTable();
            listOfAccounts = new List<Account>();
            dataTable.Columns.Add(new DataColumn("AccountNumber", typeof(System.Int32)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(System.String)));
            dataTable.Columns.Add(new DataColumn("AccountBalance", typeof(System.Double)));
            dataTable.Columns.Add(new DataColumn("PayOffDate", typeof(System.DateTime)));
        }

        /// <summary> 
        /// ParseFile function is used to parse the Fixed width file
        /// and populate the DataTable
        /// </summary>
        /// <param name="filePath">The String representation of the Path to the file</param
        
        public void ParseFile(String filePath)
        {
            String fileLine;
            Account account;
            StreamReader fileReader = new StreamReader(filePath);
            while ((fileLine = fileReader.ReadLine()) != null)
            {
                account = new Account();
                account.AccountNumber = Int32.Parse((fileLine.Substring(0, 10)).Trim());
                account.Status = RemoveQuotes(fileLine.Substring(10, 8));

                account.Balance = (Double)Double.Parse(RemoveQuotes(fileLine.Substring(18, 10)));
                String dateString = RemoveQuotes(RemoveTimeFromDate(fileLine.Substring(28, 19)));
                DateTime dTime = dateString == "" ? DateTime.MinValue : Convert.ToDateTime(dateString);
                account.PayOffDate = dTime;
                listOfAccounts.Add(account);
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
                row = dataTable.NewRow();
                row["AccountNumber"] = account.AccountNumber;
                row["Status"] = account.Status;
                row["AccountBalance"] = account.Balance;
                row["PayOffDate"] = account.PayOffDate;
                dataTable.Rows.Add(row);
            }
        }
    }
}

