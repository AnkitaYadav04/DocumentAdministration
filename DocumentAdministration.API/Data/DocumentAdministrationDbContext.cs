﻿using DocumentAdministration.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocumentAdministration.API.Data
{
    public class DocumentAdministrationDbContext: DbContext
    { 
        public DocumentAdministrationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<DocumentKeywordDetail> DocumentKeywordDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentKeywordDetail>()
            .HasKey(dc => new { dc.DocumentId });

            modelBuilder.Entity<DocumentKeywordDetail>()
                .HasOne(bc => bc.Document)
                .WithMany(b => b.DocumentKeywordDetails)
                .HasForeignKey(bc => bc.DocumentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}