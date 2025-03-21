﻿using Microsoft.EntityFrameworkCore;

namespace TestSchool.Model;

public class DataContext : DbContext
{
    public static string configSql = "Host=localhost:5432;Database=db_efcore_contrains;Username=postgres;Password=postgres";
    public DbSet<SqlSchool>? schools { get; set; }
    public DbSet<SqlStudent>? students { get; set; }
    public DbSet<SqlTeacher>? teachers { get; set; }
    public DbSet<SqlClass>? classes { get; set; }
    public DbSet<SqlStateClass>? stateClasses { get; set; }
    public DbSet<SqlOrder>? orders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(configSql);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SqlSchool>().HasMany<SqlStudent>(s => s.students).WithOne(s => s.school);
        modelBuilder.Entity<SqlSchool>().HasMany<SqlTeacher>(s => s.teachers).WithOne(s => s.school);
        modelBuilder.Entity<SqlClass>().HasMany<SqlStudent>(s => s.students).WithOne(s => s.classs);
        modelBuilder.Entity<SqlStateClass>().HasMany<SqlClass>(s => s.classes).WithOne(s => s.state);
        modelBuilder.Entity<SqlOrder>().HasOne<SqlOrder>(s => s.order).WithMany(s => s.orders);
    }
}
