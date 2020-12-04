using Catalog.Domain;
using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using Catalog.Tests.Config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static Catalog.Common.Enums;

namespace Catalog.Tests
{
	[TestClass]
	public class ProductInStockUpdateStockEventHandlerTest
	{
		private ILogger<ProductInStockUpdateStockEventHandler> GetLogger
		{
			get
			{
				return new Mock<ILogger<ProductInStockUpdateStockEventHandler>>()
					.Object;
			}
		}

		[TestMethod]
		public void TryToSubstractStockWhenProductHasStock()
		{
			var context = CatalogDbContextInMemory.Get();

			var productInStockID = 1;
			var productId = 1;

			context.Stocks.Add(new ProductInStock
			{
				ProductInStockId = productInStockID,
				ProductId = productId,
				Stock = 1
			});

			context.SaveChanges();

			var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

			handler.Handle(new ProductInStockUpdateStockCommand
			{
				Items = new List<ProductInStockUpdateItem>()
				{
					new ProductInStockUpdateItem
					{
						ProductId = productId,
						Stock = 1,
						Action = ProductInStockAction.Substract
					}
				}
			}, new CancellationToken()).Wait();

		}

		[TestMethod]
		[ExpectedException(typeof(ProductInStockUpdateStockCommandException))]
		public void TryToSubstractStockWhenProductHasntStock()
		{
			var context = CatalogDbContextInMemory.Get();

			var productInStockID = 2;
			var productId = 2;

			context.Stocks.Add(new ProductInStock
			{
				ProductInStockId = productInStockID,
				ProductId = productId,
				Stock = 1
			});

			context.SaveChanges();

			var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

			try
			{
				handler.Handle(new ProductInStockUpdateStockCommand
				{
					Items = new List<ProductInStockUpdateItem>()
				{
					new ProductInStockUpdateItem
					{
						ProductId = productId,
						Stock = 2,
						Action = ProductInStockAction.Substract
					}
				}
				}, new CancellationToken()).Wait();
			}
			catch (AggregateException ae)
			{
				if (ae.GetBaseException() is ProductInStockUpdateStockCommandException)
				{
					throw new ProductInStockUpdateStockCommandException(ae.InnerException?.Message);
				}
			}

		}

		[TestMethod]
		public void TryToAddStockWhenProductExists()
		{
			var context = CatalogDbContextInMemory.Get();

			var productInStockId = 3;
			var productId = 3;

			// Add product
			context.Stocks.Add(new ProductInStock
			{
				ProductInStockId = productInStockId,
				ProductId = productId,
				Stock = 1
			});

			context.SaveChanges();

			var command = new ProductInStockUpdateStockEventHandler(context, GetLogger);
			command.Handle(new ProductInStockUpdateStockCommand
			{
				Items = new List<ProductInStockUpdateItem> {
					new ProductInStockUpdateItem {
						ProductId = productId,
						Stock = 2,
						Action = ProductInStockAction.Add
					}
				}
			}, new CancellationToken()).Wait();

			Assert.AreEqual(context.Stocks.First(x => x.ProductInStockId == productInStockId).Stock, 3);
		}

		[TestMethod]
		public void TryToAddStockWhenProductNotExists()
		{
			var context = CatalogDbContextInMemory.Get();
			var command = new ProductInStockUpdateStockEventHandler(context, GetLogger);

			var productId = 4;

			command.Handle(new ProductInStockUpdateStockCommand
			{
				Items = new List<ProductInStockUpdateItem> {
					new ProductInStockUpdateItem {
						ProductId = productId,
						Stock = 2,
						Action = ProductInStockAction.Add
					}
				}
			}, new CancellationToken()).Wait();

			Assert.AreEqual(context.Stocks.First(x => x.ProductId == productId).Stock, 2);
		}
	}
}
