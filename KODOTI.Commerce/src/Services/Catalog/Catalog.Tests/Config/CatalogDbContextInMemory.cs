using Catalog.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Tests.Config
{
	public static class CatalogDbContextInMemory
	{
		public static CatalogDbContext Get()
		{
			var options = new DbContextOptionsBuilder<CatalogDbContext>()
				.UseInMemoryDatabase(databaseName: $"Catalog.Db")
				.Options;

			return new CatalogDbContext(options);
		}
	}
}
