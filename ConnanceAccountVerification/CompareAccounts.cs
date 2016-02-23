using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace ConnanceAccountVerification
{
    /// <summary> 
    /// Class for Comparing the DataTables received from accounts database
    /// and received files from agency or hospital
    /// </summary>
    class CompareAccounts
    {
        /// <summary> 
        /// CompareAccountsFromFile function is used to compare the datatables
        /// and generate a txt file based on the output
        /// </summary>
        /// <param name="accountsDB">The DataTable with information from Database</param>
        /// <param name="fileRecords">The DataTable genrated from information from incoming file</param>
        /// <param name="fileName">FileName of the file, is used to set the name of the output file</param>
        public void CompareAccountsFromFile(DataTable accountsDB, DataTable fileRecords, String fileName)
        {
            String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            StreamWriter generatedFile = new StreamWriter(desktopPath+@"\"+fileName + "Output.txt", true);
            generatedFile.AutoFlush = true;

            // Perform a  LINQ join and select rows only if status are not same
            var numberOfDifferent = (from acc in accountsDB.AsEnumerable()
                                     join file in fileRecords.AsEnumerable()
                                     on acc[0] equals file[0]
                                     where acc[1].Equals(file[1]) == false
                                     select new { acc });
            generatedFile.WriteLine("The Number of accounts where the statuses are different is: " + numberOfDifferent.Count());

            // Perform a  LINQ join and select rows only if balances are not same and status is "open"
            numberOfDifferent = (from acc in accountsDB.AsEnumerable()
                                 join file in fileRecords.AsEnumerable()
                                 on acc[0] equals file[0]
                                 where acc[2] != file[2] && acc[1].Equals("open")
                                 select new { acc });
            generatedFile.WriteLine("The Number of accounts where the balances are different (if account is open) is: " + numberOfDifferent.Count());

            // Perform a  LINQ join and select rows only if PayOffDate's are not same and status is "open"
            numberOfDifferent = (from acc in accountsDB.AsEnumerable()
                                 join file in fileRecords.AsEnumerable()
                                 on acc[0] equals file[0]
                                 where (acc[3]).ToString().Equals(file[3].ToString())==false && acc[1].Equals("open")
                                 select new { acc });
            generatedFile.WriteLine("The Number of accounts where the expected Pay-off date is different (if account is open) is: " + numberOfDifferent.Count());
            
            // Perform a LINQ Join to find common records
            numberOfDifferent = (from acc in accountsDB.AsEnumerable()
                                 join file in fileRecords.AsEnumerable()
                                 on acc[0] equals file[0]
                                 select new { acc });

            // Subtract the common records with length of Individual tables to get counts for unique rows.
            int numberOfFileEqualValue = fileRecords.Rows.Count - numberOfDifferent.Count();
            generatedFile.WriteLine("The Number of accounts in the file but not in the database is: " + numberOfFileEqualValue);
            int numberOfDataBaseEqualValue = accountsDB.Rows.Count - numberOfDifferent.Count();
            generatedFile.WriteLine("The Number of accounts in the database but not in the file is: " + numberOfDataBaseEqualValue);
            generatedFile.Close();
        }
    }
}
