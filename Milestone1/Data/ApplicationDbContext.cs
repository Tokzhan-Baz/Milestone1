
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Milestone1.Models;
using Milestone1.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone1.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity, IdentityRole, string>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BookNumbers> BookNumbers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBooks> AuthorBooks { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AuthorBooks>().HasKey(oc => new { oc.AuthorId, oc.BookId });

            builder.Entity<AuthorBooks>()
                .HasOne<Author>(oc => oc.Author)
                .WithMany(o => o.AuthorBooks)
                .HasForeignKey(oc => oc.AuthorId);

            builder.Entity<AuthorBooks>()
                .HasOne<Book>(oc => oc.Book)
                .WithMany(c => c.AuthorBooks)
                .HasForeignKey(oc => oc.BookId);

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=DB.db");
        }
    }
}
