﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PokeApiManagement.EntitiesDB;

namespace PokeApiManagement.DbContext
{
    public partial class PokeApiContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PokeApiContext()
        {
        }

        public PokeApiContext(DbContextOptions<PokeApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PokeStatisticsByInitial> PokeStatisticsByInitial { get; set; }
        public virtual DbSet<Pokemon> Pokemon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4F9R5IV\\SQLEXPRESS;Initial Catalog=PokeApi;User ID=sergio;Password=milla");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokeStatisticsByInitial>(entity =>
            {
                entity.Property(e => e.initial).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}