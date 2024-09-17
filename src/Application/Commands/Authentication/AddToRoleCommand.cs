using Domain;
using Domain.Dto.Authentication;
using Domain.Enums;
using FluentResults;
using Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Authentication;

public record AddToRoleRquest(string Email, string Role) : IRequest<Result<AddToRoleResponse>>;
public record AddToRoleResponse(string Email, bool Succeded, IEnumerable<IdentityError> Error);

public class AddToRoleCommandHandler : IRequestHandler<AddToRoleRquest, Result<AddToRoleResponse>>
{
    private readonly UserManager<IdentityUser> _userManager;

    public AddToRoleCommandHandler(UserManager<IdentityUser> userManager) => _userManager = userManager;

    public async Task<Result<AddToRoleResponse>> Handle(AddToRoleRquest request, CancellationToken cancellationToken)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.Email);

        if(identityUser is null)
        {
            return Result.Fail($"{request.Email} doesnot exist. Check the email address.");
        }

        if (!request.Role.IsDefinedInEnum<Role>())
        {
            return Result.Fail($"{request.Role} is not allowed.");
        }

        var identityResult = await _userManager.AddToRoleAsync(identityUser, request.Role);

        var succeeded = identityResult.Succeeded;
        if (succeeded)
        {
            return Result.Ok(new AddToRoleResponse(request.Email, succeeded, identityResult.Errors));
        }

        return Result.Fail(string.Join(',', identityResult.Errors.Select(x => x.Description)));
    }
}