using Microsoft.AspNetCore.Identity;

namespace BackEgyVision.Infrastructure
{
    public class ArabiclanguageIdentityErrorDescriber: IdentityErrorDescriber
    {
        public ArabiclanguageIdentityErrorDescriber()
        {
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format("هذا البريد مسجل من قبل", email)
            };
        }


        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "كلمة المرور لابد أن تحتوي علي أحرف"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "كلمة المرور لابد أن تحتوي علي أرقام"
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordMismatch),
                Description = "كلمة المرور غير متطابقة"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = "كلمة المرور لابد ألا تقل عن "+ length + " حروف و أرقام"
            };
        }
    }
}
