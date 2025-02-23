using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shopping.IdentityServer.Model.Context;

public class UserContext : IdentityDbContext<ApplicationUser>
{
    public UserContext()
    { }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    { }
}