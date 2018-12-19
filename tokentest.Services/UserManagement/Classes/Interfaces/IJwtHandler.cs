using tokentest.DataAccess.UserManagement.Entities;

namespace tokentest.Services.UserManagement.Classes.Interfaces
{
    public interface IJwtHandler
    {
        Token Generate(User user, string role);
    }
}
