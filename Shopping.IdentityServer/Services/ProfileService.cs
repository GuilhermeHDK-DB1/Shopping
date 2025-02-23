using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Shopping.IdentityServer.Model;

namespace Shopping.IdentityServer.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

    public ProfileService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _claimsFactory = claimsFactory;
    }
    
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        string id = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(id);
        
        var userClaims = await _claimsFactory.CreateAsync(user);
        var claims = userClaims.Claims.ToList();
        
        claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

        if (_userManager.SupportsUserRole)
        {
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
                if (_roleManager.SupportsRoleClaims)
                {
                    var identityRole = await _roleManager.FindByIdAsync(role);

                    if (identityRole != null)
                    {
                        claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                    }
                }
            }
        }
        
        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        string id = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(id);
        
        context.IsActive = user != null;
    }
}