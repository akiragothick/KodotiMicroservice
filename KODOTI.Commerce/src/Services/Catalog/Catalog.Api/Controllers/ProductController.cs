using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries;
using Catalog.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
	[ApiController]
	[Route("products")]
	public class ProductController : ControllerBase
	{
		private readonly ILogger<DefaultController> _logger;
		private readonly IProductQueryService productQueryService;
		private readonly IMediator mediator;

		public ProductController(
			ILogger<DefaultController> logger,
			IProductQueryService productQueryService,
			IMediator mediator)
		{
			_logger = logger;
			this.productQueryService = productQueryService;
			this.mediator = mediator;
		}

		[HttpGet]
		public async Task<DataCollection<ProductDto>> GetAll(int page = 1, int take = 10, string ids = null)
		{
			IEnumerable<int> products = null;

			if (!string.IsNullOrEmpty(ids))
			{
				products = ids.Split(',').Select(x => Convert.ToInt32(x));
			}

			return await productQueryService.GetAllAsync(page, take, products);
		}

		[HttpGet("{id}")]
		public async Task<ProductDto> Get(int id)
			=> await productQueryService.GetAsync(id);

		[HttpPost]
		public async Task Create(ProductCreateCommand command)
			=> await mediator.Publish(command);
	}
}
