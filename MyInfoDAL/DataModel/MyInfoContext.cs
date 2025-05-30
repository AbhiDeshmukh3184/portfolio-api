﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyInfoDAL.ViewModel;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MyInfoDAL.DataModel;

public partial class MyInfoContext : DbContext, IMyInfoContext
{
    //public MyInfoContext()
    //{
    //}

    public MyInfoContext(DbContextOptions<MyInfoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NewTable> NewTable { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;user=root;password=Abhi@3184;database=my-info", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.42-mysql"));
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<NewTable>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("new_table");

            entity.HasIndex(e => e.id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Message)
                .HasMaxLength(45)
                .HasColumnName("message");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
