﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Service.EventHandlers.Commands;
using Order.Service.Queries;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
	[ApiController]
    [Route("v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(
            ILogger<OrderController> logger,
            IMediator mediator,
            IOrderQueryService orderQueryService)
        {
            _logger = logger;
            _mediator = mediator;
            _orderQueryService = orderQueryService;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page = 1, int take = 10)
            => await _orderQueryService.GetAllAsync(page, take);

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
            => await _orderQueryService.GetAsync(id);

        /*
         * 
         * {
         *  "paymentType": 0,
         *  "clientId": 2,
         *  "items" : [
         *      {
         *          "productId": 3,
         *          "quantity": 1,
         *          "price": 100
         *      },
         *      {
         *          "productId": 1,
         *          "quantity": 1,
         *          "price": 400
         *      }
         *  ]
         * }
         */
        [HttpPost]
        public async Task Create(OrderCreateCommand notification)
            => await _mediator.Publish(notification);
    }
}
