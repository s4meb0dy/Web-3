using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.PL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderService readerService;

        public ReaderController(IReaderService readerService)
        {
            this.readerService = readerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reader>>> GetAllReaders()
        {
            var readers = await readerService.GetAllReadersAsync();
            return Ok(readers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reader>> GetReaderById(int id)
        {
            var reader = await readerService.GetReaderByIdAsync(id);

            if (reader == null)
            {
                return NotFound();
            }

            return Ok(reader);
        }

        [HttpPost]
        public async Task<ActionResult> AddReader([FromBody] ReaderDTO readerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await readerService.AddReaderAsync(readerDTO);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReader(int id, [FromBody] ReaderDTO readerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await readerService.UpdateReaderAsync(id, readerDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReader(int id)
        {
            await readerService.DeleteReaderAsync(id);

            return NoContent();
        }
    }
}
