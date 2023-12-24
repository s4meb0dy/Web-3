using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;

namespace LibraryApp.PL
{
    public class Application
    {
        private readonly IReaderService readerService;
        private readonly IBookService bookService;
        private readonly IFormService formService;
        private readonly ILibraryService libraryService;
        private readonly ICatalogService catalogService;

        public Application(IReaderService readerService, IBookService bookService, IFormService formService, ILibraryService libraryService, ICatalogService catalogService)
        {
            this.readerService = readerService;
            this.bookService = bookService;
            this.formService = formService;
            this.libraryService = libraryService;
            this.catalogService = catalogService;
        }

        public async Task Start()
        {
            var option = RenderMenu();

            var optionIsIntType = Int32.TryParse(option, out var value);

            if (optionIsIntType)
            {
                await DefineSolveStrategy(value);
            }

            Console.WriteLine("\nERROR: Incorrect option selected!");
            Thread.Sleep(2500);

            await this.Start();
        }

        private string RenderMenu()
        {
            Console.Clear();
            Console.WriteLine("Select option");
            Console.WriteLine("<1> Add new READER");
            Console.WriteLine("<2> Add new BOOK");
            Console.WriteLine("<3> Add new FORM");
            Console.WriteLine("<4> Get all READERS");
            Console.WriteLine("<5> Get all BOOKS");
            Console.WriteLine("<6> Get all FORMS");
            Console.WriteLine("<7> Borrow");
            Console.WriteLine("<8> Return");
            Console.WriteLine("<9> Search");
            Console.WriteLine("<0> Exit");
            Console.Write("[YOUR CHOICE] Option: ");
            
            var option = Console.ReadLine();

            return option;
        }

        private async Task DefineSolveStrategy(int option)
        {
            switch (option)
            {
                case 1:
                    await AddReader();

                    break;
                case 2:
                    await AddBook();

                    break;
                case 3:
                    await AddFormManually();

                    break;
                case 4:
                    await GetAllReaders();

                    break;
                case 5:
                    await GetAllBooks();

                    break;
                case 6:
                    await GetAllForms();

                    break;
                case 7:
                    await Borrow();

                    break;
                case 8:
                    await ReturnBook();

                    break;
                case 9:
                    await SearchByCriterias();

                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

            Console.WriteLine("\n<---SUCCESS--->");
            Thread.Sleep(2500);

            await this.Start();
        }

        private async Task SearchByCriterias()
        {
            Console.Clear();
            Console.WriteLine("<-Search->");
            Console.Write("Criteria 1 (or enter to ignore) ---- title: ");
            var titleToSearch = Console.ReadLine();

            Console.Write("Criteria 2 (or enter to ignore) ---- author: ");
            var authorToSearch = Console.ReadLine();

            Console.Write("Criteria 3 (or enter to ignore) ---- year: ");
            var yearToSearch = Console.ReadLine();

            var filteredBooks = await this.catalogService.SearchBooksAsync(
                string.IsNullOrWhiteSpace(titleToSearch) ? null : titleToSearch,
                string.IsNullOrWhiteSpace(authorToSearch) ? null : authorToSearch,
                string.IsNullOrWhiteSpace(yearToSearch) ? null : int.Parse(yearToSearch));

            foreach (var book in filteredBooks)
            {
                Console.WriteLine($"\nID = {book.Id}\nTitle = {book.Title}\nAuthor = {book.Author}\nYear = {book.Year}\nIs Available = {book.IsAvailable}\n");
            }
        }

        private async Task ReturnBook()
        {
            Console.Clear();
            Console.WriteLine("<-Return->");
            Console.Write("Book ID to return = ");
            var bookId = int.Parse(Console.ReadLine());

            await this.libraryService.ReturnBookAsync(bookId);
        }

        private async Task Borrow()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("<-Borrow->");
                Console.Write("Reader ID = ");
                var readerId = int.Parse(Console.ReadLine());

                Console.Write("Book IDs (space between digits) = ");
                var booksId = Console.ReadLine().Split(" ");
                var intBooksIds = booksId.Select(x => int.Parse(x)).ToList();

                await this.libraryService.BorrowBooksAsync(readerId, intBooksIds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        private async Task GetAllForms()
        {
            Console.Clear();
            Console.WriteLine("<-All FORMS->");

            var forms = await this.formService.GetAllFormsAsync();

            foreach (var form in forms)
            {
                Console.WriteLine($"ID = {form.Id}\nReader ID = {form.ReaderId}\nReader full name = {form.Reader.FullName}\nBook ID = {form.BookId}\nBook title = {form.Book.Title}");
            }
        }

        private async Task GetAllBooks()
        {
            Console.Clear();
            Console.WriteLine("<-All BOOKS->");

            var books = await this.bookService.GetAllBooksAsync();

            foreach (var book in books)
            {
                Console.WriteLine($"ID = {book.Id}\nTitle = {book.Title}\nAuthor = {book.Author}\nYear = {book.Year}\nIs Available = {book.IsAvailable}\n");
            }
        }

        private async Task GetAllReaders()
        {
            Console.Clear();
            Console.WriteLine("<-All READERS->");

            var readers = await this.readerService.GetAllReadersAsync();

            foreach (var reader in readers)
            {
                Console.WriteLine($"ID = {reader.Id}\nFull name = {reader.FullName}\nAddress = {reader.Address}\n");
            }

            Console.Write("\nEnter any symbol to main menu: ");
            Console.ReadLine();
        }

        private async Task AddFormManually()
        {
            Console.Clear();
            Console.WriteLine("<-Add new FORM->");

            var formDto = new FormDTO();
            Console.Write("Form Reader ID: ");
            formDto.ReaderId = int.Parse(Console.ReadLine());
            Console.Write("Form Book ID: ");
            formDto.BookId = int.Parse(Console.ReadLine());
            formDto.BorrowDate = DateTime.UtcNow;

            await this.formService.AddFormAsync(formDto);
        }

        private async Task AddBook()
        {
            var bookDto = new BookDTO();

            Console.Clear();
            Console.WriteLine("<-Add new READER->");

            Console.Write("Book title: ");
            var title = Console.ReadLine();

            Console.Write("Book author: ");
            var author = Console.ReadLine();

            Console.Write("Book year: ");
            var year = Console.ReadLine();

            var yearIsIntType = Int32.TryParse(year, out var value);
            if (yearIsIntType)
            {
                bookDto.Title = title;
                bookDto.Author = author;
                bookDto.Year = value;
                bookDto.IsAvailable = true;

                await this.bookService.AddBookAsync(bookDto);
            }
        }

        private async Task AddReader()
        {
            var readerDto = new ReaderDTO();

            Console.Clear();
            Console.WriteLine("<-Add new READER->");

            Console.Write("Reader fullname: ");
            var fullName = Console.ReadLine();

            Console.Write("Reader address: ");
            var address = Console.ReadLine();

            readerDto.Address = address;
            readerDto.FullName = fullName;

            await this.readerService.AddReaderAsync(readerDto);
        }
    }
}
