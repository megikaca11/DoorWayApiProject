namespace DoorWayApiProject.Helpers;

using DoorWayApiProject.Entities;
using Microsoft.EntityFrameworkCore;
using DoorWayApiProject.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        var conectionString = "Server=DESKTOP-QV2ID25;Initial Catalog=DoorWayAPIDB;Trusted_Connection=True;";
        // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        options.UseSqlServer(conectionString);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<CheckIn> CheckIn { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<TagValidity> TagValidity { get; set; }
}