using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentalMovies.API.Controllers;
using RentalMovies.Application;
using RentalMovies.Application.Movies.AddMovieLike;
using RentalMovies.Application.Movies.CreateMovie;
using RentalMovies.Application.Movies.DeleteMovie;
using RentalMovies.Application.Movies.GetAllMoviesList;
using RentalMovies.Application.Movies.GetMovieDetail;
using RentalMovies.Application.Movies.UpdateMovie;
using RentalMovies.Application.RentalMovies.CreateRentalMovie;
using RentalMovies.Application.RentalMovies.UpdateRentalMovie;
using RentalMovies.Domain.Entities;
using RentalMovies.Domain.Enums;
using Xunit;

namespace RentalMovies.Tests.API
{
    public class RentaMoviesControllerTests
    {
        private readonly IMediator _mediatorFake;
        private readonly ILogger<RentalMoviesController> _loggerFake;
        private const bool HAS_PENALTY = true;
        public RentaMoviesControllerTests()
        {
            _mediatorFake = A.Fake<IMediator>();

            ConfigureMediator(_mediatorFake);

            _loggerFake = A.Fake<ILogger<RentalMoviesController>>();


        }
        [Fact]
        public async Task GetAllMoviesTest()
        {
            var controller = new RentalMoviesController(_loggerFake, _mediatorFake);

            var result = await controller.GetAllMovies();

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MoviesListVm>>(result);

        }

        [Fact]
        public async Task GetAllMoviesByNameTest()
        {
            var controller = new RentalMoviesController(_loggerFake, _mediatorFake);

            var result = await controller.GetAllMoviesByName(1, 8, "Title Movie");

            Assert.NotNull(result);
            Assert.IsType<ActionResult<MoviesListVm>>(result);
        }

        [Fact]
        public async Task GetMovieTest()
        {
            var controller = new RentalMoviesController(_loggerFake, _mediatorFake);

            var result = await controller.GetMovie(1);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<SingleMovieDto>>(result);
        }

        [Fact]
        public async Task CreateMovieTest()
        {
            var controller = new RentalMoviesController(_loggerFake, _mediatorFake);

            var result = await controller.Create(new CreateMovieCommand()
            {
                Title = "Job Title Test",
                Description = "Description Job Test",
                SalePrice = 12.64m,
                RentalPrice = 44.54m,
                Image = "jpg",
                NumberOfStocks = 5
            });

            var r = (OkObjectResult)result.Result;

            Assert.NotNull(result);
            Assert.IsType<ActionResult<int>>(result);
            Assert.Equal(1, r.Value);
        }

