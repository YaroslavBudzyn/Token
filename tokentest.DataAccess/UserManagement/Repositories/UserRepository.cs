using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using tokentest.Common.Enums;
using tokentest.Common.ViewModels.UserManagement.User;
using tokentest.DataAccess.Context;
using tokentest.DataAccess.UserManagement.Entities;

namespace tokentest.DataAccess.UserManagement.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private readonly TokentestDbContext _dbContext;

        public UserRepository(TokentestDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public new User Get(int id)
        {
            return _dbContext.Users
                .Where(u => u.RoleId != (int)RoleEnums.Admin)
                .Include(r => r.Role)
                .SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users
                .Where(u => u.RoleId != (int)RoleEnums.Admin)
                .Include(r => r.Role)
                .OrderBy(u => u.Id)
                .ToList();
        }

        public int Count()
        {
            return _dbContext.Users.Count(u => u.RoleId != (int)RoleEnums.Admin);
        }

        public User GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Email.Equals(email));
        }

        public User GetByResetPasswordToken(string code)
        {
            return _dbContext.Users.FirstOrDefault(c => c.ResetPasswordToken.Equals(code));
        }

        public IEnumerable<User> Search(UserSearchModel model)
        {
            var query = from u in _dbContext.Users
                    .Where(u => u.RoleId != (int)RoleEnums.Admin)
                    .Include(r => r.Role)
                        select u;

            if (!string.IsNullOrEmpty(model.Phone))
            {
                query = query.Where(u => u.Phone.ToLower().Contains(model.Phone.ToLower()));
            }

            if (model.Id != null)
            {
                query = query.Where(u => u.Id.Equals(model.Id));
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(u => u.Name.ToLower().Contains(model.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                query = query.Where(u => u.Email.ToLower().Contains(model.Email.ToLower()));
            }

            if (model.RoleId != null)
            {
                query = query.Where(u => u.RoleId.Equals(model.RoleId));
            }

            if (!string.IsNullOrEmpty(model.Role))
            {
                query = query.Where(i => i.Role.Name.ToLower().Contains(model.Role.ToLower()));
            }

            return query;
        }
    }
}
