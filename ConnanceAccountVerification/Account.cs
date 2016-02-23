using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnanceAccountVerification
{  
    /// <summary> 
   /// Object representation of a record of an account
   /// </summary>
    class Account
    {
        private Int32 accountNumber;
        private String status;
        private Double balance;
        private DateTime payOffDate;
        /// <summary>
        /// Status </summary>
        /// <value>
        /// Value of status being assigned to property</value>
        /// <returns>Returns the status of an account</returns>>
        public String Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// Balance </summary>
        /// <value>
        /// Value of balance being assigned to property</value>
        /// <returns>Returns the balance of an account</returns>>
        public Double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        /// <summary>
        /// PayOffDate </summary>
        /// <value>
        /// Value of PayOffDate being assigned to property</value>
        /// <returns>Returns the PayOffDate of an account</returns>>
        public DateTime PayOffDate
        {
            get { return payOffDate; }
            set { payOffDate = value; }
        }
        /// <summary>
        /// AccountNumber </summary>
        /// <value>
        /// Value of AccountNumber being assigned to property</value>
        /// <returns>Returns the AccountNumber of an account</returns>>
        public Int32 AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }
    }
}
