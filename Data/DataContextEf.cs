using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data;

public class DataContextEf : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DataContextEf(IConfiguration config)
    {
        _configuration = config;
        _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
    }

    public DbSet<UserAuth> UserAuth { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }
    public DbSet<RestaurantType> RestaurantTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentUrl> DocumentUrls { get; set; }
    public DbSet<WorkingDetail> WorkingDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(_connectionString, options => options.EnableRetryOnFailure());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TutorialShnp");

        modelBuilder.Entity<UserAuth>().ToTable("UserAuth", "TutorialShnp").HasKey(u => u.UserId);
        modelBuilder.Entity<UserAuth>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<RefreshToken>().ToTable("RefreshToken", "TutorialShnp").HasKey(u => u.TokenId);

        modelBuilder.Entity<RestaurantType>().ToTable("RestaurantTypes", "TutorialShnp").HasKey(u => u.TypeId);
        modelBuilder.Entity<RestaurantType>().HasIndex(rt => rt.Name).IsUnique();
        modelBuilder.Entity<RestaurantType>().HasData(
            new RestaurantType { TypeId = 1, Name = "Fast Food" },
            new RestaurantType { TypeId = 2, Name = "Fine Dining" },
            new RestaurantType { TypeId = 3, Name = "Arabic" }
        );

        modelBuilder.Entity<User>().ToTable("Users", "TutorialShnp").HasKey(u => u.UserId);

        modelBuilder.Entity<Document>().ToTable("Documents", "TutorialShnp").HasKey(u => u.DocumentId);
        modelBuilder.Entity<DocumentUrl>().ToTable("DocumentUrls", "TutorialShnp").HasKey(u => u.UrlId);

        modelBuilder.Entity<WorkingDetail>().ToTable("WorkingDetails", "TutorialShnp").HasKey(u => u.WorkingDetailId);

        //many-to-many: Restaurant <=> RestaurantType
        // modelBuilder.Entity<User>().HasMany(r => r.RestaurantTypes).WithMany(rt => rt.Users);
        // THIS IS DONE AUTOMATICALLY BY EF UNLESS WE WANT TO SPECIFY COLUMNS, TABLE NAME ETC.
    }
}
