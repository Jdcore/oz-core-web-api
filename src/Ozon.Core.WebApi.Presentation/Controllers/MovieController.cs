using Microsoft.AspNetCore.Mvc;

using Ozon.Core.WebApi.Presentation.Models;

namespace Ozon.Core.WebApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : Controller
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovieModel>> Get(Guid id, CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return Ok(
                    new MovieModel()
                    {
                        Id = id, Name = "Leon", Year = 1994
                    });
            }, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<MovieModel>> Find([FromQuery]string filter, CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return NotFound();
            }, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<MovieModel>> Create([FromQuery]string name, [FromQuery]int year, CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return Ok(
                    new MovieModel()
                    {
                        Id = Guid.NewGuid(), Name = name, Year = year
                    });
            }, cancellationToken);
    }

    [HttpPost("{movieId}/actors/{actorId}")]
    public async Task<ActionResult<MovieModel>> AddActor(Guid movieId, Guid actorId, CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return NotFound();

            }, cancellationToken);
    }

    [HttpDelete("{movieId}/actors/{actorId}")]
    public async Task<ActionResult<MovieModel>> RemoveActor(Guid movieId, Guid actorId, CancellationToken token)
    {
        return await Task.Run(
            () =>
            {
                return NotFound();

            },
            token);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MovieModel>> Remove([FromQuery]Guid id, CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return NotFound();

            }, cancellationToken);
    }
}