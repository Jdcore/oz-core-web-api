using Microsoft.AspNetCore.Mvc;

using Ozon.Core.WebApi.Presentation.Models;

namespace Ozon.Core.WebApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : Controller
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ActorModel>> Get(Guid id, CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return Ok(
                    new ActorModel()
                    {
                        Id = id,
                        Name = "Natalie Portman",
                        BirthDate = new DateTimeOffset(new DateTime(1981, 6, 9)),
                        Gender = "W",
                        Rate = 0
                    });
            }, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<ActorModel>> Find([FromQuery]string filter, CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return NotFound();
            }, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<ActorModel>> Create(
        [FromQuery]ActorCreateModel createModel,
        CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return Ok(
                    new ActorModel()
                    {
                        Id = Guid.NewGuid(),
                        Name = createModel.Name,
                        BirthDate = new DateTimeOffset(createModel.BirthDate),
                        Gender = createModel.Gender,
                        Rate = 0
                    });
            }, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ActorModel>> AddRate(Guid id, [FromQuery]int rate, CancellationToken cancellationToken)
    {
        return await Task.Run(
            () =>
            {
                return NotFound();

            },
            cancellationToken);
    }
}