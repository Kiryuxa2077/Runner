using MediatR;
using Microsoft.AspNetCore.Mvc;
using RunnerScore.Commands;

namespace RunnerScore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{name}")]
    public async Task<UserGetResponse> Get(string name)
    {
        return await _mediator.Send(new UserGetRequest { Name = name });
    }

    [HttpPost("score")]
    public async Task UpdateScore(ScoreWriteRequest request)
    {
        await _mediator.Send(request);
    }
}
