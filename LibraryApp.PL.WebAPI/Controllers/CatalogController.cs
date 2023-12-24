using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.PL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks([FromQuery] string? title = null, [FromQuery] string? author = null, [FromQuery] int? year = null)
        {
            var books = await catalogService.SearchBooksAsync(title, author, year);

            if (books == null || !books.Any())
            {
                return NotFound();
            }

            return Ok(books);
        }
    }
}
