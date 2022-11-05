using Microsoft.EntityFrameworkCore;
using ITKT_PROJEKTAS.Entities;
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
        options.UseMySql(connectionString: @"server=localhost;database=zaza;uid=root;password=;",
            ServerVersion.AutoDetect(@"server=localhost;database=zaza;uid=root;password=;"));
    }

    public DbSet<ITKT_PROJEKTAS.Entities.Route> Route { get; set; }

    public DbSet<ITKT_PROJEKTAS.Entities.Reservation> Reservation { get; set; }
}