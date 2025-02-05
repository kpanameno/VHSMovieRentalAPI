using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;
using VHSMovieRentalAPI.Repositories;
using VHSMovieRentalAPI.SecurityHelpers;

namespace VHSMovieRentalAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            // Authentication
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("VHSMovieRentalSettings").Get<VHSMovieRentalSettings>().SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            // DBContext
            services.AddDbContext<VHSMovieRentalDBContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("VHSMovieRentalDB"))
            );

            // Repositories
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieLikeRepository, MovieLikeRepository>();
            services.AddTransient<IMoviePriceLogRepository, MoviePriceLogRepository>();
            
            services.AddTransient<IMovieTransactionRepository, MovieTransactionRepository>();
            services.AddTransient<IMovieTransactionDetailRepository, MovieTransactionDetailRepository>();

            services.AddTransient<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddTransient<IMovieRentalTermRepository, MovieRentalTermRepository>();

            services.AddControllersWithViews();
            
            var oSettingsSection = Configuration.GetSection("VHSMovieRentalSettings");
            services.Configure<VHSMovieRentalSettings>(oSettingsSection);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Movie}/{action=Index}");
                endpoints.MapControllers();

            });

        }
    }
}
