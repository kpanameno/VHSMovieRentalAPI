using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class MovieTransactionRepository : DBContextRepository<MovieTransaction>, IMovieTransactionRepository
    {
        private IMovieTransactionDetailRepository oDetailRepository;

        public MovieTransactionRepository(VHSMovieRentalDBContext context) : base(context)
        {
            oDetailRepository = new MovieTransactionDetailRepository(oContext);
        }

        public MovieTransaction GetWithDetailsById(int iTransactionID)
        {
            // Get Transaction by ID and include all Details
            return oContext.MovieTransaction
                .Where(x => x.MovieTransactionID == iTransactionID)
                .Include(x => x.MovieTransactionDetails).FirstOrDefault();
        }

        public int CreateTransaction(int iUserTransactionID, MovieTransaction oMovieTransaction, out string sErrorMessage)
        {
            int iResult = 0;
            sErrorMessage = "";
            try
            {
                // Validate all fields are correct
                if (iUserTransactionID == 0)
                {
                    sErrorMessage = "Empty User Transaction ID";
                    iResult = -1;
                    return iResult;
                }

                // Validate Transaction Details
                int iDetailResult = 0;
                foreach (MovieTransactionDetail oDetail in oMovieTransaction.MovieTransactionDetails)
                {
                    iDetailResult = oDetailRepository.ValidateTransactionDetail(iUserTransactionID, oDetail, out sErrorMessage);

                    // Exit if there was an error during the Detail creation
                    if (!string.IsNullOrEmpty(sErrorMessage))
                        return -1;

                    if (iDetailResult == 0)
                    {
                        sErrorMessage = "Internal Error: Unable to create Detail ";
                        return -1;
                    }

                }

                // Create Transaction Header
                oMovieTransaction.TransactionUserID = iUserTransactionID; // User that comes from the Token
                oMovieTransaction.UpdatedUserID = iUserTransactionID;
                oMovieTransaction.Created = DateTime.Now;
                oMovieTransaction.Updated = DateTime.Now;
                Create(oMovieTransaction);

                iResult = oMovieTransaction.MovieTransactionID;
            }
            catch (Exception ex)
            {
                sErrorMessage = ex.Message;
            }

            return iResult;
        }

        private string ValidateInfo(MovieTransaction oMovieTransaction)
        {
            string sInvalidEntity = "";

            if (oMovieTransaction.TransactionUserID == 0)
            {
                sInvalidEntity += " Transaction User ID";
            }

            if (oMovieTransaction.MovieTransactionDetails.Count() == 0)
            {
                sInvalidEntity += " Quantity";
            }

            if (oMovieTransaction.UpdatedUserID == 0)
            {
                sInvalidEntity += " Updated User";
            }

            if (!string.IsNullOrEmpty(sInvalidEntity))
            {
                sInvalidEntity = "The following fields are missing " + sInvalidEntity;
            }

            return sInvalidEntity;
        }

    }
}
