using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tokentest.Common.ViewModels.UserManagement.Role;

namespace tokentest.Services.UserManagement.Classes.Interfaces
{
    public interface IRoleService
    {
        Task<ObjectResult> GetAll(int offset, int limit);
        Task<ObjectResult> GetById(int id);
        Task<ObjectResult> Update(UpdateRoleIncomeModel model);
        Task<ObjectResult> SearchWithPaging(RoleSearchModel model);
    }
}
