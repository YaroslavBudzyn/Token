using System;
using tokentest.DataAccess.Context;
using tokentest.DataAccess.UserManagement.Repositories;

namespace tokentest.DataAccess.UserManagement.UnitOfWork.Interfaces
{
    public class UmUnitOfWork : IUmUnitOfWork
    {
        private readonly TokentestDbContext _dbContext;
        private UserRepository _userRepository;
        private RoleRepository _roleRepository;
        private TokenRepository _tokenRepository;

        //Constructor
        public UmUnitOfWork(TokentestDbContext db)
        {
            _dbContext = db;
        }

        public UserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_dbContext));

        public RoleRepository Roles => _roleRepository ?? (_roleRepository = new RoleRepository(_dbContext));

        public TokenRepository Tokens => _tokenRepository ?? (_tokenRepository = new TokenRepository(_dbContext));

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
