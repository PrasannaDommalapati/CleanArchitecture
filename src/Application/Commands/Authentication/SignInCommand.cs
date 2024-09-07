using Domain.Dto.Authentication;
using FluentResults;
using Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Authentication;

public record SignInRequest(SignInDto SignInDto) : IRequest<Result<SignInResponse>>;
public record SignInResponse(IdentityUser User);

public class SignInCommandHandler : IRequestHandler<SignInRequest, Result<SignInResponse>>
{
    private readonly IUserManagementService _userManagement;

    public SignInCommandHandler(IUserManagementService userManagement) => _userManagement = userManagement;

    public async Task<Result<SignInResponse>> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        var (email, userName, password, rememberMe) = request.SignInDto;

        (SignInResponseDto responseDto, string errorMessage) result = await _userManagement.PasswordSignIn(new SignInDto( email, userName, password, rememberMe), cancellationToken);

        if (string.IsNullOrEmpty(result.errorMessage))
        {
            return Result.Ok(new SignInResponse(result.responseDto.User!));
        }

        return Result.Fail(result.errorMessage);
    }
}
