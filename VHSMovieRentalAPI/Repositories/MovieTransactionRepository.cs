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
            return oContext.MovieTransaction
                .Where(x => x.MovieTransactionID == iTransactionID)
                .Include(x => x.MovieTransactionDetails).FirstOrDefault();
        }

        public int CreateTransaction(MovieTransaction oMovieTransaction)
        {
            int iResult = 0;

            // Validate all fields are correct
            string sValidEntity = ValidateInfo(oMovieTransaction);
            if (!string.IsNullOrEmpty(sValidEntity))
            {
                throw new Exception(sValidEntity);
            }

            oMovieTransaction.UpdatedUserID = oMovieTransaction.TransactionUserID;
            oMovieTransaction.Created = DateTime.Now;
            oMovieTransaction.Updated = DateTime.Now;
            oContext.Add(oMovieTransaction);

            foreach (MovieTransactionDetail oDetail in oMovieTransaction.MovieTransactionDetails)
            {

                oDetail.MovieTransactionID = oMovieTransaction.MovieTransactionID;
                oDetail.UpdatedUserID = oMovieTransaction.TransactionUserID;
                oDetail.Created = DateTime.Now;
                oDetail.Updated = DateTime.Now;

                oDetailRepository.CreateTransactionDetail(oDetail);
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
