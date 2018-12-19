using System.Collections.Generic;
using tokentest.Common.ViewModels.UserManagement.Role;
using tokentest.DataAccess.UserManagement.Entities;
using tokentest.DataAccess.Context;
using System.Linq;
using tokentest.Common.Enums;

namespace tokentest.DataAccess.UserManagement.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private readonly TokentestDbContext _dbContext;

        public RoleRepository(TokentestDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return _dbContext.Roles
                .Where(r => r.Id != (int)RoleEnums.Admin)
                .OrderBy(r => r.Id)
                .ToList();
        }

        public int Count()
        {
            return _dbContext.Roles.Count();
        }

        public IEnumerable<Role> Search(RoleSearchModel model)
        {
            var query = from r in _dbContext.Roles
                .Where(r => r.Id != (int)RoleEnums.Admin)
                        select r;

            if (model.Id != null)
            {
                query = query.Where(u => u.Id.Equals(model.Id));
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(u => u.Name.ToLower().Contains(model.Name.ToLower()));
            }

            return query;
        }

    }
}
