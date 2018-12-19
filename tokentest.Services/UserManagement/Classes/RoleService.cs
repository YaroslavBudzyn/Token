
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using tokentest.Common.Helpers.Interfaces;
using tokentest.Common.ViewModels.UserManagement.Role;
using tokentest.DataAccess.UserManagement.UnitOfWork.Interfaces;
using tokentest.Services.UserManagement.Classes.Interfaces;

namespace tokentest.Services.UserManagement.Classes
{
    public class RoleService : IRoleService
    {
        private readonly IUmUnitOfWork _umUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IResultHelper _result;

        //Constructor
        public RoleService(
            IUmUnitOfWork umUnitOfWork,
            IMapper mapper,
            IResultHelper result
        )
        {
            _umUnitOfWork = umUnitOfWork;
            _mapper = mapper;
            _result = result;
        }

        /**
         * GetAll
         */
        public async Task<ObjectResult> GetAll()
        {
            var roles = _umUnitOfWork.Roles.GetAll();
            var total = _umUnitOfWork.Roles.Count();
            var result = roles.Select(role => _mapper.Map<RoleViewModel>(role)).ToList();

            return await _result.Response(HttpStatusCode.OK, new
            {
                Data = result,
                Paging = new
                {
                    Total = total,
                    Returned = result.Count
                }
            });
        }

        /**
         * GetById
         */
        public async Task<ObjectResult> GetById(int id)
        {
            var dbRole = _umUnitOfWork.Roles.Get(id);

            if (dbRole == null)
            {
                return await _result.Response(HttpStatusCode.NotFound, new MessageHelper { Message = MessageEnums.NotFound });
            }

            var role = _mapper.Map<RoleViewModel>(dbRole);

            return await _result.Response(HttpStatusCode.OK, role);
        }

        /**
         * Update
         */
        public async Task<ObjectResult> Update(UpdateRoleIncomeModel model)
        {
            var dbRole = _umUnitOfWork.Roles.Get(model.Id);

            if (dbRole == null)
            {
                return await _result.Response(HttpStatusCode.NotFound, new MessageHelper { Message = MessageEnums.NotFound });
            }

            dbRole.Description = model.Description;

            _umUnitOfWork.Roles.Update(dbRole);

            return _umUnitOfWork.Save()
                ? await _result.Response(HttpStatusCode.OK, new MessageHelper { Message = MessageEnums.Ok })
                : await _result.Response(HttpStatusCode.BadRequest, new MessageHelper { Message = MessageEnums.NotUpdated });
        }

        /**
         * SearchWithPaging
         */
        public async Task<ObjectResult> SearchWithPaging(RoleSearchModel model)
        {

            var searchResult = _umUnitOfWork.Roles.Search(model);

            var enumerable = searchResult.ToList();
            var total = enumerable.Count;
            var roles = enumerable
                .OrderBy(u => u.Id)
                .ToList();

            var result = roles.Select(r => _mapper.Map<RoleViewModel>(r)).ToList();

            return await _result.Response(HttpStatusCode.OK, new
            {
                Data = result,
                Paging = new
                {
                    Total = total,
                    Returned = result.Count
                }
            });
        }
    }
}
