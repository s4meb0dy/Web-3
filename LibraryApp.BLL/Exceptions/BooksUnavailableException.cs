namespace LibraryApp.BLL.Exceptions
{
    public class BooksUnavailableException : Exception
    {
        public BooksUnavailableException(IEnumerable<int> bookIds)
            : base($"Books with ids in [{string.Join(',', bookIds)}] are not available.")
        {

        }
    }

}
