using Microsoft.AspNetCore.Mvc;

using Ozon.Core.WebApi.Abstractions.Repositories;
using Ozon.Core.WebApi.Domain;
using Ozon.Core.WebApi.Domain.Exceptions;
using Ozon.Core.WebApi.Presentation.Converters;
using Ozon.Core.WebApi.Presentation.Models;

namespace Ozon.Core.WebApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : Controller
{
    private readonly IMovieRepository _movieRepository;
    private readonly IActorRepository _actorRepository;

    public MovieController(IMovieRepository movieRepository, IActorRepository actorRepository)
    {
        (_movieRepository, _actorRepository) = (movieRepository, actorRepository);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovieModel>> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Movie movie = await _movieRepository.Get(id, cancellationToken);

            return Ok(movie.ToModel());
        }
        catch (DataNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieModel>>> Find([FromQuery]string filter, CancellationToken cancellationToken)
    {
        try
        {
            Movie[] movies = (await _movieRepository.Find(filter, cancellationToken)).ToArray();

            if (movies.Length <= 0)
            {
                return NotFound();
            }

            return Ok(movies.Select(s => s.ToModel()));
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieModel>> Create([FromQuery]MovieCreateModel createModel, CancellationToken cancellationToken)
    {
        try
        {
            Movie movie = createModel.ToDomain();

            await _movieRepository.Save(movie, cancellationToken);

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie.ToModel());
        }
        catch (DataNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("{movieId}/actors/{actorId}")]
    public async Task<ActionResult<MovieModel>> AddActor(Guid movieId, Guid actorId, CancellationToken cancellationToken)
    {
        try
        {
            Actor actor = await _actorRepository.Get(actorId, cancellationToken);
            Movie movie = await _movieRepository.Get(movieId, cancellationToken);

            movie.AddActor(actor);

            await _movieRepository.Save(movie, cancellationToken);

            return AcceptedAtAction(nameof(Get), new { id = movie.Id }, movie.ToModel());
        }
        catch (DataNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{movieId}/actors/{actorId}")]
    public async Task<ActionResult<MovieModel>> RemoveActor(Guid movieId, Guid actorId, CancellationToken cancellationToken)
    {
        try
        {
            Actor actor = await _actorRepository.Get(actorId, cancellationToken);
            Movie movie = await _movieRepository.Get(movieId, cancellationToken);

            movie.RemoveActor(actor);

            await _movieRepository.Save(movie, cancellationToken);

            return AcceptedAtAction(nameof(Get), new { id = movie.Id }, movie.ToModel());
        }
        catch (DataNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove([FromQuery]Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Movie movie = await _movieRepository.Get(id, cancellationToken);

            await _movieRepository.Delete(movie, cancellationToken);

            return Ok();
        }
        catch (DataNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}