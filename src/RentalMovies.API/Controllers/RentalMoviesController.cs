using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentalMovies.Application.Movies.AddMovieLike;
using RentalMovies.Application.Movies.CreateMovie;
using RentalMovies.Application.Movies.DeleteMovie;
using RentalMovies.Application.Movies.GetAllMoviesList;
using RentalMovies.Application.Movies.GetMovieDetail;
using RentalMovies.Application.Movies.UpdateMovie;
using RentalMovies.Application.RentalMovies.CreateRentalMovie;
using RentalMovies.Application.RentalMovies.UpdateRentalMovie;

namespace RentalMovies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalMoviesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RentalMoviesController> _logger;

        public RentalMoviesController(IMediator mediator, ILogger<RentalMoviesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{page:int=1}/{pageSize=4}")]
        [AllowAnonymous]
        public async Task<ActionResult<MoviesListVm>> GetAllMovies(int? page = 1, int? pageSize = 4)
        {
            var vm = await _mediator.Send(new GetAllMoviesListQuery() {Page = page, PageSize = pageSize});
            return Ok(vm);
        }

        [HttpGet("MoviesByName/{page:int=1}/{pageSize=8}/{name}")]
        [AllowAnonymous]
        public async Task<ActionResult<MoviesListVm>> GetAllMoviesByName(int? page = 1, int? pageSize = 8,
            string name = null)
        {
            var vm = await _mediator.Send(new GetAllMoviesListQuery() { Page = page, PageSize = pageSize, Filter = name });
            return Ok(vm);
        }

        [HttpGet("GetMovie/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SingleMovieDto>> GetMovie(int id)
        {
            var movieDto = await _mediator.Send(new GetMovieDetailQuery() { MovieId = id });

            return Ok(movieDto);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> Create([FromBody] CreateMovieCommand command)
        {
            var movieId = await _mediator.Send(command);

            return Ok(movieId);
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] UpdateMovieCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteMovieCommand { MovieId = id });

            return NoContent();
        }

        [HttpPost("MovieLike")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<int>> AddLike([FromBody] AddMovieLikeCommand command)
        {
            var movieId = await _mediator.Send(command);

            return Ok(movieId);
        }

        [HttpPut("RentMovie")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RentalMovie([FromBody] RentalMovieCommand command)
        {

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("ReturnMovie")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<int>> ReturnMovie([FromBody] ReturnMovieCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}