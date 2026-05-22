using ExceptionHandlerMiddlewareExample.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlerMiddlewareExample
{
    [ApiController]
    [Route("[controller]")]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogCategoryController(IBlogCategoryRepository blogCategoryRepository)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        [HttpGet(Name = "DeleteCategory")]
        public async Task DeleteCategory(Guid id)
        {
          await _blogCategoryRepository.DeleteBlogCategoryAsync(id);           
        }
    }
}
