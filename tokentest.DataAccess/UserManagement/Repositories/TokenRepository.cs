using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tokentest.DataAccess.Context;
using tokentest.DataAccess.UserManagement.Entities;

namespace tokentest.DataAccess.UserManagement.Repositories
{
    public class TokenRepository : BaseRepository<Token>
    {
        private readonly TokentestDbContext _dbContext;

        public TokenRepository(TokentestDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public IEnumerable<Token> GetAll()
        {
            return _dbContext.Tokens
                .Include(u => u.User)
                .OrderBy(u => u.Id)
                .ToList();
        }

        public Token GetByUserId(int id)
        {
            return _dbContext.Tokens.FirstOrDefault(u => u.UserId.Equals(id));
        }

        public IEnumerable<Token> GetAllByUserId(int id)
        {
            return _dbContext.Tokens.Where(u => u.UserId.Equals(id));
        }

        public User GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Email.Equals(email));
        }

        public void DeleteByToken(string code)
        {
            Token token = _dbContext.Tokens.FirstOrDefault(t => t.Code.Equals(code));

            if (token != null)
            {
                _dbContext.Tokens.Remove(token);
            }
        }

        public Token GetByToken(string token)
        {
            if (token.Contains("Bearer"))
            {
                var code = token.Split("Bearer").Last().Trim();
                return _dbContext.Tokens.Include(u => u.User).FirstOrDefault(t => t.Code.Equals(code));
            }

            if (token.Contains("bearer"))
            {
                var code = token.Split("bearer").Last().Trim();
                return _dbContext.Tokens.Include(u => u.User).FirstOrDefault(t => t.Code.Equals(code));
            }

            return null;
        }

        public Token GetByRefreshToken(string code)
        {
            return _dbContext.Tokens.FirstOrDefault(t => t.RefreshToken.Equals(code));
        }
    }
}
