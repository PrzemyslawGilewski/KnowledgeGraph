using KnowledgeGraph.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<KnowledgeAuthorSource>()
                .HasKey(kas => new { kas.AuthorId, kas.SourceId });
      
            builder.Entity<KnowledgeCategory>()
                .HasIndex(kc => new { kc.Name, kc.UserId })
                .IsUnique();

            builder.Entity<KnowledgeAuthorSource>()
                .HasOne(x => x.Author)
                .WithMany(x => x.AuthorSource)
                .HasForeignKey(x => x.AuthorId);

            builder.Entity<KnowledgeAuthorSource>()
                .HasOne(x => x.Source)
                .WithMany(x => x.AuthorSource)
                .HasForeignKey(x => x.SourceId);

            base.OnModelCreating(builder);
        }

        public DbSet<KnowledgeCategory> KnowledgeCategories { get; set; }
        public DbSet<KnowledgeConcept> KnowledgeConcepts { get; set; }
        public DbSet<KnowledgeContent> KnowledgeContents { get; set; }
        public DbSet<KnowledgeSource> KnowledgeSources { get; set; }
        public DbSet<KnowledgeAuthor> KnowledgeAuthors { get; set; }
        public DbSet<KnowledgeSourceType> KnowledgeSourceTypes { get; set; }
    }
}
