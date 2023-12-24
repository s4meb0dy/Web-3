using LibraryApp.DAL.Entities;

namespace LibraryApp.BLL.Exceptions
{
    public class ReaderNotFoundException : Exception
    {
        public ReaderNotFoundException(int id) 
            : base($"Reader with ID = {id} hasn't been founded.")
        {

        }
    }

}
