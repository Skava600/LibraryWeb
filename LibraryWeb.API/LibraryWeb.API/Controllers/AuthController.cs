using LibraryWeb.Contracts.DTO;
using LibraryWeb.Core.Exceptions;
using LibraryWeb.Core.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryWeb.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [HttpPost, Route("Login")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {
                var request = new LoginUserCommand(model);
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (InvalidRequestBodyException ex)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [Authorize]
        [MapToApiVersion("1.0")]
        [HttpPost, Route("Logout")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Logout()
        {
            var request = new LogoutUserCommand();
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
