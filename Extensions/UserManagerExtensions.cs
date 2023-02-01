using EcommerceAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Extensions
{
    // extract the Address data of user
    public static class UserManagerExtensions
    {
        public static async Task<User> SearchUserWithAddressAsync(this UserManager<User> input, ClaimsPrincipal usr)
        {
            var email = usr?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public static async Task<User> SearchUserAsync(this UserManager<User> input, ClaimsPrincipal usr)
        {
            var email = usr?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await input.Users.SingleOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
