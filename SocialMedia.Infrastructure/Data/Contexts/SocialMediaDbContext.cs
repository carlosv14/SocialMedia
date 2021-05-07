using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Data.Contexts
{
    public class SocialMediaDbContext : DbContext
    {
        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasKey(k => k.Id);
            modelBuilder.Entity<Post>().Property(k => k.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>().Property(p => p.Content).HasMaxLength(500);
            modelBuilder.Entity<Post>().HasMany(p => p.Comments)
                .WithOne(comment => comment.Post)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Comment>().HasKey(k => k.Id);
            modelBuilder.Entity<Comment>().Property(k => k.Id).ValueGeneratedOnAdd();

            //seeding
            modelBuilder.Entity<Post>().HasData(new Post
            {
                Content = "Primer post",
                Date = DateTime.Now,
                Id = -1
            });
        }
    }
}
