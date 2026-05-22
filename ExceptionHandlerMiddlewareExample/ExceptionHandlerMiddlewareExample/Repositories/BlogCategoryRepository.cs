using ExceptionHandlerMiddlewareExample.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ExceptionHandlerMiddlewareExample.Repositories
{
    public interface IBlogCategoryRepository
    {
        Task AddBlogCategoryAsync(BlogCategory blogCategory);
        Task UpdateBlogCategoryAsync(BlogCategory blogCategory);
        Task DeleteBlogCategoryAsync(Guid blogCategoryId);
        Task<BlogCategory?> GetByIdAsync(Guid blogCategoryId);
        Task<BlogCategory?> GetBySlugAsync(string blogCategorySlug);
        Task<bool> IsNameOrSlugAlreadyOccupied(string name, string categorySlug, Guid? existingBlogCategoryId);
        Task<bool> IsBlogCategoryExists(Guid blogCategoryId);
    }
    public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(AppDbContext context) : base(context) { }

        public async Task AddBlogCategoryAsync(BlogCategory blogCategory)
        {
            await _context.Set<BlogCategory>().AddAsync(blogCategory);
            await SaveChangesAsync();
        }

        public async Task UpdateBlogCategoryAsync(BlogCategory blogCategory)
        {
            var exists = await _context.Set<BlogCategory>().AnyAsync(c => c.BlogCategoryId == blogCategory.BlogCategoryId);
            if (exists)
            {
                _context.Set<BlogCategory>().Update(blogCategory);
                await SaveChangesAsync();
            }
        }

        public async Task DeleteBlogCategoryAsync(Guid blogCategoryID)
        {
            var blogCategory = await _context.Set<BlogCategory>().FirstOrDefaultAsync(c => c.BlogCategoryId == blogCategoryID);
            if (blogCategory != null)
            {

                _context.Set<BlogCategory>().Remove(blogCategory);
                await SaveChangesAsync();
            }
        }
        public async Task<bool> IsNameOrSlugAlreadyOccupied(string name, string categorySlug, Guid? existingBlogCategoryId)
        {
            var query = _context.Set<BlogCategory>().AsQueryable();

            if (true)
            {
                if (existingBlogCategoryId != null)
                {
                    // Check if there's any other record with the same name or slug
                    return await query.AnyAsync(x =>
                        (x.Name.ToLower() == name.ToLower() || x.CategorySlug.ToLower() == categorySlug.ToLower()) &&
                        x.BlogCategoryId != existingBlogCategoryId);
                }

                // Check if there's any record with the same name or slug
                return await query.AnyAsync(x =>
                    x.Name == name || x.CategorySlug == categorySlug);
            }
            return false;
        }

        public override async Task<BlogCategory?> GetByIdAsync(Guid blogCategoryId)
        {
            return await _context.Set<BlogCategory>().FirstOrDefaultAsync(c => c.BlogCategoryId == blogCategoryId);
        }
        public async Task<BlogCategory?> GetBySlugAsync(string blogCategorySlug)
        {
            return await _context.Set<BlogCategory>().FirstOrDefaultAsync(c => c.CategorySlug.ToLower() == blogCategorySlug.ToLower());
        }
        public async Task<bool> IsBlogCategoryExists(Guid blogCategoryId)
        {
            return await _context.Set<BlogCategory>().AnyAsync(c => c.BlogCategoryId == blogCategoryId);
        }
    }
}
