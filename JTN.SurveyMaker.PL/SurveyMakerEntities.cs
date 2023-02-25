﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JTN.SurveyMaker.PL;

public partial class SurveyMakerEntities : DbContext
{
    public SurveyMakerEntities()
    {
    }

    public SurveyMakerEntities(DbContextOptions<SurveyMakerEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<tblAnswer> tblAnswers { get; set; }

    public virtual DbSet<tblQuestion> tblQuestions { get; set; }

    public virtual DbSet<tblQuestionAnswer> tblQuestionAnswers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JTN.SurveyMaker.DB;Integrated Security=true");
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblAnswe__3214EC0761FB954B");

            entity.ToTable("tblAnswer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Answer)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC07DF277E2A");

            entity.ToTable("tblQuestion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Question)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC0734D6E269");

            entity.ToTable("tblQuestionAnswer");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Answer).WithMany(p => p.tblQuestionAnswers)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkAnswerId");

            entity.HasOne(d => d.Question).WithMany(p => p.tblQuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkQuestionId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
