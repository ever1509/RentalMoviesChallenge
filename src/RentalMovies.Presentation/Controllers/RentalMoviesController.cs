using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentalMovies.Application.Movies.Commands.AddMovieLike;
using RentalMovies.Application.Movies.Commands.CreateMovie;
using RentalMovies.Application.Movies.Commands.DeleteMovie;
using RentalMovies.Application.Movies.Commands.UpdateMovie;
using RentalMovies.Application.Movies.Queries.GetAllMoviesList;
using RentalMovies.Application.Movies.Queries.GetMovieDetail;
using RentalMovies.Application.RentalMovies.Commands.CreateRentalMovie;
using RentalMovies.Application.RentalMovies.Commands.UpdateRentalMovie;

namespace RentalMovies.Presentation.Controllers
{
    [Route("api/movies/")]
    [ApiController]
    [EnableCors("AllowRentalMoviesApp")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RentalMoviesController : ControllerBase
    {
        private IMediator _mediator;
        private readonly ILogger<RentalMoviesController> _logger;

        public RentalMoviesController(IMediator mediator, ILogger<RentalMoviesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("all/{page:int=1}/{pageSize=4}")]
        public async Task<ActionResult<MoviesListVm>> GetAllMovies(int? page = 1, int? pageSize = 4)
        {
            var vm = await _mediator.Send(new GetAllMoviesListQuery() { Page = page, PageSize = pageSize });
            return Ok(vm);
        }
        [HttpGet("byname/{page:int=1}/{pageSize=8}/{name?}")]
        public async Task<ActionResult<MoviesListVm>> GetAllMoviesByName(int? page = 1, int? pageSize = 8, string name = null)
        {
            var vm = await _mediator.Send(new GetAllMoviesListQuery() { Page = page, PageSize = pageSize, Filter = name });
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleMovieDto>> GetMovie(int id)
        {
            var movieDto = await _mediator.Send(new GetMovieDetailQuery() { MovieId = id });

            return Ok(movieDto);
        }
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> Create([FromBody] CreateMovieCommand command)
        {
            var movieId = await _mediator.Send(command);

            return Ok(movieId);
        }
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] UpdateMovieCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteMovieCommand { MovieId = id });

            return NoContent();
        }

        [HttpPost("addlike")]
        public async Task<ActionResult<int>> AddLike([FromBody] AddMovieLikeCommand command)
        {
            var movieId = await _mediator.Send(command);

            return Ok(movieId);
        }

        [HttpPut("rentmovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RentalMovie([FromBody] RentalMovieCommand command)
        {

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("returnmovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<int>> ReturnMovie([FromBody] ReturnRentalMovieCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}