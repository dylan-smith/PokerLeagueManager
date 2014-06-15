using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using PokerLeagueManager.Common.DTO;

namespace EFSpike.Domain
{
    public class DtoContext : DbContext
    {
        public DtoContext()
            : base("default")
        {
            Database.SetInitializer<DtoContext>(null);
        }

        public DbSet<GetGameResultsDto> Results { get; set; }

        public IEnumerable<GetGameResultsDto> GetResults()
        {
            return Results.ToList();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
