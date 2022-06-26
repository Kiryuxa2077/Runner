using MediatR;
using RunnerScore.Interfaces;

namespace RunnerScore.Commands;

public class ScoreWriteRequest : IRequest
{
    public string Id { get; set; }
    public decimal Score { get; set; }
}

public class ScoreWriteCommandHandler : IRequestHandler<ScoreWriteRequest>
{
    private readonly IUserRepository _userRepository;

    public ScoreWriteCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ScoreWriteRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Id);

        if (user.Score < request.Score)
            user.Score = request.Score;

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}