        [Fact]
        public async Task UpdateMovieTest()
        {
            var controller = new RentalMoviesController(_loggerFake, _mediatorFake);

            var result = await controller.Update(new UpdateMovieCommand()
            {
                Title = "Job Title Test Updated",
                Description = "Description Job Test Updated",
                SalePrice = 12.64m,
                RentalPrice = 44.54m,
                Image = "Updated.jpg",
            });

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public async Task DeleteMovieTest()
        {
            var controller = new RentalMoviesController(_loggerFake, _mediatorFake);

            var result = await controller.Delete(1);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task RentalMovie()
        {
            var controller = new RentalMoviesController(_loggerFake, _mediatorFake);

            var result = await controller.RentalMovie(new RentalMovieCommand()
            {
                RentalMovies = new List<RentalMovieVm>()
                {
                    new RentalMovieVm()
                    {
                        MovieId = 1,
                        StockId = 1,
                        UserId = 1,
                        StatusMovie = StatusMovie.Rented,
                        Days = 5
                    },
                    new RentalMovieVm()
                    {
                        MovieId = 2,
                        StockId = 1,
                        UserId = 1,
                        StatusMovie = StatusMovie.Rented,
                        Days = 6
                    },
                    new RentalMovieVm()
                    {
                    MovieId = 3,
                    StockId = 1,
                    UserId = 1,
                    StatusMovie = StatusMovie.Rented,
                    Days = 2
                }
                }
            });

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public async Task ReturnMovie()
        {
            var controller = new RentalMoviesController(_loggerFake, _mediatorFake);
            var result = await controller.ReturnMovie(new ReturnMovieCommand()
            {
                MovieId = 1,
                StockId = 2,
                UserId = 1
            });
            var r = (OkObjectResult)result.Result;
            Assert.NotNull(result);
            Assert.IsType<ActionResult<int>>(result);
            Assert.Equal(true, r.Value);
        }
        private void ConfigureMediator(IMediator mediatorFake)
        {
            A.CallTo(() => mediatorFake.Send(A<AddMovieLikeCommand>._, A<CancellationToken>._))
                .Returns(AddMovieLikeFake());
            A.CallTo(() => mediatorFake.Send(A<CreateMovieCommand>._, A<CancellationToken>._))
                .Returns(CreateUpdateMovie());
            A.CallTo(() => mediatorFake.Send(A<DeleteMovieCommand>._, A<CancellationToken>._));
            A.CallTo(() => mediatorFake.Send(A<UpdateMovieCommand>._, A<CancellationToken>._));

            A.CallTo(() => mediatorFake.Send(A<GetAllMoviesListQuery>._, A<CancellationToken>._))
                .Returns(GetMoviesListQueryFake());
            A.CallTo(() => mediatorFake.Send(A<GetMovieDetailQuery>._, A<CancellationToken>._))
                .Returns(GetMovieDetailFake());
            A.CallTo(() => mediatorFake.Send(A<RentalMovieCommand>._, A<CancellationToken>._));
            A.CallTo(() => mediatorFake.Send(A<ReturnMovieCommand>._, A<CancellationToken>._))
                .Returns(ReturnFakeMovie());
        }

        private Task<bool> ReturnFakeMovie()
        {
            return Task.Run(() => { return HAS_PENALTY; });
        }

        private Task<SingleMovieDto> GetMovieDetailFake()
        {
            return Task.Run(() =>
            {
                return new SingleMovieDto()
                {
                    MovieId = 1,
                    Description = "Test Description",
                    Title = "Test Title",
                    SalePrice = 12.5m,
                    RentalPrice = 53.3m,
                    Image = "jpg"

                };
            });
        }

        private Task<MoviesListVm> GetMoviesListQueryFake()
        {
            return Task.Run(() =>
            {
                var moviesList = new MoviesListVm()
                {
                    Movies = new Pagination<MovieDto>()
                    {
                        Items = new List<MovieDto>()
                       {
                           new MovieDto()
                           {
                               MovieId = 1,
                               Description = "Test Description",
                               Title = "Test Title",
                               SalePrice = 12.5m,
                               RentalPrice = 53.3m,
                               Image = "jpg",
                               NumberOfLikes = 4,
                               NumberOfStocks = 3
                           },
                           new MovieDto()
                           {
                               MovieId = 2,
                               Description = "Test Description",
                               Title = "Test Title",
                               SalePrice = 12.5m,
                               RentalPrice = 53.3m,
                               Image = "jpg",
                               NumberOfLikes = 4,
                               NumberOfStocks = 3
                           },
                           new MovieDto()
                           {
                               MovieId = 3,
                               Description = "Test Description",
                               Title = "Test Title",
                               SalePrice = 12.5m,
                               RentalPrice = 53.3m,
                               Image = "jpg",
                               NumberOfLikes = 4,
                               NumberOfStocks = 3
                           }
                       },
                        Page = 5,
                        TotalCount = 10,
                        TotalPages = 200
                    }
                };
                return moviesList;
            });
        }

        private Task<int> CreateUpdateMovie(string title = "")
        {
            return Task.Run(() =>
            {
                var movieLike = new Movie()
                {
                    MovieId = 1,
                    Title = "The Avengers",
                    Image = "EndGame.jpg",
                    RentalPrice = 12.50m,
                    SalePrice = 55.43m,
                    Description = "End Game"
                };
                return movieLike.MovieId;
            });
        }

        private Task<int> AddMovieLikeFake()
        {
            return Task.Run(() =>
            {
                var movieLike = new MovieLike()
                {
                    MovieId = 1,
                    UserId = 1,
                    CreatedDate = DateTime.Now
                };
                return movieLike.MovieId;
            });

        }

    }
}
