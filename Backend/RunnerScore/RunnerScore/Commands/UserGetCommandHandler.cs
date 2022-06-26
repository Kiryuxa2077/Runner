using MediatR;
using RunnerScore.Interfaces;
using RunnerScore.Models;

namespace RunnerScore.Commands;

public class UserGetRequest : IRequest<UserGetResponse>
{
    public string Name { get; set; }
}

public class UserGetResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Score { get; set; }
}

public class UserGetCommandHandler : IRequestHandler<UserGetRequest, UserGetResponse>
{
    private readonly IUserRepository _repository;

    public UserGetCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserGetResponse> Handle(UserGetRequest request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByNameAsync(request.Name);

        if (user is null)
            await _repository.AddAsync(User.Of(request.Name));

        user = await _repository.GetByNameAsync(request.Name);

        return new UserGetResponse { Id = user.Id, Name = user.Name, Score = user.Score};
    }
}
