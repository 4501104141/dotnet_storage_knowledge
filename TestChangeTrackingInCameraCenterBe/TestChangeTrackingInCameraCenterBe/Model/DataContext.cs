using Microsoft.EntityFrameworkCore;

namespace CameraCenterBe.Model;

public class DataContext : DbContext
{
    public static string configSql = "Host=dev.smartlook.com.vn:30000;Database=db_camera_center;Username=postgres;Password=stvg"; // để ý Database.Migrate khi trỏ đến database khác

    public DbSet<SqlUser>? users { get; set; }
    public DbSet<SqlRole>? roles { get; set; }
    public DbSet<SqlCamera>? cameras { get; set; }
    public DbSet<SqlCustomer>? customers { get; set; }
    public DbSet<SqlGroup>? groups { get; set; }
    public DbSet<SqlGroupCamera>? groupCameras { get; set; }
    public DbSet<SqlUserGroup>? userGroups { get; set; }
    public DbSet<SqlFile>? files { get; set; }
    public DbSet<SqlCameraUser>? cameraUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(configSql);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SqlRole>().HasMany<SqlUser>(s => s.users).WithOne(s => s.role);
        modelBuilder.Entity<SqlUser>().HasMany<SqlUserGroup>(s => s.userGroups).WithOne(s => s.user);
        modelBuilder.Entity<SqlGroup>().HasMany<SqlUserGroup>(s => s.userGroups).WithOne(s => s.group);
        modelBuilder.Entity<SqlGroup>().HasMany<SqlGroupCamera>(s => s.groupCameras).WithOne(s => s.group);
        modelBuilder.Entity<SqlCamera>().HasMany<SqlGroupCamera>(s => s.groupCameras).WithOne(s => s.camera);
        modelBuilder.Entity<SqlCustomer>().HasMany<SqlGroup>(s => s.groups).WithOne(s => s.customer);
        modelBuilder.Entity<SqlCustomer>().HasMany<SqlUser>(s => s.users).WithOne(s => s.customer);
        modelBuilder.Entity<SqlCustomer>().HasMany<SqlCamera>(s => s.cameras).WithOne(s => s.customer);
        modelBuilder.Entity<SqlCamera>().HasMany<SqlCameraUser>(s => s.cameraUsers).WithOne(s => s.camera);
        modelBuilder.Entity<SqlUser>().HasMany<SqlCameraUser>(s => s.cameraUsers).WithOne(s => s.user);
    }
}
