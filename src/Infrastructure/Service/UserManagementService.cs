using Domain.Dto.Authentication;
using Interface;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Service;

public class UserManagementService : IUserManagementService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserManagementService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<(SignInResponseDto, string)> PasswordSignIn(SignInDto signInDto, CancellationToken cancellationToken)
    {
        var response = new SignInResponseDto(null);
        var identityUser = await _userManager.FindByEmailAsync(signInDto.Email);
        if (identityUser is null)
        {
            return (response, "User doesnot exists!");
        }

        if (!identityUser.EmailConfirmed)
        {
            return (response, "The email address is not confirmed");
        }

        var signInResult = await _signInManager.PasswordSignInAsync(identityUser.UserName,
                                                                    signInDto.Password,
                                                                    signInDto.RememberMe,
                                                                    lockoutOnFailure: false);

        if (signInResult.Succeeded)
        {
            return (new SignInResponseDto(identityUser), signInResult.ToString());
        }
        else
        {
            return (response, signInResult.ToString());
        }
    }

    public async Task<(SignupResponseDto, string)> SignupUserWithDefaultRole(SignupDto signupDto, CancellationToken cancellationToken)
    {
        var response = new SignupResponseDto(string.Empty, string.Empty, string.Empty);
        var (email, userName, password, confirmPassword) = signupDto;

        if (password != confirmPassword)
        {
            return (response, "Password and confirm password should be same");
        }

        // check user exist in db
        var user = await _userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            return (response, "User areleady exists!");
        }

        //Add user to db
        var identityUser = new IdentityUser
        {
            UserName = userName,
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = false
        };

        var identityResult = await _userManager.CreateAsync(identityUser, password);

        if (identityResult.Succeeded)
        {
            string role = _roleManager.Roles.First(x => x.NormalizedName == "USER").Name!;
            //assign the role
            var roleResult = await _userManager.AddToRoleAsync(identityUser, role);
            if (!roleResult.Succeeded)
            {
                var deleteResult = await _userManager.DeleteAsync(identityUser);
                if (!deleteResult.Succeeded)
                {
                    return (response, $"Deleting User failed after adding role failed '{deleteResult.Errors.First().Description}'.");
                }

                return (response, $"User failed to create '{roleResult.Errors.First().Description}'");
            }
            return (new SignupResponseDto(email, userName, role), string.Empty);
        }

        return (response, $"User failed to create, '{identityResult.Errors.First().Description}'");
    }
}
