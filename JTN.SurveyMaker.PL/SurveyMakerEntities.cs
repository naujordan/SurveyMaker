using System;
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

    public virtual DbSet<tblActivation> tblActivations { get; set; }

    public virtual DbSet<tblAnswer> tblAnswers { get; set; }

    public virtual DbSet<tblQuestion> tblQuestions { get; set; }

    public virtual DbSet<tblQuestionAnswer> tblQuestionAnswers { get; set; }

    public virtual DbSet<tblResponse> tblResponses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JTN.SurveyMaker.DB;Integrated Security=true");
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblActivation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblActiv__3214EC074FFD6309");

            entity.ToTable("tblActivation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActivationCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.tblActivations)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkQuestionIdActivationTable");
        });

        modelBuilder.Entity<tblAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblAnswe__3214EC0746591AD9");

            entity.ToTable("tblAnswer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Answer)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC074CA38463");

            entity.ToTable("tblQuestion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Question)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblQuest__3214EC07066CF976");

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

        modelBuilder.Entity<tblResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblRespo__3214EC0768BE31ED");

            entity.ToTable("tblResponse");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Answer).WithMany(p => p.tblResponses)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkAnswerIdResponseTable");

            entity.HasOne(d => d.Question).WithMany(p => p.tblResponses)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkQuestionIdResponseTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
