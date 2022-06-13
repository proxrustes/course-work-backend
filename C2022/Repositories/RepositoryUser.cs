using DBAccess.Contexts;
using DBAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DBAccess.Repositories
{
    public class RepositoryUser : IRepositories<User>
    {
        private readonly ApplicationDbContext dbContext;
        public RepositoryUser(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public void Add(User model)
        {
            dbContext.Add(model);
            dbContext.SaveChanges();
        }

        public void Delete(User model)
        {
            var person = dbContext.Users.Where(x => x == model).FirstOrDefault();
            if (person != null)
            {
                dbContext.Users.Remove(person);
                dbContext.SaveChanges();
            }
        }

        public User Get(int id)
        {
            User? toReturn = dbContext.Users
                .Where(x => x.userId == id)
                .FirstOrDefault();

            if (toReturn != null)
            {
                toReturn.characters = null;
                var characters = dbContext.Characters.Where(x => x.userId == id);
                foreach (var item in characters)
                {
                    toReturn.characters.Add(item);
                }
            }
            return toReturn;
        }

        public List<User> GetAll()
        {
            return dbContext.Users.ToList();
        }

        public void Update(User model)
        {
            var result = dbContext.Users.SingleOrDefault(b => b.userId == model.userId);
            if (result != null)
            {
                result.userName = model.userName;

                result.bio = model.bio;

                result.imageLink = model.imageLink;

                result.password = model.password;
                dbContext.SaveChanges();
            }
        }
        
    }
}
