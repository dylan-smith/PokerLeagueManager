using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class QueryDataStore : DbContext, IQueryDataStore
    {
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "foo", Justification = "Needed to fix problem with EF references being copied")]
        public QueryDataStore()
            : base("default")
        {
            Database.SetInitializer<QueryDataStore>(null);

            // Do not remove this line, or the references won't be properly picked up by consumer projects
            var foo = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public void Insert<T>(T dto)
            where T : class, IDataTransferObject
        {
            var dtoSet = base.Set<T>();
            dtoSet.Add(dto);
            base.SaveChanges();
        }

        public IQueryable<T> GetData<T>()
            where T : class, IDataTransferObject
        {
            var allICollections = typeof(T).GetProperties().Where(p => p.PropertyType.Name == typeof(ICollection<>).Name);
            var dtoCollections = allICollections.Where(c => typeof(IDataTransferObject).IsAssignableFrom(c.PropertyType.GenericTypeArguments.First()));
            var dtoMembers = typeof(T).GetProperties().Where(p => typeof(IDataTransferObject).IsAssignableFrom(p.PropertyType));

            DbQuery<T> results = base.Set<T>();

            foreach (var col in dtoCollections)
            {
                results = results.Include(col.Name);
            }

            foreach (var prop in dtoMembers)
            {
                results = results.Include(prop.Name);
            }

            return results;
        }

        public void Delete<T>(Guid dtoId)
            where T : class, IDataTransferObject
        {
            var dtoSet = base.Set<T>();
            var dtoToDelete = dtoSet.Single(d => d.DtoId == dtoId);
            dtoSet.Remove(dtoToDelete);
            base.SaveChanges();
        }

        public void Delete<T>(T dto)
            where T : class, IDataTransferObject
        {
            var dtoSet = base.Set<T>();
            dtoSet.Remove(dto);
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            AddAllDtoToModel(modelBuilder);
            base.Configuration.LazyLoadingEnabled = false;

            base.OnModelCreating(modelBuilder);
        }

        private string GetTableName(Type type)
        {
            return type.FullName.Substring(type.FullName.LastIndexOf(".") + 1);
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
                var typeConfig = entityMethod.MakeGenericMethod(dto)
                                             .Invoke(modelBuilder, new object[] { });

                var tableName = GetTableName(dto);
                var genericType = typeof(EntityTypeConfiguration<>).MakeGenericType(dto);
                var toTableMethod = genericType.GetMethod("ToTable", new Type[2] { typeof(string), typeof(string) });

                if (tableName.StartsWith("Lookup"))
                {
                    toTableMethod.Invoke(typeConfig, new object[] { tableName, "Lookups" });
                }
                else
                {
                    toTableMethod.Invoke(typeConfig, new object[] { tableName, "DTO" });
                }
            }
        }
    }
}
