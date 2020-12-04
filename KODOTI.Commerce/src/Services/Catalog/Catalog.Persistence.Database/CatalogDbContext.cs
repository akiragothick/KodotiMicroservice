using Catalog.Domain;
using Catalog.Persistence.Database.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Catalog.Persistence.Database
{
	public class CatalogDbContext : DbContext
	{
		public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductInStock> Stocks { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Database Schema
			builder.HasDefaultSchema("Catalog");

			// Model Contraints
			ModelConfig(builder);
		}

		private void ModelConfig(ModelBuilder modelBuilder)
		{
			new ProductConfiguration(modelBuilder.Entity<Product>());
			new ProductInStockConfiguration(modelBuilder.Entity<ProductInStock>());
		}

		//add-migration InitialCreate
		//update-database -context CatalogDbContext
	}
}
