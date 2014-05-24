using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using PokerLeagueManager.Common.DTO.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class QueryDataStore : DbContext, IQueryDataStore
    {
        public QueryDataStore()
            : base("default")
        {
            Database.SetInitializer<QueryDataStore>(null);
        }

        public void Insert<T>(T dto) where T : class, IDataTransferObject
        {
            var dtoSet = base.Set<T>();
            dtoSet.Add(dto);
            base.SaveChanges();
        }

        public IEnumerable<T> GetData<T>() where T : class, IDataTransferObject
        {
            return base.Set<T>();
        }

        public void Update<T>(T dto) where T : class, IDataTransferObject
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            AddAllDtoToModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void AddAllDtoToModel(DbModelBuilder modelBuilder)
        {
            var dtoTypes = typeof(IDataTransferObject).Assembly.GetExportedTypes()
                             .Where(x => x.IsClass &&
                                         x.GetInterfaces().Contains(typeof(IDataTransferObject)) &&
                                         x.Name != "BaseDataTransferObject");

            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            foreach (var dto in dtoTypes)
            {
                entityMethod.MakeGenericMethod(dto)
                    .Invoke(modelBuilder, new object[] { });
            }
        }
    }
}
