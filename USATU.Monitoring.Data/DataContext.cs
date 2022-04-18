using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using USATU.Monitoring.Data.Tasks;
using USATU.Monitoring.Data.Users;


namespace USATU.Monitoring.Data
{
    public class DataContext : DbContext
    {
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<TaskMonitoringDbModel> Tasks { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

    }

    public class Factory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder()
                .UseNpgsql("Host=localhost;Port=5432;Database=USATU;Username=postgres;Password=mandarin2012")
                .Options;

            return new DataContext(options);
        }
    }

}