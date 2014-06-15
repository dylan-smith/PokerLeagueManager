
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PokerLeagueManager.Common.DTO;
namespace EFSpike.Domain
{
    public class Repository : DbContext
    {
        public Repository()
            : base("default")
        {
            Database.SetInitializer<Repository>(null);
        }

        public DbSet<GetGameResultsDto> Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
