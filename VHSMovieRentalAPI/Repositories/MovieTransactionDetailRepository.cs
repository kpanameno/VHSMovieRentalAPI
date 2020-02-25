using Microsoft.EntityFrameworkCore;
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

        public int ValidateTransactionDetail(int iTransactionUserID, MovieTransactionDetail oDetail, out string sErrorMessage)
        {
            int iResult = 0;
            sErrorMessage = "";
            try
            {
                // Validate Foreign Keys
                var oMovie = oContext.Movies.AsNoTracking().Where(x => x.MovieID == oDetail.MovieID && x.Available).FirstOrDefault();
                if (oMovie == null)
                {
                    sErrorMessage = "Movie doesn't exist";
                    return -1;
                }
                
                // Check if there is any stock available
                int oOtherDetails = oContext.MovieTransactionDetail.AsNoTracking().Where(x => x.MovieID == oDetail.MovieID).Select(x => x.Quantity).Sum();
                if (oOtherDetails + oDetail.Quantity > oMovie.Stock)
                {
                    sErrorMessage = "Cant Rent/Buy Movie: " + oMovie.Title + " not enough Stock.";
                    return -1;
                }

                var oTransactionType = oContext.TransactionType.AsNoTracking().Where(x => x.TransactionTypeID == oDetail.TransactionTypeID).FirstOrDefault();
                if (oTransactionType == null)
                {
                    sErrorMessage = "Transaction Type doesn't exist";
                    return -1;
                }

                if (oDetail.MovieRentalTermID != 0)
                {
                    var oRentalTerm = oContext.MovieRentalTerm.AsNoTracking().Where(x => x.MovieRentalTermID == oDetail.MovieRentalTermID).FirstOrDefault();
                    if (oRentalTerm == null)
                    {
                        sErrorMessage = "Movie Rental Term doesn't exist";
                        return -1;
                    }
                }
                else
                {
                    oDetail.MovieRentalTermID = null;
                }

                // By Default a Movie Rent will NOT be paid, take Price from Movies
                if (oTransactionType.Description.ToLower().Trim() == "rental")
                {
                    oDetail.Returned = false;
                    oDetail.Price = oMovie.RentalPrice;
                }
                else
                {
                    oDetail.Returned = true;
                    oDetail.Price = oMovie.SalePrice;
                }

                // Rental Term is needed for Movie Rentals
                if (!oDetail.Returned && oDetail.MovieRentalTermID == 0)
                {
                    throw new Exception("Rented Movie requieres a Movie Rental Term.");
                }

                // Create Detail
                oDetail.UpdatedUserID = iTransactionUserID;
                oDetail.Created = DateTime.Now;
                oDetail.Updated = DateTime.Now;
                iResult = 1;
            }
            catch (Exception ex)
            {
                sErrorMessage = ex.Message;
            }

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

            if (oDetail.TransactionTypeID == 0)
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
