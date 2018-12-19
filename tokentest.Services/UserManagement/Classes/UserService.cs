using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using tokentest.Common.ApplicationSettings;
using tokentest.Common.Helpers.Interfaces;
using tokentest.Common.ViewModels.UserManagement.User;
using tokentest.DataAccess.UserManagement.Entities;
using tokentest.DataAccess.UserManagement.UnitOfWork.Interfaces;
using tokentest.Services.UserManagement.Classes.Interfaces;

namespace tokentest.Services.UserManagement.Classes
{
    public class UserService : IUserService
    {
        private readonly IUmUnitOfWork _umUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IResultHelper _result;
        private readonly JwtOptions _options;

        //Constructor
        public UserService(
            IUmUnitOfWork umUnitOfWork,
            IMapper mapper,
            IResultHelper result,
            IOptions<JwtOptions> options
        )
        {
            _umUnitOfWork = umUnitOfWork;
            _mapper = mapper;
            _options = options.Value;
            _result = result;
        }

        /**
         * GetAll
         */
        public async Task<ObjectResult> GetAll()
        {
            var dbUsers = _umUnitOfWork.Users.GetAll();
            var total = _umUnitOfWork.Users.Count();
            var usersToMap = new List<UserViewModel>();

            foreach (var dbUser in dbUsers)
            {
                usersToMap.Add(_mapper.Map<UserViewModel>(dbUser));
                var user = usersToMap.SingleOrDefault(m => m.Id == dbUser.Id);

                if (null == user) continue;
                user.Role = dbUser.Role?.Name;
            }

            return await _result.Response(HttpStatusCode.OK, new
            {
                Data = usersToMap,
                Paging = new
                {
                    Total = total,
                    Returned = usersToMap.Count
                }
            });
        }

        
        /**
         * GetById
         */
        public async Task<ObjectResult> GetById(int id)
        {
            var dbUser = _umUnitOfWork.Users.Get(id);

            if (dbUser == null)
            {
                return await _result.Response(HttpStatusCode.NotFound, new MessageHelper { Message = MessageEnums.NotFound });
            }

            var user = _mapper.Map<UserViewModel>(dbUser);
            user.Role = dbUser.Role?.Name;

            return await _result.Response(HttpStatusCode.OK, user);
        }

       
        

        /**
         * Create
         */
        public async Task<ObjectResult> Create(UserCreateModel model)
        {
            if (null != _umUnitOfWork.Users.GetByEmail(model.Email))
            {
                return await _result.Response(HttpStatusCode.Conflict, new MessageHelper { Message = MessageEnums.EmailUsed });
            }

            var user = _mapper.Map<User>(model);
            var password = GenerateRandomString();
            user.RoleId = model.RoleId;

            _umUnitOfWork.Users.Create(user);

            if (!_umUnitOfWork.Save())
            {
                return await _result.Response(HttpStatusCode.BadRequest, new MessageHelper { Message = MessageEnums.NotCreated });
            }

            var to = user.Email;
            var message = "Login: " + user.Email + " \nPassword: " + password;

            return await _result.Response(HttpStatusCode.Created, new MessageHelper { Message = MessageEnums.Created });
        }

        
        /**
         * Update
         */
        public async Task<ObjectResult> Update(UserUpdateModel model)
        {
            var user = _umUnitOfWork.Users.Get(model.Id);

            if (null == user)
            {
                return await _result.Response(HttpStatusCode.NotFound, new MessageHelper { Message = MessageEnums.NotFound });
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;

            _umUnitOfWork.Users.Update(user);

            return _umUnitOfWork.Save()
                ? await _result.Response(HttpStatusCode.OK, new MessageHelper { Message = MessageEnums.Ok })
                : await _result.Response(HttpStatusCode.BadRequest, new MessageHelper { Message = MessageEnums.NotUpdated });
        }

        
        /**
         * Assign
         */
        public async Task<ObjectResult> Assign(UserAssignModel model)
        {
            var user = _umUnitOfWork.Users.Get(model.Id);
            var role = _umUnitOfWork.Roles.Get(model.RoleId);

            if (null == user || null == role)
            {
                return await _result.Response(HttpStatusCode.NotFound, new MessageHelper { Message = MessageEnums.NotFound });
            }

            user.RoleId = model.RoleId;

            _umUnitOfWork.Users.Update(user);

            //logic remove all tokens
            var tokens = _umUnitOfWork.Tokens.GetAllByUserId(user.Id);

            foreach (var token in tokens)
            {
                _umUnitOfWork.Tokens.Delete(token.Id);
            }

            if (_umUnitOfWork.Save())
            {
                return await _result.Response(HttpStatusCode.OK, new MessageHelper { Message = MessageEnums.Ok });
            }
            return await _result.Response(HttpStatusCode.BadRequest, new MessageHelper { Message = MessageEnums.NotUpdated });
        }

        /**
         * SearchWithPaging
         */
        public async Task<ObjectResult> SearchWithPaging(UserSearchModel model)
        {
            var searchResult = _umUnitOfWork.Users.Search(model);

            var enumerable = searchResult.ToList();
            var total = enumerable.Count;
            var users = enumerable
                .ToList();

            var usersToMap = new List<UserViewModel>();

            foreach (var dbUser in users)
            {
                usersToMap.Add(_mapper.Map<UserViewModel>(dbUser));

                var user = usersToMap.SingleOrDefault(m => m.Id == dbUser.Id);

                if (null == user) continue;
                user.Role = dbUser.Role?.Name;
            }

            return await _result.Response(HttpStatusCode.OK, new
            {
                Data = usersToMap,
                Paging = new
                {
                    Total = total,
                    Returned = usersToMap.Count
                }
            });
        }
        
        /**
         * GenerateRandomString
         */
        private string GenerateRandomString(int leng = 8)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[leng];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}
