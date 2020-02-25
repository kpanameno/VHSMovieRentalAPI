using System;
using System.Linq;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class MovieTransactionDetailRepository : DBContextRepository<MovieTransactionDetail>, IMovieTransactionDetailRepository
    {

        public MovieTransactionDetailRepository(VHSMovieRentalDBContext context) : base(context)
        {

        }

        public int  CreateTransactionDetail(MovieTransactionDetail oDetail)
        {
            int iResult = 0;
            // Validate all fields are correct
            string sValidEntity = ValidateInfo(oDetail);
            if (!string.IsNullOrEmpty(sValidEntity))
            {
                throw new Exception(sValidEntity);
            }

            var oTransaction = oContext.TransactionType.Where(x => x.Description == "RENT").FirstOrDefault();

            if (oDetail.TransactionTypeID == oTransaction.TransactionTypeID)
                oDetail.Returned = false;
            else oDetail.Returned = true;

            if (oDetail.Returned = false && oDetail.TransactionTypeID == 0)
            {
                throw new Exception("Rented Movie requieres a Transaction Type.");
            }

            oContext.Add(oDetail);

            iResult = oDetail.MovieTransactionDetailID;

            return iResult;
        }

        /// <summary>
        /// Result = 0: The client doesn't have to Pay any additional charges
        /// Result <> 0: The client pays the resulting value
        /// </summary>
        /// <param name="iMovieTransactionDetailId"></param>
        /// <returns></returns>
        public decimal ReturnMovieRental(int iMovieTransactionDetailId)
        {
            decimal dResult = 0;
            var oExistingDetail = oContext.MovieTransactionDetail
                .Where(x => x.MovieTransactionDetailID == iMovieTransactionDetailId)
                .FirstOrDefault();
            
            // Non existing Detail
            if (oExistingDetail == null)
                return dResult;

            // Not a rental
            var oTransaction = oContext.TransactionType.Where(x => x.Description == "RENT").FirstOrDefault();
            if (oExistingDetail.TransactionTypeID != oTransaction.TransactionTypeID)
                return dResult;

            // Rental already returned
            if (oExistingDetail.Returned)
                return dResult;

            var oTerm = oContext.MovieRentalTerm.Where(x => x.MovieRentalTermID == oExistingDetail.MovieRentalTermID).FirstOrDefault();
            DateTime tStartDate = oExistingDetail.Created;
            DateTime tEndDate = DateTime.Now;
            TimeSpan oRentedInterval = tEndDate - tStartDate;

            if (oRentedInterval.Days > oTerm.RentalDays)
            {
                int iExtraDays = oRentedInterval.Days - oTerm.RentalDays;
                dResult = oTerm.LateReturnCharge * iExtraDays;
            }

            return dResult;
        }

    private string ValidateInfo(MovieTransactionDetail oDetail)
        {
            string sInvalidEntity = "";

            if (oDetail.MovieTransactionID == 0)
                sInvalidEntity += " Transaction";

            if (oDetail.MovieID == 0)
                sInvalidEntity += " Movie";

            if (oDetail.TransactionTypeID==0)
                sInvalidEntity += " Transaction Type";

            if (oDetail.Quantity == 0)
                sInvalidEntity += " Quantity";

            if (oDetail.UpdatedUserID == 0)
                sInvalidEntity += " Updated User";

            if (!string.IsNullOrEmpty(sInvalidEntity))
            {
                sInvalidEntity = "The following fields are missing for the Detail " + sInvalidEntity;
            }

            return sInvalidEntity;
        }
               
    }
}
