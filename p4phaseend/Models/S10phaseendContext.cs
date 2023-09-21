using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace p4phaseend.Models;

public partial class S10phaseendContext : DbContext
{
    public S10phaseendContext()
    {
    }

    public S10phaseendContext(DbContextOptions<S10phaseendContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustLogInfo> CustLogInfos { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:s10phaseend.database.windows.net,1433;Initial Catalog=s10phaseend;User Id=Admin1234;Password=SRajendar@123;Encrypt=True;TrustServerCertificate=True;Connection Timeout=60;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustLogInfo>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__CustLogI__5E548648CD131032");

            entity.ToTable("CustLogInfo");

            entity.Property(e => e.LogId).ValueGeneratedNever();
            entity.Property(e => e.CustEmail).HasMaxLength(100);
            entity.Property(e => e.CustName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.LogStatus).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.CustLogInfos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CustLogIn__UserI__619B8048");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserInfo__1788CC4CC1CA7163");

            entity.ToTable("UserInfo");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
