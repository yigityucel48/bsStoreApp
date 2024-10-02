﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Repositories.Config;

namespace WebApp.Repositories
{
    public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions options):
            base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
