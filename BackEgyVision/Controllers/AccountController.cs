using BackEgyVision.Models;
using DinkToPdf.Contracts;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using EgyVisionService.HelperServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;

namespace BackEgyVision.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Serializable]
    public class AccountController : BaseController
    {
        private readonly IStringLocalizer<AccountController> _localizer;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private readonly IStringLocalizer<AspNetUsersService> _servicelocalizer;
        public AccountController(IStringLocalizer<AccountController> localizer, UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr, IConverter converter, IStringLocalizer<AspNetUsersService> servicelocalizer) : base(converter)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            _localizer = localizer;
            _servicelocalizer = servicelocalizer;
        }
        [AllowAnonymous]
        public IActionResult Login(LoginModel LoginModel)
        {
            SetSessions();
           
            if (LoginModel != null && LoginModel.IsPostBack == true)
            {
                return View(LoginModel);
            }

            LoginModel model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromServices] IHostingEnvironment hostingEnvironment, LoginModel details, string returnUrl)
        {
            AppUser _user;
            signInManager.SignOutAsync();
            if (!string.IsNullOrEmpty(details.Email) && !string.IsNullOrEmpty(details.Password))
            {
                _user = userManager.FindByNameAsync(details.Email).Result;
                if (_user == null)
                    _user = userManager.FindByEmailAsync(details.Email).Result;

                if (_user != null)
                {
                    if (_user.Id == _user.Email)
                    {
                        ModelState.AddModelError("", _localizer["DeletedAccount"]);
                        return View(details);
                    }

                    byte[] UserImage = DownAtt(_user.Id, _user.AccountType.Value);
                    if (UserImage != null && UserImage.Length > 0)
                        HttpContext.Session.Set("UserProfileImage", UserImage);

                    Microsoft.AspNetCore.Identity.SignInResult result = signInManager.PasswordSignInAsync(_user, details.Password, false, true).Result;

                    if (!userManager.IsLockedOutAsync(_user).Result)
                    {
                        
                        
                        if (result.Succeeded)
                        {
                            await signInManager.ClaimsFactory.CreateAsync(_user);
                            string UnReadNotifications = "";

                            if (UnReadNotifications.EndsWith('-'))
                                UnReadNotifications = UnReadNotifications.Remove(UnReadNotifications.Length - 1, 1);

                            HttpContext.Session.SetString("UnReadNotifications", UnReadNotifications);
                            HttpContext.Session.SetInt32("UnReadNotificationsCount", 0);

                            // 6 is cashier  7 is rest manager 8 is branch manager. 
                            if ((_user.IsActive == null || _user.IsActive.Value == false) ||_user.AccountType != 1)
                            {
                                ModelState.AddModelError(nameof(LoginModel.Email), _localizer["NotAuthorized"].Value);
                                return View(details);
                            }

                            RefreshUserRoles(_user);
                            HttpContext.Session.SetString("TestSession", "alive");
                            HttpContext.Session.SetString("UserEmail", _user.Email);
                            HttpContext.Session.SetString("UserId", _user.Id);
                            HttpContext.Session.SetString("UserName", _user.UserName);
                            HttpContext.Session.SetString("UserFullName", _user.FullName);
                            if (!string.IsNullOrEmpty(_user.NationalId))
                                HttpContext.Session.SetString("UserNationalId", _user.NationalId);
                            //HttpContext.Session.SetString("Mobile", _user.PhoneNumber);
                            //HttpContext.Session.SetInt32("LKCountryId", _user.LKCountryId.Value);
                            //HttpContext.Session.SetString("AccountType", _user.AccountType.ToString());
                            //HttpContext.Session.SetString("IsMobileActive", _user.IsMobileActivated.ToString().ToLower());
                            HttpContext.Session.SetString("Role", _user.EmployeeCode.ToString());

                            // Get A token and load it to the session.
                            // IHostingEnvironment hostingEnvironment;

                            //HttpContext.Session.SetString("Token", APIService.getToken((Microsoft.Extensions.Hosting.IHostingEnvironment)hostingEnvironment));

                            // notification
                            string AccountTypeString = HttpContext.Session.GetString("AccountType");

                            // Cashier / or waiter / or kitchen / Or delivery
                            if (AccountTypeString == "4" || AccountTypeString == "5" || AccountTypeString == "6" || AccountTypeString == "2" || AccountTypeString == "9")
                            {
                                INotificationsService NotificationsServ = new NotificationsService();
                                string parameters = "";

                                // get the  AccountTypeName according to the culture
                                ILKAccountTypesService LKAccountTypesService = new LKAccountTypesService();

                                string AccountTypeName = "";
                                if (_cookieSelectedLanguage == "ar-SA")
                                    AccountTypeName = LKAccountTypesService.GetById(int.Parse(HttpContext.Session.GetString("AccountType"))).LKAccountTypeNameAr;
                                else if (_cookieSelectedLanguage == "en-US")
                                    AccountTypeName = LKAccountTypesService.GetById(int.Parse(HttpContext.Session.GetString("AccountType"))).LKAccountTypeNameEn;
                                else
                                    AccountTypeName = LKAccountTypesService.GetById(int.Parse(HttpContext.Session.GetString("AccountType"))).LKAccountTypeNameEn;
                            }
                            if (!String.IsNullOrEmpty(returnUrl))
                                return Redirect(returnUrl ?? "/");
                            else
                                return RedirectToAction("Index", "Home", null);
                        }
                        else
                        {
                            userManager.AccessFailedAsync(_user);
                            ModelState.AddModelError("", "Login Failed");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", _localizer["LoginAttemptsConsumed"]);
                    }
                }
            }

            return View(details);
        }
        [System.Web.Mvc.HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var user = new AppUser() { Email = userModel.Email, EmployeeCode = "0" ,
                FullName = userModel.FirstName +" "+ userModel.LastName,
                AccountType=1,LKCountryId=1,IsActive=true,
                UserName=userModel.Username};
            var result = await userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }
            //await userManager.AddToRoleAsync(user, "Visitor");
            return RedirectToAction("Index","Home");
        }
        private byte[] DownAtt(string userId, byte AccountType)
        {
            IAttachmentsService AttachmentsServ = new AttachmentsService();

            AttachmentsVM model = new AttachmentsVM();
            model.KeyIdStr = userId;
            if (AccountType == 1)
            {
                model.LKKeyTypeId = 1;
                model.LKAttachmentTypeId = 1;
            }
            else
            {
                model.LKKeyTypeId = 2;
                model.LKAttachmentTypeId = 2;
            }

            model = AttachmentsServ.Search(model).FirstOrDefault();

            if (model != null && model.AttachmentFile != null && model.AttachmentFile.Length > 0)
            {
                return model.AttachmentFile;
            }

            return null;
        }
        private void SetSessions()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            var Configuration = builder.Build();
            string IsAtmaAdmin = Configuration.GetSection("ApplicationSettings:IsAtmaAdmin").Value.ToString();
            string ParentId = Configuration.GetSection("ApplicationSettings:ParentId").Value.ToString();
            HttpContext.Session.SetString("IsAtmaAdmin", IsAtmaAdmin);
            HttpContext.Session.SetString("ParentId", ParentId);
            HttpContext.Session.SetString("TestSession", "TestSession");
            if (!string.IsNullOrEmpty(ParentId))
            {
                string imgDataURL = "";
                IAttachmentsService attachmentsService = new AttachmentsService();
                AttachmentsVM attachmentsVM = new AttachmentsVM();
                attachmentsVM.LKKeyTypeId = 2;
                attachmentsVM.LKAttachmentTypeId = 2;
                attachmentsVM.KeyId = long.Parse(ParentId);
                List<AttachmentsVM> attachmentRestLogo = attachmentsService.Search(attachmentsVM);
                if (attachmentRestLogo.Count > 0)
                {
                    byte[] Temp = attachmentRestLogo[0].AttachmentFile;
                    string imreBase64Data = Convert.ToBase64String(Temp);
                    imgDataURL = "data:image/png;base64," + imreBase64Data;
                    HttpContext.Session.SetString("ParentImageLogo", imgDataURL);
                }
            }
        }
        private void RefreshUserRoles(AppUser user)
        {
            IAspNetGroupsUsersService _aspNetGroupsUsersService = new AspNetGroupsUsersService();
            IGroupsRolesViewService _UsersGroupsRolesViewService = new GroupsRolesViewService();
            IUsersRolesViewService _usersRolesViewService = new UsersRolesViewService();
            IAspNetRolesService _aspNetRolesService = new AspNetRolesService();
            IAspNetUserRolesService _aspNetUserRolesService = new AspNetUserRolesService();

            List<AppRole> actualRoles = new List<AppRole>();

            List<AspNetGroupsUsersVM> groups = _aspNetGroupsUsersService.Search(new AspNetGroupsUsersVM()
            {
                UserId = user.Id
            });

            foreach (AspNetGroupsUsersVM group in groups)
            {
                List<GroupsRolesViewVM> userRoles = _UsersGroupsRolesViewService.Search(new GroupsRolesViewVM()
                {
                    GroupId = group.GroupId
                });

                foreach (GroupsRolesViewVM uR in userRoles)
                {
                    if (actualRoles.Where(x => x.Id == uR.RoleId).FirstOrDefault() == null)
                    {
                        AppRole newRole = new AppRole();
                        newRole.Id = uR.RoleId;
                        newRole.Name = uR.RoleName;
                        newRole.Description = uR.RoleDescription;

                        actualRoles.Add(newRole);
                    }
                }
            }

            List<UsersRolesViewVM> uRolesViewList = _usersRolesViewService.Search(new UsersRolesViewVM()
            {
                UserId = user.Id
            });

            IList<string> uRoles = new List<string>();
            foreach (UsersRolesViewVM r in uRolesViewList)
            {
                if (uRoles.Where(x => x == r.RoleName).FirstOrDefault() == null && !String.IsNullOrEmpty(r.RoleName))
                    uRoles.Add(r.RoleName);
            }

            foreach (string r in uRoles)
            {
                if (actualRoles.Where(x => x.Name == r).FirstOrDefault() == null)
                {
                    AspNetRolesVM ar = _aspNetRolesService.Search(new AspNetRolesVM()
                    {
                        Name = r
                    }).FirstOrDefault();
                    AspNetUserRolesVM urole = new AspNetUserRolesVM();
                    urole.RoleId = ar.Id;
                    urole.UserId = user.Id;
                    _aspNetUserRolesService.Delete(urole);
                }
            }

            foreach (AppRole r in actualRoles)
            {
                if (uRoles.Where(x => x == r.Name).FirstOrDefault() == null)
                {
                    AspNetUserRolesVM urole = new AspNetUserRolesVM();
                    urole.RoleId = r.Id;
                    urole.UserId = user.Id;
                    _aspNetUserRolesService.Insert(urole);
                }
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            string AccountTypeString = HttpContext.Session.GetString("AccountType");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
