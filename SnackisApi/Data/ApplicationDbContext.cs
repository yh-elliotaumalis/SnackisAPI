using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnackisApi.Models;

namespace SnackisApi.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ForumCategory> Categories { get; set; }
    public DbSet<ForumThread> Threads { get; set; }
    public DbSet<ForumPost> Posts { get; set; }
    public DbSet<ForumPostReport> PostReports { get; set; }
    public DbSet<ForumMessage> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
