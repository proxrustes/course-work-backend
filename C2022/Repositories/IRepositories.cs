using System.Collections.Generic;

namespace DBAccess.Repositories
{
    public interface IRepositories<Model> where Model : class
    {
        Model Get(int id);
        List<Model> GetAll();
        void Add(Model model);
        void Update(Model model);
        void Delete(Model model);

    }
}
