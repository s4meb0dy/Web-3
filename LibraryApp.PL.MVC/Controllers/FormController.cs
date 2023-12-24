using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.PL.MVC.Controllers
{
    public class FormController : Controller
    {
        private readonly IFormService formService;

        public FormController(IFormService formService)
        {
            this.formService = formService;
        }
   
        [HttpGet]
        public async Task<ActionResult> GetForms()
        {
            var forms = await this.formService.GetAllFormsAsync();
            return View(forms);
        }

        [HttpGet]
        public ActionResult AddForm() => View();

        [HttpPost]
        public async Task<ActionResult> AddForm(int readerId, int bookId)
        {
            var formDto = new FormDTO() { ReaderId = readerId, BookId = bookId };
            await this.formService.AddFormAsync(formDto);

            return RedirectToAction("GetForms");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteForm(int id)
        {
            await this.formService.DeleteFormAsync(id);
            return RedirectToAction("GetForms");
        }
    }
}
