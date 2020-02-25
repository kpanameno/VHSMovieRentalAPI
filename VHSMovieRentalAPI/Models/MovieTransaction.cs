using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Models
{
    public class MovieTransaction
    {
        public int MovieTransactionID { get; set; }
        public int TransactionUserID { get; set; }
        public string PaymentType { get; set; }
        public string Branch { get; set; }
        public DateTime Created { get; set; }
        public int UpdatedUserID { get;set; }
        public DateTime Updated { get; set; }

        public User User { get; set; }
        public User UpdatedUser { get; set; }
        public ICollection<MovieTransactionDetail> MovieTransactionDetails { get; set; }
    }

}
