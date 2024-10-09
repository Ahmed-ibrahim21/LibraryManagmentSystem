using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repositories
{
    public interface ICheckOutRepository
    {
        public List<CheckOut> GetAll();

        public CheckOut GetById(int id);

        public bool Add(CheckOut CheckOut);
        public bool Update(CheckOut CheckOut);
        public bool Delete(CheckOut CheckOut);
        public void Save();
    }
}
