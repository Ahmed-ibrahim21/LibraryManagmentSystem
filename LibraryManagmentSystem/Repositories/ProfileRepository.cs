using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        LibraryContext context;
        public ProfileRepository(LibraryContext _context)
        {
            context = _context;
        }
        public bool Add(Profile Profile)
        {
            try
            {
                context.Profile.Add(Profile);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(Profile Profile)
        {
            try
            {
                context.Profile.Remove(Profile);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Profile> GetAll()
        {
            return context.Profile.ToList();
        }

        public Profile GetById(int id)
        {
            return context.Profile.FirstOrDefault(C => C.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public bool Update(Profile Profile)
        {
            try
            {
                context.Profile.Update(Profile);
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
