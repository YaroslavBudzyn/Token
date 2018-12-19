using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tokentest.Common.Helpers.Interfaces;
using tokentest.Common.ViewModels.UserManagement.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using tokentest.Services.UserManagement.Classes.Interfaces;

namespace tokentest.Controllers.UserManagement
{
    [Route("api_v1/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminController : ControllerBase
    {
        private readonly IResultHelper _result;
        private readonly IUserService _userService;

        // Constructor
        public AdminController(
            IResultHelper result,
            IUserService userService
        )
        {
            _result = result;
            _userService = userService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return await _userService.GetAll();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return await _userService.GetById(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateModel model)
        {
            return !ModelState.IsValid
                ? await _result.Response(HttpStatusCode.BadRequest, ModelState)
                : await _userService.Create(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateModel model)
        {
            return !ModelState.IsValid
                ? await _result.Response(HttpStatusCode.BadRequest, ModelState)
                : await _userService.Update(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> SearchWithPaging([FromBody] UserSearchModel model)
        {
            return !ModelState.IsValid
                ? await _result.Response(HttpStatusCode.BadRequest, ModelState)
                : await _userService.SearchWithPaging(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch]
        public async Task<IActionResult> Assign([FromBody] UserAssignModel model)
        {
            return !ModelState.IsValid
                ? await _result.Response(HttpStatusCode.BadRequest, ModelState)
                : await _userService.Assign(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Block(int id)
        {
            return await _userService.Block(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UnBlock(int id)
        {
            return await _userService.UnBlock(id);
        }
    }
}