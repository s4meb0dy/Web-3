using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.PL.MVC.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService readerService;

        public ReaderController(IReaderService readerService)
        {
            this.readerService = readerService;
        }
   
        [HttpGet]
        public async Task<ActionResult> GetReaders()
        {
            var readers = await this.readerService.GetAllReadersAsync();
            return View(readers);
        }

        [HttpGet]
        public ActionResult AddReader() => View();

        [HttpPost]
        public async Task<ActionResult> AddReader(string fullname, string address)
        {
            var readerDto = new ReaderDTO() { FullName = fullname, Address = address };
            await this.readerService.AddReaderAsync(readerDto);

            return RedirectToAction("GetReaders");
        }

        public async Task<ActionResult> DeleteReader(int id)
        {
            await this.readerService.DeleteReaderAsync(id);
            return RedirectToAction("GetReaders");
        }
    }
}
