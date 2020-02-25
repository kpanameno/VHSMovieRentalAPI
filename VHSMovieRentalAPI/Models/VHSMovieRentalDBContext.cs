using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Models
{
    public class VHSMovieRentalDBContext: DbContext
    {

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieLike> MovieLikes { get; set; }
        public DbSet<MoviePriceLog> MoviePriceLogs { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<MovieRentalTerm> MovieRentalTerm { get; set; }
        public DbSet<MovieTransaction> MovieTransaction { get; set; }
        public DbSet<MovieTransactionDetail> MovieTransactionDetail { get; set; }

        public VHSMovieRentalDBContext(DbContextOptions<VHSMovieRentalDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            //optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=VHSMovieRentalDB;Integrated Security=True");
        }

        


    }
}
