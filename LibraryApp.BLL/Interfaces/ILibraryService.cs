namespace LibraryApp.BLL.Interfaces
{
    public interface ILibraryService
    {
        Task BorrowBooksAsync(int readerId, IEnumerable<int> bookIds);
        Task ReturnBookAsync(int bookId);
    }
}