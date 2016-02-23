using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConnanceAccountVerification
{
    /// <summary> 
    /// Main Class
    /// </summary>
    /*
        Assumptions made for this program:
        1> All files that will be sent from hospital and agency will have unique name.
        2> Database nulls for PayOffDate and no date in files for the same will be converted into DatTime.Minvalue. 
        3> All output files will be saved at the Desktop of the computer.
        4> Change the files paths to the current filePath after downloading
        5> Please attach the database backup and check in LocalDB. 
        6> Please update App.config file to change the connectionString as per new DB. 
        
        */
    class Program
    {
        /// <summary>
        /// The Main function to run for AccountVerification Application.
        /// </summary>
        /// <param name="args"> An array of CLI arguments</param>
        public static void Main(String[] args)
        {
            AccountsDatabase accDB = new AccountsDatabase();
            accDB.PopulateAccountDatabase();
            FileParser fileParser = new FileParser();
            String csvFilePath = @"C:\Users\Karan\Desktop\trycsv.csv";
            String fixedWidthFilePath = @"C:\Users\Karan\Desktop\tryfixedWidth.txt";
            DataTable fromFile = fileParser.ParseFile(csvFilePath, fileParser.GetFileExtension(csvFilePath));
            
            CompareAccounts compareAccounts = new CompareAccounts();

            // For CSV
            if (fromFile != null && accDB.AccountDatabase != null) {
                compareAccounts.CompareAccountsFromFile(accDB.AccountDatabase, fromFile, fileParser.GetFileName(csvFilePath));
            }
            fromFile = fileParser.ParseFile(fixedWidthFilePath, fileParser.GetFileExtension(fixedWidthFilePath));

            // For Fixed Width File
            if (fromFile != null && accDB.AccountDatabase != null)
            {
                compareAccounts.CompareAccountsFromFile(accDB.AccountDatabase, fromFile, fileParser.GetFileName(fixedWidthFilePath));
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
