using DemoApplication.Areas.Client.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Blog")]
    public class BlogController : Controller
    {
        [HttpGet("blogindex",Name ="client-blog-index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("blogfilter", Name = "client-blog-filter")]
        public async Task<IActionResult> Filter(string? searchBy = null,
          string? search = null,
          [FromQuery] int? categoryId = null, [FromQuery] int? tagId = null)
        {

            return ViewComponent(nameof(BlogPage), new
            {
                searchBy = searchBy,
                search = search,
                categoryId = categoryId,
                tagId = tagId
            });

        }
    }
}
