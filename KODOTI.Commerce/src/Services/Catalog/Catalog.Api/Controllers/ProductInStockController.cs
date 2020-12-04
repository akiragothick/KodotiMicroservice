using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
	[ApiController]
	[Route("v1/stocks")]
	public class ProductInStockController : ControllerBase
	{
		private readonly ILogger<DefaultController> _logger;
		private readonly IMediator mediator;

		public ProductInStockController(
			ILogger<DefaultController> logger,
			IMediator mediator)
		{
			_logger = logger;
			this.mediator = mediator;
		}

		[HttpPut]
		public async Task UpdateStock(ProductInStockUpdateStockCommand command)
		{
			await mediator.Publish(command);
		}
	}
}
