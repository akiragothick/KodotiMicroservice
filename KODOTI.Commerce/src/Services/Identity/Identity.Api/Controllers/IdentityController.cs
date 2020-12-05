using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
	[ApiController]
	[Route("v1/identity")]
	public class IdentityController : ControllerBase
	{
		private readonly IMediator _mediator;

		public IdentityController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/*
         * {
         *   "firstName": "Ernesto",
         *   "lastName": "Vargas",
         *   "email": "el.vargas@up.edu.pe",
         *   "password": "Akir@Goth35"
         * }
         */
		[HttpPost]
		public async Task<IActionResult> Create(UserCreateCommand command)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(command);

				if (!result.Succeeded)
				{
					return BadRequest(result.Errors);
				}

				return Ok();
			}

			return BadRequest();
		}

		/*
        * {
        *   "email": "el.vargas@up.edu.pe",
        *   "password": "Akir@Goth35"
        * }
        */
		[HttpPost("authentication")]
		public async Task<IActionResult> Authentication(UserLoginCommand command)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(command);

				if (!result.Succeeded)
				{
					return BadRequest("Access denied");
				}

				return Ok(result);
			}

			return BadRequest();
		}
	}
}
