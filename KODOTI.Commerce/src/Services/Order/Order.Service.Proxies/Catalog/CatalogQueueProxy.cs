using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Service.Proxies.Catalog
{
	public class CatalogQueueProxy : ICatalogQueueProxy
    {
        public readonly string connectionString;
        public readonly string queueName = "order-stock-update";

        public CatalogQueueProxy(
            IOptions<AzureServiceBus> azure)
        {
            connectionString = azure.Value.ConnectionString;
        }

        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {
			// send a message to the queue
			await using ServiceBusClient client = new ServiceBusClient(connectionString);

			// create a sender for the queue 
			ServiceBusSender sender = client.CreateSender(queueName);

			// create a message that we can send
			string body = JsonSerializer.Serialize(command);
			var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(body));

			// send the message
			await sender.SendMessageAsync(message);
		}
    }
}
