using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repositories
{
    public interface IBooksCheckedOutRepository
    {
        public List<BooksCheckedOut> GetAll();

        public BooksCheckedOut GetById(int id,int CheckoutId);

        public bool Add(BooksCheckedOut bookcheckedout);
        public bool Update(BooksCheckedOut bookcheckedout);
        public bool Delete(BooksCheckedOut bookcheckedout);
        public void Save();
    }
}
