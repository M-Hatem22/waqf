using System;
using System.ComponentModel.DataAnnotations;

namespace BackEgyVision.Models.Models
{
    [Serializable]
    public class ForgotPasswordVM2
    {
        [Required(ErrorMessage = "يرجى كتابة اسم المستخدم")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "يرجى كتابة كلمة المرور الجديدة")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "يرجى إعادة كتابة كلمة المرور")]
        public string NewPasswordConfirm { get; set; }
    }

    [Serializable]
    public class VerifyCodeVM2
    {
        [Required(ErrorMessage = "يجب كتابة رمز التحقق المرسل الى الجوال")]
        public string VerCode { get; set; }
        public string SentCode { get; set; }
        public int TrysCounter { get; set; }
        public int SmsCounter { get; set; }
        public int UserType { get; set; }   // 1 Individual         2 Charity.

        public string MobileNumber { get; set; }
        public string UserName { get; set; }
    }

    [Serializable]
    public class VerifyUserVM2
    {
        [Display(Name = "BeneficiarId", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public long BeneficiarId { get; set; }

        [Required(ErrorMessageResourceName = "NationalIdError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "NationalId", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string NationalId { get; set; }


        [Required(ErrorMessageResourceName = "LKCountryIdError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Range(minimum: 1, maximum: 999999999, ErrorMessageResourceName = "LKCountryIdError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "LKCountryId", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public Nullable<int> LKCountryId { get; set; }


        [Required(ErrorMessageResourceName = "ServiceCountryIDError2", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Range(minimum: 1, maximum: 999999999, ErrorMessageResourceName = "ServiceCountryIDError2", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "ServiceCountryID2", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public Nullable<int> ServiceCountryID { get; set; }

        [Required(ErrorMessageResourceName = "PhoneNumberError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessageResourceName = "FirstNameError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceName = "FatherNameError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "FatherName", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string FatherName { get; set; }
        [Required(ErrorMessageResourceName = "FamilyNameError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "FamilyName", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string FamilyName { get; set; }
        [Required(ErrorMessageResourceName = "CountryKeyError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "CountryKey", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string CountryKey { get; set; }
    }

    [Serializable]
    public class EditIndiviualVM2
    {
        [Display(Name = "NationalId", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string NationalId { get; set; }
        [Display(Name = "LKCountryId", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public Nullable<int> LKCountryId { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessageResourceName = "FirstNameError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceName = "FatherNameError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "FatherName", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string FatherName { get; set; }
        [Required(ErrorMessageResourceName = "FamilyNameError", ErrorMessageResourceType = typeof(Resources.Models_VerifyUserVM))]
        [Display(Name = "FamilyName", ResourceType = typeof(Resources.Models_VerifyUserVM))]
        public string FamilyName { get; set; }

        public string NationalityName { get; set; }
        public Nullable<long> ProfileAttachmentId { get; set; }
        [Required(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Resources.Models_IndividualDataVM))]
        [EmailAddress(ErrorMessageResourceName = "WrongEmail", ErrorMessageResourceType = typeof(Resources.Models_IndividualDataVM))]
        [Display(Name = "Email", ResourceType = typeof(Resources.Models_IndividualDataVM))]
        public string Email { get; set; }   // البريد الإلكتروني
    }

    [Serializable]
    public class ChangePasswordVM2
    {
        [Required(ErrorMessage = "يرجى كتابة كلمة المرور الحالية")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "يرجى كتابة كلمة المرور الجديدة")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "يرجى إعادة كتابة كلمة المرور")]
        public string NewPasswordConfirm { get; set; }
    }

    [Serializable]
    public class ChangeMobileVM2
    {
        [Required(ErrorMessage = "يرجى كتابة رقم الجوال")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "يرجى اختيار كود الدولة")]
        public string CountryCode { get; set; }
    }


    public class IndividualType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
