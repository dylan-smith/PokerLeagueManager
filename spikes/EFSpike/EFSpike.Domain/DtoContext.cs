using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            var allICollections = typeof(GetGameResultsDto).GetProperties().Where(p => p.PropertyType.Name == typeof(ICollection<>).Name);
            var dtoCollections = allICollections.Where(c => typeof(IDataTransferObject).IsAssignableFrom(c.PropertyType.GenericTypeArguments.First()));
            var dtoMembers = typeof(GetGameResultsDto).GetProperties().Where(p => typeof(IDataTransferObject).IsAssignableFrom(p.PropertyType));

            DbQuery<GetGameResultsDto> result = Results;

            foreach (var col in dtoCollections)
            {
                result = result.Include(col.Name);
            }

            foreach (var prop in dtoMembers)
            {
                result = result.Include(prop.Name);
            }

            return result.ToList();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.Configuration.LazyLoadingEnabled = false;
            base.OnModelCreating(modelBuilder);
        }
    }
}
