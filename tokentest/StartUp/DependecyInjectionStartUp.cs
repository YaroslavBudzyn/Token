using Microsoft.Extensions.DependencyInjection;
using tokentest.Services.UserManagement.Classes;
using tokentest.Services.UserManagement.Classes.Interfaces;

namespace tokentest.StartUp
{
    public class DependecyInjectionStartUp
    {
        public static void Inject(IServiceCollection services)
        {
            #region Services

            //User management
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            #endregion

            #region Helpers

            services.AddSingleton<IJwtHandler, JwtHandler>();

            #endregion
        }
    }
}
