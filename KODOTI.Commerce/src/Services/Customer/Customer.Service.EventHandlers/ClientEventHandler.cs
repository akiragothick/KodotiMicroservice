using Customer.Domain;
using Customer.Persistence.Database;
using Customer.Service.EventHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.EventHandlers
{
	public class ClientEventHandler : INotificationHandler<ClientCreateCommand>
	{
		private readonly CustomerDbContext context;

		public ClientEventHandler(
			CustomerDbContext context)
		{
			this.context = context;
		}

		public async Task Handle(ClientCreateCommand notification, CancellationToken cancellationToken)
		{
			await context.AddAsync(new Client
			{
				Name = notification.Name
			});

			await context.SaveChangesAsync();
		}
	}
}
