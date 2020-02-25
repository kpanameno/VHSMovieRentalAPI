using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Models
{
    public class MovieTransactionDetail
    {
        public int MovieTransactionDetailID { get; set; }
        public int MovieTransactionID { get; set; }
        public int MovieID { get; set; }
        public int TransactionTypeID { get; set; }
        public bool Returned { get; set; }
        public int MovieRentalTermID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime Created { get; set; }
        public int UpdatedUserID { get; set; }
        public DateTime Updated { get; set; }

        public Movie Movie { get; set; }
        public TransactionType TransactionType { get; set; }
        public MovieRentalTerm MovieRentalTerm { get; set; }
        public User UpdatedUser { get; set; }
    }
}
