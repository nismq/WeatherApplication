using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WeatherApplication.Models;

namespace WeatherApplication.Data;

public partial class WeatherdbContext : DbContext
{
    public WeatherdbContext()
    {
    }

    public WeatherdbContext(DbContextOptions<WeatherdbContext> options) : base(options){ }

    public virtual DbSet<Locality> Localities { get; set; }

    public virtual DbSet<Weatherdatum> Weatherdata { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer();*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Locality>(entity =>
        {
            entity.HasKey(e => e.LocalityId).HasName("PK__locality__EBA257B25F0CDFA4");
        });

        modelBuilder.Entity<Weatherdatum>(entity =>
        {
            entity.HasKey(e => e.WeatherdataId).HasName("PK__weatherd__49831657EAC52DD9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Locality).WithMany(p => p.Weatherdata).HasConstraintName("FK__weatherda__local__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
