using Microsoft.AspNetCore.Identity;

namespace Domain.Dto.Authentication;

public record SignInDto(string Email, string UserName, string Password, bool RememberMe);
public record SignInResponseDto(IdentityUser? User);
