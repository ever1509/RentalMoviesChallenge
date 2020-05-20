using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentalMovies.Application.Movies.Commands.CreateMovie;
using RentalMovies.Application.Movies.Commands.DeleteMovie;
using RentalMovies.Application.Movies.Commands.UpdateMovie;

namespace RentalMovies.Presentation.Controllers
{
    [Route("api/movies/")]
    [ApiController]
    [EnableCors("AllowRentalMoviesApp")]
    public class RentalMoviesController : ControllerBase
    {
        private IMediator _mediator;
        private readonly ILogger<RentalMoviesController> _logger;

        public RentalMoviesController(IMediator mediator, ILogger<RentalMoviesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateMovieCommand command)
        {
            var movieId = await _mediator.Send(command);

            return Ok(movieId);
        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] UpdateMovieCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteMovieCommand { MovieId = id });

            return NoContent();
        }
    }
}