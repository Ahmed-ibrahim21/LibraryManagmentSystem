using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repositories
{
    public class BooksCheckedOutRepository : IBooksCheckedOutRepository
    {
        LibraryContext context;
        public BooksCheckedOutRepository(LibraryContext _context) 
        {
            context = _context;
        }
        public bool Add(BooksCheckedOut bookcheckedout)
        {
            try
            {
                context.BooksCheckedOuts.Add(bookcheckedout);
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(BooksCheckedOut bookcheckedout)
        {
            try
            {
                context.BooksCheckedOuts.Remove(bookcheckedout);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<BooksCheckedOut> GetAll()
        {
            return context.BooksCheckedOuts.ToList();
        }

        public BooksCheckedOut GetById(int BookId,int CheckoutId)
        {
            return context.BooksCheckedOuts.FirstOrDefault(B => B.CheckOutId == CheckoutId && B.BookId == BookId);
        }


        public void Save()
        {
            context.SaveChanges();
        }

        public bool Update(BooksCheckedOut bookcheckedout)
        {
            try
            {
                context.BooksCheckedOuts.Update(bookcheckedout);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
