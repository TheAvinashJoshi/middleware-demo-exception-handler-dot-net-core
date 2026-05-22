using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExceptionHandlerMiddlewareExample.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<BlogCategory> BlogCategories => Set<BlogCategory>();
        public DbSet<UserBlog> UserBlogs => Set<UserBlog>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureModel(modelBuilder);
        }

        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<BlogCategory>().HasIndex(b => b.CategorySlug).IsUnique();

            modelBuilder.Entity<UserBlog>().HasIndex(b => b.Slug).IsUnique();
            modelBuilder.Entity<UserBlog>().Property(b => b.Keywords).HasColumnType("text[]");
            modelBuilder.Entity<UserBlog>().Property(b => b.Tags).HasColumnType("text[]");
            modelBuilder.Entity<UserBlog>().HasOne(b => b.BlogCategory).WithMany(c => c.UserBlogs).HasForeignKey(b => b.BlogCategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }

 
        public class AuditableEntity
        {
            public bool IsActive { get; set; } = true;
            public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
            public Guid? CreatedBy { get; set; }
            public DateTime? UpdationDate { get; set; } = DateTime.UtcNow;
            public Guid? UpdatedBy { get; set; }
        }

        public class BlogCategory : AuditableEntity
        {
            public Guid BlogCategoryId { get; set; }
            public required string Name { get; set; }
            public required string Description { get; set; }
            public string? CategoryImageUrl { get; set; }
            public required string CategorySlug { get; set; }
            public ICollection<UserBlog>? UserBlogs { get; set; }
        }
        public class UserBlog : AuditableEntity
        {
            [Key]
            public required Guid UserBlogId { get; set; }
            public required string BlogTitle { get; set; }
            public required string Slug { get; set; }
            public required string Summary { get; set; }
            public required string BlogThumbnailUrl { get; set; }
            public required Guid BlogCategoryId { get; set; }
            public BlogCategory BlogCategory { get; set; }
            public required string BlogContent { get; set; }
            public required string AuthorId { get; set; }
            public bool IsPublished { get; set; }
            public DateTime? PublishedOn { get; set; }
            public int? ReadingTime { get; set; }

            // SEO
            public string? MetaTitle { get; set; }
            public string? MetaDescription { get; set; }
            public string? CanonicalUrl { get; set; }
            public string? OgTitle { get; set; }
            public string? OgDescription { get; set; }
            public string? OgImageUrl { get; set; }
            public string? TwitterCardType { get; set; }
            public string[]? Keywords { get; set; }
            public string[]? Tags { get; set; }       

    }



}