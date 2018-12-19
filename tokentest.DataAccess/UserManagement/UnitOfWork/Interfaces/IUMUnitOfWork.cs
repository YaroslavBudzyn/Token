using System;
using tokentest.DataAccess.UserManagement.Repositories;

namespace tokentest.DataAccess.UserManagement.UnitOfWork.Interfaces
{
    public interface IUmUnitOfWork : IDisposable
    {
        UserRepository Users { get; }
        RoleRepository Roles { get; }
        TokenRepository Tokens { get; }

        bool Save();
    }
}
