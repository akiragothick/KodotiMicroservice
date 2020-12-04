using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.EventHandlers
{
	public class ProductCreateEventHandler : INotificationHandler<ProductCreateCommand>
	{
		private readonly CatalogDbContext context;

		public ProductCreateEventHandler(CatalogDbContext context)
		{
			this.context = context;
		}

		public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
		{
			await context.AddAsync(new Product
			{
				Name = command.Name,
				Description = command.Description,
				Price = command.Price
			});

			await context.SaveChangesAsync();
		}
	}
}
