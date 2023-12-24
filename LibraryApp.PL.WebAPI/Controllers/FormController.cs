using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.PL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService formService;

        public FormController(IFormService formService)
        {
            this.formService = formService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Form>>> GetAllForms()
        {
            var forms = await formService.GetAllFormsAsync();
            return Ok(forms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Form>> GetFormById(int id)
        {
            var form = await formService.GetFormByIdAsync(id);

            if (form == null)
            {
                return NotFound();
            }

            return Ok(form);
        }

        [HttpPost]
        public async Task<ActionResult> AddForm([FromBody] FormDTO formDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await formService.AddFormAsync(formDTO);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateForm(int id, [FromBody] FormDTO formDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await formService.UpdateFormAsync(id, formDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteForm(int id)
        {
            await formService.DeleteFormAsync(id);

            return NoContent();
        }
    }
}
