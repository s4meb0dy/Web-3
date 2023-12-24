namespace LibraryApp.DAL.Entities
{
    public class Form
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public int ReaderId { get; set; }
        public Reader Reader { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
