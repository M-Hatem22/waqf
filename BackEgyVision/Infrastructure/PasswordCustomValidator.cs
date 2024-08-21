using EgyVisionCore.Entities.EgyVision;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackEgyVision.Infrastructure
{
    public class PasswordCustomValidator<TUser> : IPasswordValidator<TUser> where TUser : AppUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            if (Regex.Matches(password, @"[a-zA-Z]").Count == 0)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UsernameAsPassword",
                    Description = "كلمة المرور لابد أن تحتوي علي حروف"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
