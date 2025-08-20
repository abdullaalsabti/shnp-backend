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
    public DbSet<Restaurant> Users { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentUrl> DocumentUrls { get; set; }
    public DbSet<WorkingDetail> WorkingDetails { get; set; }
    public DbSet<RestaurantCount> ResaurantCounts { get; set; }

    public DbSet<DocumentType> DocumentTypeCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(_connectionString, options => options.EnableRetryOnFailure());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TutorialShnp");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContextEf).Assembly);

        //many-to-many: Restaurant <=> RestaurantType
        // modelBuilder.Entity<User>().HasMany(r => r.RestaurantTypes).WithMany(rt => rt.Users);
        // THIS IS DONE AUTOMATICALLY BY EF UNLESS WE WANT TO SPECIFY COLUMNS, TABLE NAME ETC.
    }
}
