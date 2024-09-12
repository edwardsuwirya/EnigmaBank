using Application.Repositories;
using Common.Exceptions;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using Domain;
using MediatR;

namespace Application.Features.UserAccess.Queries;

public class AuthenticateUserQuery : IRequest<ResponseWrapper<AuthenticationResponse>>
{
    public UserAuthentication UserAuthentication { get; set; }
}

public class AuthenticateUserQueryHandler(IUnitOfWork<int> unitOfWork, IJwtManagerRepository jwtManagerRepository)
    : IRequestHandler<AuthenticateUserQuery,
        ResponseWrapper<AuthenticationResponse>>
{
    public Task<ResponseWrapper<AuthenticationResponse>> Handle(AuthenticateUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = UserAccessDummyDb.Users.SingleOrDefault(x =>
            x.Username == request.UserAuthentication.UserName && x.Password == request.UserAuthentication.Password);

        if (user is null)
        {
            return Task.FromResult(ResponseWrapper<AuthenticationResponse>.Fail(
                AuthenticationErrors.InvalidCredential()));
        }

        var token = jwtManagerRepository.GenerateAccessToken(user.Id.ToString());
        var refToken = jwtManagerRepository.GenerateRefreshToken();

        //  for simplicity, The code below is dummy, should store at database level,
        refToken.Id = user.Id;
        refToken.UserName = user.Username;
        UserAccessDummyDb.UserRefreshTokens.Add(refToken);

        var response = new AuthenticationResponse
        {
            Id = user.Id,
            AccessToken = token,
            RefreshToken = refToken.RefreshToken
        };

        return Task.FromResult(ResponseWrapper<AuthenticationResponse>.Success(response));
    }
}