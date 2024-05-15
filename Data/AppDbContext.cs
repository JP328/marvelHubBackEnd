using marvelHub.Model;
using Microsoft.EntityFrameworkCore;

namespace marvelHub.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().ToTable("tb_posts");
        modelBuilder.Entity<Theme>().ToTable("tb_themes");
        modelBuilder.Entity<Comment>().ToTable("tb_comments");
        modelBuilder.Entity<User>().ToTable("tb_users");
        modelBuilder.Entity<Report>().ToTable("tb_reports");

        _ = modelBuilder.Entity<Post>()
            .HasOne(_ => _.Theme)
            .WithMany(t => t.Post)
            .HasForeignKey("ThemeId")
            .OnDelete(DeleteBehavior.Cascade);

        _ = modelBuilder.Entity<Post>()
            .HasOne(_ => _.User)
            .WithMany(u => u.Post)
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);

        _ = modelBuilder.Entity<Comment>()
            .HasOne(_ => _.Post)
            .WithMany(p => p.Comment)
            .HasForeignKey("PostId")
            .OnDelete(DeleteBehavior.ClientCascade);

        _ = modelBuilder.Entity<Report>()
            .HasOne(_ => _.User)
            .WithMany(u => u.Report)
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.ClientCascade);

        _ = modelBuilder.Entity<Report>()
            .HasOne(_ => _.Post)
            .WithMany(p => p.Report)
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.ClientCascade);

        _ = modelBuilder.Entity<Report>()
            .HasOne(_ => _.Comment)
            .WithMany(c => c.Report)
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.ClientCascade);


    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Theme> Themes { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var insertedEntries = this.ChangeTracker.Entries()
                                 .Where(x => x.State == EntityState.Added)
                                 .Select(x => x.Entity);

        foreach (var insertedEntry in insertedEntries)
        {
            if (insertedEntry is Auditable auditableEntity)
            {
                auditableEntity.Data = new DateTimeOffset(DateTime.Now, new TimeSpan(-3, 0, 0));
            }
        }

        var modifiedEntries = ChangeTracker.Entries()
                   .Where(x => x.State == EntityState.Modified)
                   .Select(x => x.Entity);

        foreach (var modifiedEntry in modifiedEntries)
        { 
            if (modifiedEntry is Auditable auditableEntity)
            {
                auditableEntity.Data = new DateTimeOffset(DateTime.Now, new TimeSpan(-3, 0, 0));
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
