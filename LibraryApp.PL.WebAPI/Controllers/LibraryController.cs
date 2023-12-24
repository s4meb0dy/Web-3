using LibraryApp.BLL.Exceptions;
using LibraryApp.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.PL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        [HttpPost("borrow")]
        public async Task<ActionResult> BorrowBooks([FromQuery] int readerId, [FromBody] IEnumerable<int> bookIds)
        {
            try
            {
                await libraryService.BorrowBooksAsync(readerId, bookIds);
                return NoContent();
            }
            catch (ReaderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BooksUnavailableException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("return")]
        public async Task<ActionResult> ReturnBook([FromQuery] int bookId)
        {
            try
            {
                await libraryService.ReturnBookAsync(bookId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
