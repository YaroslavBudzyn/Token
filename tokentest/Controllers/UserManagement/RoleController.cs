using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tokentest.Common.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using tokentest.Services.UserManagement.Classes.Interfaces;
using tokentest.Common.ViewModels.UserManagement.Role;

namespace tokentest.Controllers.UserManagement
{
    [Route("api_v1/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : ControllerBase
    {
        private readonly IResultHelper _result;
        private readonly IRoleService _roleService;

        //Constructor
        public RoleController(
            IResultHelper result,
            IRoleService roleService
        )
        {
            _result = result;
            _roleService = roleService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return await _roleService.GetAll();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _roleService.GetById(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRoleIncomeModel model)
        {
            return !ModelState.IsValid 
                ? await _result.Response(HttpStatusCode.BadRequest, ModelState) 
                : await _roleService.Update(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchWithPaging([FromBody] RoleSearchModel model)
        {
            return !ModelState.IsValid
                ? await _result.Response(HttpStatusCode.BadRequest, ModelState)
                : await _roleService.SearchWithPaging(model);
        }
    }
}