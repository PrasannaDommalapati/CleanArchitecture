using Domain.Dto.Authentication;
using FluentResults;
using Interface;
using MediatR;

namespace Application.Commands.Authentication;

public record SignupRequest(SignupDto SignupDto) : IRequest<Result<SignupResponse>>;
public record SignupResponse(string Email, string UserName, string Role);

public class SignupCommandHandler : IRequestHandler<SignupRequest, Result<SignupResponse>>
{
    private readonly IUserManagementService _userManagement;

    public SignupCommandHandler(IUserManagementService userManagement) => _userManagement = userManagement;

    public async Task<Result<SignupResponse>> Handle(SignupRequest request, CancellationToken cancellationToken)
    {
        var (email, userName, password, confirmPassword) = request.SignupDto;

        (SignupResponseDto responseDto, string errorMessage) result = await _userManagement.SignupUserWithDefaultRole(new SignupDto( email, userName, password, confirmPassword), cancellationToken);

        if (string.IsNullOrEmpty(result.errorMessage))
        {
            return Result.Ok(new SignupResponse(result.responseDto.Email, result.responseDto.UserName, result.responseDto.Role));
        }

        return Result.Fail(result.errorMessage);
    }
}