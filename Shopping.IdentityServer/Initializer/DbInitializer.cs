using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Shopping.IdentityServer.Configuration;
using Shopping.IdentityServer.Model;
using Shopping.IdentityServer.Model.Context;

namespace Shopping.IdentityServer.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly UserContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(
        UserContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result != null)
            return;
        
        _roleManager
            .CreateAsync(new IdentityRole(IdentityConfiguration.Admin))
            .GetAwaiter()
            .GetResult();
        
        _roleManager
            .CreateAsync(new IdentityRole(IdentityConfiguration.Client))
            .GetAwaiter()
            .GetResult();

        var admin = new ApplicationUser
        {
            UserName = "Guilherme-admin",
            Email = "guilherme@email.com",
            EmailConfirmed = true,
            PhoneNumber = "+55 (44) 99999-9999",
            FirstName = "Guilherme",
            LastName = "Admin",
        };
        
        _userManager.CreateAsync(admin, "Senha123!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
        
        var adminClaims = _userManager
            .AddClaimsAsync(
                admin, 
                new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
                }).Result;
        
        var client = new ApplicationUser
        {
            UserName = "Guilherme-client",
            Email = "guilherme@email.com",
            EmailConfirmed = true,
            PhoneNumber = "+55 (44) 99999-9999",
            FirstName = "Guilherme",
            LastName = "Client",
        };
        
        _userManager.CreateAsync(client, "Senha123!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
        
        var clientClaims = _userManager
            .AddClaimsAsync(
                client, 
                new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
                }).Result;
    }
}