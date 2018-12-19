using System.Collections.Generic;

namespace tokentest.DataAccess.UserManagement.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
    }
}