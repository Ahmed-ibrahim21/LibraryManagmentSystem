using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repositories
{
    public interface IProfileRepository
    {
        public List<Profile> GetAll();

        public Profile GetById(int id);

        public bool Add(Profile Profile);
        public bool Update(Profile Profile);
        public bool Delete(Profile Profile);
        public void Save();
    }
}
