using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication1.Models;

namespace WebApplication1.Data;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite; Initial Catalog=school");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Student>().ToTable("students");

        modelBuilder.Entity<Student>().Property(p => p.Id).ValueGeneratedOnAdd()
    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__students__3214EC07AEB97193");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.TeL).IsFixedLength();

            entity.HasOne(d => d.Teacher).WithMany(p => p.Students).HasConstraintName("FK_students_Teacher");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
