using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConnanceAccountVerification
{
    /// <summary> 
    /// Object representation of a FileParser
    /// </summary>
    class FileParser
    {
        /// <summary> 
        /// GetFileExtension function is used to get the file extension of a file
        /// </summary>
        /// <param name="filePath">The String representation of the Path to the file</param>
        /// <returns>Returns the String version of the file extension.</returns>

        public String GetFileExtension(String filePath)
        {
            String[] filePathSplit = filePath.Split('.');
            return filePathSplit[1].Trim();
        }

        /// <summary> 
        /// GetFileName function is used to get the file name of a file
        /// </summary>
        /// <param name="filePath">The String representation of the Path to the file</param>
        /// <returns>Returns the String version of the file name.</returns>

        public String GetFileName(String filePath)
        {
            String[] filePathSplit = filePath.Split('\\');
            return filePathSplit[filePathSplit.Length - 1].Split('.')[0];
        }

        /// <summary> 
        /// ParseFile function is used to parse the files based on file extension
        /// </summary>
        /// <param name="filePath">The String representation of the Path to the file</param>
        /// <returns>Returns the DataTable populated with file records.</returns>
        
        public DataTable ParseFile(String filePath, String fileExtension)
        {
            switch (fileExtension)
            {
                case "csv":
                    CSVFile fileParserCSV = new CSVFile();
                    fileParserCSV.ParseFile(filePath);
                    return fileParserCSV.CSVDataTable;
                case "txt":
                    TabDelimFile fileParserTDF = new TabDelimFile();
                    fileParserTDF.ParseFile(filePath);
                    return fileParserTDF.TDFDataTable;
                default:
                    Console.WriteLine("Provided File format not supported");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return null;
            }
        }
    }
}
