namespace ITKT_PROJEKTAS.Helpers;

using Microsoft.EntityFrameworkCore;
using ITKT_PROJEKTAS.Entities;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql(connectionString: @"server=localhost;database=ktprojektasdb;uid=root;password=;",
                new MySqlServerVersion(new Version(10, 4, 25)));
    }
}