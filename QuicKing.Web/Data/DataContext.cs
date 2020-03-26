using Microsoft.EntityFrameworkCore;
using QuicKing.Web.Data.Entities;

namespace QuicKing.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TaxiEntity> Taxis { get; set; }

        public DbSet<TripEntity> Trips { get; set; }

        public DbSet<TripDetailEntity> TripDetails { get; set; }

    }

}

