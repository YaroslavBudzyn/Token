using System.Linq;
using tokentest.DataAccess.Context;
using tokentest.DataAccess.UserManagement.Entities;

namespace tokentest.DataAccess.DBInitializer
{
    public static class TokentestDbInitializer
    {
        public static void Initialize(TokentestDbContext context)
        {
            //if db is not exist ,it will create database .but ,do nothing.
            //context.Database.EnsureCreated();

            #region Roles

            if (!context.Roles.Any())
            {
                var roles = new[]
                {
                    new Role {Name = "admin", Description = "Role for admin"},
                    new Role {Name = "user", Description = "Role for user"}
                };

                foreach (var r in roles)
                {
                    context.Roles.Add(r);
                }

                context.SaveChanges();
            }

            #endregion

            #region Users

            // Look for any users.
            if (!context.Users.Any())
            {
                var users = new[]
                {
                    new User
                    {
                        RoleId = 1,
                        Email = "admin@admin.com",
                        Name = "Admin admiN",
                        Password = "AQAAAAEAACcQAAAAEBPAFBJbQeL4G7d4hdRoEKQCtDH925+BxR41g6co93GjI1NrwzloBe4Ztw8bW8uTaw==",
                        Phone = "+10961111144"
                    },
                    new User
                    {
                        RoleId = 2,
                        Email = "user@user.com",
                        Name = "User useR",
                        Password = "AQAAAAEAACcQAAAAEBPAFBJbQeL4G7d4hdRoEKQCtDH925+BxR41g6co93GjI1NrwzloBe4Ztw8bW8uTaw==",
                        Phone = "+10761111144"
                    }
                };

                foreach (var u in users)
                {
                    context.Users.Add(u);
                }

                context.SaveChanges();
            }

            #endregion
        }
    }
}
