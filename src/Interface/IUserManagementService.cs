using Domain.Dto.Authentication;

namespace Interface;

public interface IUserManagementService
{
    Task<(SignupResponseDto, string)> SignupUserWithDefaultRole(SignupDto signup, CancellationToken cancellationToken);
    Task<(SignInResponseDto, string)> PasswordSignIn(SignInDto signInDto, CancellationToken cancellationToken);
}
