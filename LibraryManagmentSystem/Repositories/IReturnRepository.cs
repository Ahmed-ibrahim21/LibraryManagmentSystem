using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repositories
{
    public interface IReturnRepository
    {
        public List<Return> GetAll();

        public Return GetById(int id);

        public bool Add(Return Return);
        public bool Update(Return Return);
        public bool Delete(Return Return);
        public void Save();
    }
}
