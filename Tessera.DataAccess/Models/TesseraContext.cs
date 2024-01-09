﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tessera.DataAccess.Models;

public partial class TesseraContext : DbContext
{
    public TesseraContext()
    {
    }

    public TesseraContext(DbContextOptions<TesseraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appeal> Appeals { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketSolution> TicketSolutions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-LLV8UN8R;Initial Catalog=Tessera;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appeal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appeals__3214EC07213B0BE3");

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC075AEBB144");

            entity.Property(e => e.Id)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ReportsTo)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .HasConstraintName("FK__Employees__Repor__25869641");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC07E753C0E0");

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.AppointedTo)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Details)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Urgency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WorkStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('InProgress')");

            entity.HasOne(d => d.Appeal).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.AppealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__AppealI__35BCFE0A");

            entity.HasOne(d => d.AppointedToNavigation).WithMany(p => p.TicketAppointedToNavigations)
                .HasForeignKey(d => d.AppointedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__Appoint__34C8D9D1");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__Created__33D4B598");
        });

        modelBuilder.Entity<TicketSolution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TicketSo__3214EC07D0F5AB63");

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CompletedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CompletionStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Details)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Supervisor)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.SupervisorReviewDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TicketSolution)
                .HasForeignKey<TicketSolution>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketSoluti__Id__38996AB5");

            entity.HasOne(d => d.SupervisorNavigation).WithMany(p => p.TicketSolutions)
                .HasForeignKey(d => d.Supervisor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketSol__Super__3B75D760");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC079813D845");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E413DF4732").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Id__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
