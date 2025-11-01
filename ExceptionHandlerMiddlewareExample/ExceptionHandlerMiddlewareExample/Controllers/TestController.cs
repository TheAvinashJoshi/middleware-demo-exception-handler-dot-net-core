using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlerMiddlewareExample
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {  
        public TestController()
        {
        }

        [HttpGet(Name = "GetTestData")]
        public List<string> Get()
        {
            List<string> names = null;
            // This will throw a NullReferenceException
            var length = names.Count;
            return names;
        }
    }
}
