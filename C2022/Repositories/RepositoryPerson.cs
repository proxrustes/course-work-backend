using DBAccess.Contexts;
using DBAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DBAccess.Repositories
{
    public class RepositoryCharacter : IRepositories<Character>
    {
        private readonly ApplicationDbContext dbContext;
        public RepositoryCharacter(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public void Add(Character model)
        {
            using(dbContext)
            {
                dbContext.Add(model);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Character model)
        {
            var person = dbContext.Characters.Where(x => x == model).SingleOrDefault();
            if (person != null)
            {
                dbContext.Characters.Remove(person);
                dbContext.SaveChanges();
            }
        }

        public Character Get(int id)
        {
            try
            {
                return dbContext.Characters.Where(x => x.characterId == id).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<Character> GetAll()
        {
            return dbContext.Characters.ToList();
        }

        public void Update(Character model)
        {
            var result = dbContext.Characters.SingleOrDefault(b => b.characterId == model.characterId);
            if (result != null)
            {
                try
                {
                    result.firstName = model.firstName;
                    result.lastName = model.lastName;
                    result.dexterity = model.dexterity;
                    result.wisdom = model.wisdom;
                    result.charisma = model.charisma;
                    result.intellect = model.intellect;
                    result.race = model.race;
                    result.strength = model.strength;
                    result.imageLink =  model.imageLink;

                    dbContext.SaveChanges();
                }
                catch { }
            }
        }
    }
}
