namespace Domain.Dto.Authentication;

public record SignupDto(string Email, string UserName, string Password, string ConfirmPassword);
public record SignupResponseDto(string Email, string UserName, string Role);


