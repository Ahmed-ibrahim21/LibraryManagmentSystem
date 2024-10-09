using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repositories
{
    public class CheckOutRepository : ICheckOutRepository
    {
        LibraryContext context;
        public CheckOutRepository(LibraryContext _context) 
        {
            context = _context;
        }
        public bool Add(CheckOut CheckOut)
        {
            try
            {
                context.CheckOuts.Add(CheckOut);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(CheckOut CheckOut)
        {
            try
            {
                context.CheckOuts.Remove(CheckOut);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<CheckOut> GetAll()
        {
            return context.CheckOuts.ToList();
        }

        public CheckOut GetById(int id)
        {
            return context.CheckOuts.FirstOrDefault(C => C.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public bool Update(CheckOut CheckOut)
        {
            try
            {
                context.CheckOuts.Update(CheckOut);
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
