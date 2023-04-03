using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Contracts.DTO;
using LibraryWeb.Core.Handlers.Commands;
using LibraryWeb.Core.Handlers.Queries;
using LibraryWeb.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWeb.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllBooksQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BookDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var query = new GetBookByIdQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        [ProducesResponseType(typeof(BookDTO), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromBody] CreateOrUpdateBookDTO model)
        {
            try
            {
                var command = new CreateBookCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BookDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Put(int id, [FromBody] CreateOrUpdateBookDTO model)
        {
            try
            {
                var command = new UpdateBookCommand(model, id);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteBookCommand(id);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
