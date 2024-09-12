using Application.Repositories;
using Common.Exceptions;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using Domain;
using MediatR;

namespace Application.Features.UserAccess.Queries;

public class RefreshTokenQuery : IRequest<ResponseWrapper<AuthenticationResponse>>
{
    public RefreshTokenRequest UserRefreshToken { get; set; }
}

public class RefreshTokenHandler(IUnitOfWork<int> unitOfWork, IJwtManagerRepository jwtManagerRepository)
    : IRequestHandler<RefreshTokenQuery,
        ResponseWrapper<AuthenticationResponse>>
{
    public Task<ResponseWrapper<AuthenticationResponse>> Handle(RefreshTokenQuery request,
        CancellationToken cancellationToken)
    {
        var refreshToken = UserAccessDummyDb.UserRefreshTokens.SingleOrDefault(x =>
            x.Id == request.UserRefreshToken.Id && x.RefreshToken == request.UserRefreshToken.token);

        if (refreshToken is null || refreshToken.Expires < DateTime.UtcNow)
        {
            return Task.FromResult(ResponseWrapper<AuthenticationResponse>.Fail(
                AuthenticationErrors.InvalidToken()));
        }

        var token = jwtManagerRepository.GenerateAccessToken(request.UserRefreshToken.Id.ToString());

        var response = new AuthenticationResponse
        {
            Id = request.UserRefreshToken.Id,
            AccessToken = token,
            RefreshToken = request.UserRefreshToken.token,
        };

        return Task.FromResult(ResponseWrapper<AuthenticationResponse>.Success(response));
    }
}