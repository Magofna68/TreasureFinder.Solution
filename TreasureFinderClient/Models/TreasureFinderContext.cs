using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TreasureFinder.Models
{
  public class TreasureFinderContext : IdentityDbContext<ApplicationUser>
  {
    public TreasureFinderContext(DbContextOptions options) : base(options) {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}