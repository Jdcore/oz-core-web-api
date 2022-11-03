using Microsoft.AspNetCore.Mvc;

using Ozon.Core.WebApi.Abstractions.Repositories;
using Ozon.Core.WebApi.Domain;
using Ozon.Core.WebApi.Domain.Exceptions;
using Ozon.Core.WebApi.Domain.ValueObjects;
using Ozon.Core.WebApi.Presentation.Converters;
using Ozon.Core.WebApi.Presentation.Models;

namespace Ozon.Core.WebApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : Controller
{
    private readonly IActorRepository _actorRepository;

    public ActorController(IActorRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _actorRepository = repository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ActorModel>> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Actor actor = await _actorRepository.Get(id, cancellationToken);

            return Ok(actor.ToModel());
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
    public async Task<ActionResult<IEnumerable<ActorModel>>> Find([FromQuery]string filter, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Actor> actors = await _actorRepository.Find(filter, cancellationToken);

            if (actors is null || actors.Count() <= 0)
            {
                return  NotFound();
            }

            return Ok(actors.Select(x => x.ToModel()));
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult<ActorModel>> Create([FromQuery]ActorCreateModel createModel, CancellationToken cancellationToken)
    {
        try
        {
            Actor actor = createModel.ToDomain();
            await _actorRepository.Save(actor, cancellationToken);

            return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor.ToModel());
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

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ActorModel>> AddRate(Guid id, [FromQuery]int rate, CancellationToken cancellationToken)
    {
        try
        {
            Actor actor = await _actorRepository.Get(id, cancellationToken);

            actor.AddRate(new Rate(rate));

            await _actorRepository.Save(actor, cancellationToken);

            return AcceptedAtAction(nameof(Get), new { id = actor.Id }, actor.ToModel());
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