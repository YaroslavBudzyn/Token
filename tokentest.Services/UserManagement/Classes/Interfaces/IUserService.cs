using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tokentest.Common.ViewModels.UserManagement.User;

namespace tokentest.Services.UserManagement.Classes.Interfaces
{
    public interface IUserService
    {
        Task<ObjectResult> GetAll();
        Task<ObjectResult> GetById(int id);
        Task<ObjectResult> Block(int id);
        Task<ObjectResult> UnBlock(int id);
        Task<ObjectResult> Create(UserCreateModel model);
        Task<ObjectResult> Update(UserUpdateModel model);
        Task<ObjectResult> Assign(UserAssignModel model);
        Task<ObjectResult> SearchWithPaging(UserSearchModel model);
        void DeleteExpiredTokens();
    }
}
