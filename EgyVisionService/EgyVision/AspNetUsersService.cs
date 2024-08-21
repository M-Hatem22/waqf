using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IAspNetUsersService
	{
		List<AspNetUsersVM> Search(AspNetUsersVM model);
		bool Insert(AspNetUsersVM vm);
		bool Update(AspNetUsersVM vm);
		bool Delete(AspNetUsersVM vm);
		AspNetUsersVM GetById(string Id);
	}

	public class AspNetUsersService : IAspNetUsersService
	{
		private IEgyVisionRepository<AspNetUsers> _AspNetUsersRepo = null;
		public AspNetUsersService()
		{
			_AspNetUsersRepo = new EgyVisionRepository<AspNetUsers>();
		}

		public bool Insert(AspNetUsersVM vm)
		{
			AspNetUsers model = new AspNetUsers();
			copyToModel(vm,model);
			bool success = _AspNetUsersRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AspNetUsersVM vm)
		{
			AspNetUsers model = _AspNetUsersRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AspNetUsersRepo.Update(model);
		}

		public bool Delete(AspNetUsersVM vm)
		{
			AspNetUsers model = _AspNetUsersRepo.GetById(vm.Id);
			return _AspNetUsersRepo.Delete(model);
		}

		public List<AspNetUsersVM> Search(AspNetUsersVM model)
		{
			List<AspNetUsersVM> returned = new List<AspNetUsersVM>();
			var predicate = PredicateBuilder.New<AspNetUsers>(true);

			//if (!String.IsNullOrEmpty(model.Id))
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (!String.IsNullOrEmpty(model.Email))
			//{
				//predicate = predicate.And(p => p.Email == model.Email);
			//}
				//predicate = predicate.And(p => p.EmailConfirmed == model.EmailConfirmed);
			//if (!String.IsNullOrEmpty(model.PasswordHash))
			//{
				//predicate = predicate.And(p => p.PasswordHash == model.PasswordHash);
			//}
			//if (!String.IsNullOrEmpty(model.SecurityStamp))
			//{
				//predicate = predicate.And(p => p.SecurityStamp == model.SecurityStamp);
			//}
			//if (!String.IsNullOrEmpty(model.ConcurrencyStamp))
			//{
				//predicate = predicate.And(p => p.ConcurrencyStamp == model.ConcurrencyStamp);
			//}
				//predicate = predicate.And(p => p.LockoutEnd == model.LockoutEnd);
			//if (!String.IsNullOrEmpty(model.NormalizedEmail))
			//{
				//predicate = predicate.And(p => p.NormalizedEmail == model.NormalizedEmail);
			//}
			//if (!String.IsNullOrEmpty(model.NormalizedUserName))
			//{
				//predicate = predicate.And(p => p.NormalizedUserName == model.NormalizedUserName);
			//}
			//if (!String.IsNullOrEmpty(model.PhoneNumber))
			//{
				//predicate = predicate.And(p => p.PhoneNumber == model.PhoneNumber);
			//}
				//predicate = predicate.And(p => p.PhoneNumberConfirmed == model.PhoneNumberConfirmed);
				//predicate = predicate.And(p => p.TwoFactorEnabled == model.TwoFactorEnabled);
				//predicate = predicate.And(p => p.LockoutEndDateUtc == model.LockoutEndDateUtc);
				//predicate = predicate.And(p => p.LockoutEnabled == model.LockoutEnabled);
			//if (model.AccessFailedCount > 0)
			//{
				//predicate = predicate.And(p => p.AccessFailedCount == model.AccessFailedCount);
			//}
			//if (!String.IsNullOrEmpty(model.UserName))
			//{
				//predicate = predicate.And(p => p.UserName == model.UserName);
			//}
			//if (!String.IsNullOrEmpty(model.NationalId))
			//{
				//predicate = predicate.And(p => p.NationalId == model.NationalId);
			//}
				//predicate = predicate.And(p => p.LastLogin == model.LastLogin);
				//predicate = predicate.And(p => p.RegisterDate == model.RegisterDate);
				//predicate = predicate.And(p => p.IsActive == model.IsActive);
				//predicate = predicate.And(p => p.IsMobileActivated == model.IsMobileActivated);
			//if (!String.IsNullOrEmpty(model.EmployeeCode))
			//{
				//predicate = predicate.And(p => p.EmployeeCode == model.EmployeeCode);
			//}
			//if (!String.IsNullOrEmpty(model.FullName))
			//{
				//predicate = predicate.And(p => p.FullName == model.FullName);
			//}
			//if (!String.IsNullOrEmpty(model.VerCode))
			//{
				//predicate = predicate.And(p => p.VerCode == model.VerCode);
			//}
				//predicate = predicate.And(p => p.VerCodeExpireDate == model.VerCodeExpireDate);
			//if (model.AccountType > 0)
			//{
				//predicate = predicate.And(p => p.AccountType == model.AccountType);
			//}
			//if (model.LKCountryId > 0)
			//{
				//predicate = predicate.And(p => p.LKCountryId == model.LKCountryId);
			//}
			//if (model.ServiceCountryId > 0)
			//{
				//predicate = predicate.And(p => p.ServiceCountryId == model.ServiceCountryId);
			//}
			//if (model.Points > 0)
			//{
				//predicate = predicate.And(p => p.Points == model.Points);
			//}
			//if (model.PartnerId > 0)
			//{
				//predicate = predicate.And(p => p.PartnerId == model.PartnerId);
			//}
			//if (model.CharityId > 0)
			//{
				//predicate = predicate.And(p => p.CharityId == model.CharityId);
			//}

			IQueryable<AspNetUsers> query = _AspNetUsersRepo.Table.AsExpandable().Where(predicate);

			string[] orderStr = null;
			if (!String.IsNullOrEmpty(model.jtSorting))
			{
				orderStr = model.jtSorting.Split(' ');
				model.OrderBy = orderStr[0];
				if (orderStr[1].ToLower() == "asc")
					model.OrderByReversed = false;
				else
					model.OrderByReversed = true;
			}
			else
			{
					model.OrderBy = "Id";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "Id" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Id).Where(predicate);
			else if (model.OrderBy == "Id" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Id).Where(predicate);
			else if (model.OrderBy == "Email" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Email).Where(predicate);
			else if (model.OrderBy == "Email" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Email).Where(predicate);
			else if (model.OrderBy == "EmailConfirmed" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.EmailConfirmed).Where(predicate);
			else if (model.OrderBy == "EmailConfirmed" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.EmailConfirmed).Where(predicate);
			else if (model.OrderBy == "PasswordHash" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.PasswordHash).Where(predicate);
			else if (model.OrderBy == "PasswordHash" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.PasswordHash).Where(predicate);
			else if (model.OrderBy == "SecurityStamp" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.SecurityStamp).Where(predicate);
			else if (model.OrderBy == "SecurityStamp" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.SecurityStamp).Where(predicate);
			else if (model.OrderBy == "ConcurrencyStamp" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ConcurrencyStamp).Where(predicate);
			else if (model.OrderBy == "ConcurrencyStamp" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ConcurrencyStamp).Where(predicate);
			else if (model.OrderBy == "LockoutEnd" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LockoutEnd).Where(predicate);
			else if (model.OrderBy == "LockoutEnd" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LockoutEnd).Where(predicate);
			else if (model.OrderBy == "NormalizedEmail" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NormalizedEmail).Where(predicate);
			else if (model.OrderBy == "NormalizedEmail" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NormalizedEmail).Where(predicate);
			else if (model.OrderBy == "NormalizedUserName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NormalizedUserName).Where(predicate);
			else if (model.OrderBy == "NormalizedUserName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NormalizedUserName).Where(predicate);
			else if (model.OrderBy == "PhoneNumber" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.PhoneNumber).Where(predicate);
			else if (model.OrderBy == "PhoneNumber" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.PhoneNumber).Where(predicate);
			else if (model.OrderBy == "PhoneNumberConfirmed" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.PhoneNumberConfirmed).Where(predicate);
			else if (model.OrderBy == "PhoneNumberConfirmed" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.PhoneNumberConfirmed).Where(predicate);
			else if (model.OrderBy == "TwoFactorEnabled" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TwoFactorEnabled).Where(predicate);
			else if (model.OrderBy == "TwoFactorEnabled" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TwoFactorEnabled).Where(predicate);
			else if (model.OrderBy == "LockoutEndDateUtc" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LockoutEndDateUtc).Where(predicate);
			else if (model.OrderBy == "LockoutEndDateUtc" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LockoutEndDateUtc).Where(predicate);
			else if (model.OrderBy == "LockoutEnabled" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LockoutEnabled).Where(predicate);
			else if (model.OrderBy == "LockoutEnabled" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LockoutEnabled).Where(predicate);
			else if (model.OrderBy == "AccessFailedCount" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AccessFailedCount).Where(predicate);
			else if (model.OrderBy == "AccessFailedCount" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AccessFailedCount).Where(predicate);
			else if (model.OrderBy == "UserName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserName).Where(predicate);
			else if (model.OrderBy == "UserName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserName).Where(predicate);
			else if (model.OrderBy == "NationalId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NationalId).Where(predicate);
			else if (model.OrderBy == "NationalId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NationalId).Where(predicate);
			else if (model.OrderBy == "LastLogin" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LastLogin).Where(predicate);
			else if (model.OrderBy == "LastLogin" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LastLogin).Where(predicate);
			else if (model.OrderBy == "RegisterDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RegisterDate).Where(predicate);
			else if (model.OrderBy == "RegisterDate" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.RegisterDate).Where(predicate);
			else if (model.OrderBy == "IsActive" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.IsActive).Where(predicate);
			else if (model.OrderBy == "IsActive" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.IsActive).Where(predicate);
			else if (model.OrderBy == "IsMobileActivated" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.IsMobileActivated).Where(predicate);
			else if (model.OrderBy == "IsMobileActivated" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.IsMobileActivated).Where(predicate);
			else if (model.OrderBy == "EmployeeCode" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.EmployeeCode).Where(predicate);
			else if (model.OrderBy == "EmployeeCode" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.EmployeeCode).Where(predicate);
			else if (model.OrderBy == "FullName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.FullName).Where(predicate);
			else if (model.OrderBy == "FullName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.FullName).Where(predicate);
			else if (model.OrderBy == "VerCode" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.VerCode).Where(predicate);
			else if (model.OrderBy == "VerCode" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.VerCode).Where(predicate);
			else if (model.OrderBy == "VerCodeExpireDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.VerCodeExpireDate).Where(predicate);
			else if (model.OrderBy == "VerCodeExpireDate" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.VerCodeExpireDate).Where(predicate);
			else if (model.OrderBy == "AccountType" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AccountType).Where(predicate);
			else if (model.OrderBy == "AccountType" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AccountType).Where(predicate);
			else if (model.OrderBy == "LKCountryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCountryId).Where(predicate);
			else if (model.OrderBy == "LKCountryId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCountryId).Where(predicate);
			else if (model.OrderBy == "ServiceCountryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ServiceCountryId).Where(predicate);
			else if (model.OrderBy == "ServiceCountryId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ServiceCountryId).Where(predicate);
			else if (model.OrderBy == "Points" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Points).Where(predicate);
			else if (model.OrderBy == "Points" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Points).Where(predicate);
			else if (model.OrderBy == "PartnerId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.PartnerId).Where(predicate);
			else if (model.OrderBy == "PartnerId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.PartnerId).Where(predicate);
			else if (model.OrderBy == "CharityId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.CharityId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.CharityId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (AspNetUsers record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AspNetUsersVM vm = new AspNetUsersVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AspNetUsersVM GetById(string Id)
		{
			AspNetUsers model = _AspNetUsersRepo.GetById(Id);
			AspNetUsersVM vm = new AspNetUsersVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AspNetUsersVM src, AspNetUsers dest)
		{
			if (!String.IsNullOrEmpty(src.Id))
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.Email))
				dest.Email = src.Email;
			dest.EmailConfirmed = src.EmailConfirmed;
			if (!String.IsNullOrEmpty(src.PasswordHash))
				dest.PasswordHash = src.PasswordHash;
			if (!String.IsNullOrEmpty(src.SecurityStamp))
				dest.SecurityStamp = src.SecurityStamp;
			if (!String.IsNullOrEmpty(src.ConcurrencyStamp))
				dest.ConcurrencyStamp = src.ConcurrencyStamp;
			dest.LockoutEnd = src.LockoutEnd;
			if (!String.IsNullOrEmpty(src.NormalizedEmail))
				dest.NormalizedEmail = src.NormalizedEmail;
			if (!String.IsNullOrEmpty(src.NormalizedUserName))
				dest.NormalizedUserName = src.NormalizedUserName;
			if (!String.IsNullOrEmpty(src.PhoneNumber))
				dest.PhoneNumber = src.PhoneNumber;
			dest.PhoneNumberConfirmed = src.PhoneNumberConfirmed;
			dest.TwoFactorEnabled = src.TwoFactorEnabled;
			if (src.LockoutEndDateUtc != null && src.LockoutEndDateUtc != DateTime.MinValue)
				dest.LockoutEndDateUtc = src.LockoutEndDateUtc;
			dest.LockoutEnabled = src.LockoutEnabled;
			if (src.AccessFailedCount > 0)
				dest.AccessFailedCount = src.AccessFailedCount;
			if (!String.IsNullOrEmpty(src.UserName))
				dest.UserName = src.UserName;
			if (!String.IsNullOrEmpty(src.NationalId))
				dest.NationalId = src.NationalId;
			if (src.LastLogin != null && src.LastLogin != DateTime.MinValue)
				dest.LastLogin = src.LastLogin;
			if (src.RegisterDate != null && src.RegisterDate != DateTime.MinValue)
				dest.RegisterDate = src.RegisterDate;
			dest.IsActive = src.IsActive;
			dest.IsMobileActivated = src.IsMobileActivated;
			if (!String.IsNullOrEmpty(src.EmployeeCode))
				dest.EmployeeCode = src.EmployeeCode;
			if (!String.IsNullOrEmpty(src.FullName))
				dest.FullName = src.FullName;
			if (!String.IsNullOrEmpty(src.VerCode))
				dest.VerCode = src.VerCode;
			if (src.VerCodeExpireDate != null && src.VerCodeExpireDate != DateTime.MinValue)
				dest.VerCodeExpireDate = src.VerCodeExpireDate;
			dest.AccountType = src.AccountType;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
			if (src.ServiceCountryId > 0)
				dest.ServiceCountryId = src.ServiceCountryId;
			if (src.Points > 0)
				dest.Points = src.Points;
			dest.PartnerId = src.PartnerId;
			dest.CharityId = src.CharityId;
		}

		private void copyToVM(AspNetUsers src, AspNetUsersVM dest)
		{
			if (!String.IsNullOrEmpty(src.Id))
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.Email))
				dest.Email = src.Email;
			dest.EmailConfirmed = src.EmailConfirmed;
			if (!String.IsNullOrEmpty(src.PasswordHash))
				dest.PasswordHash = src.PasswordHash;
			if (!String.IsNullOrEmpty(src.SecurityStamp))
				dest.SecurityStamp = src.SecurityStamp;
			if (!String.IsNullOrEmpty(src.ConcurrencyStamp))
				dest.ConcurrencyStamp = src.ConcurrencyStamp;
			dest.LockoutEnd = src.LockoutEnd;
			if (!String.IsNullOrEmpty(src.NormalizedEmail))
				dest.NormalizedEmail = src.NormalizedEmail;
			if (!String.IsNullOrEmpty(src.NormalizedUserName))
				dest.NormalizedUserName = src.NormalizedUserName;
			if (!String.IsNullOrEmpty(src.PhoneNumber))
				dest.PhoneNumber = src.PhoneNumber;
			dest.PhoneNumberConfirmed = src.PhoneNumberConfirmed;
			dest.TwoFactorEnabled = src.TwoFactorEnabled;
			if (src.LockoutEndDateUtc != null && src.LockoutEndDateUtc != DateTime.MinValue)
				dest.LockoutEndDateUtc = src.LockoutEndDateUtc;
			dest.LockoutEnabled = src.LockoutEnabled;
			if (src.AccessFailedCount > 0)
				dest.AccessFailedCount = src.AccessFailedCount;
			if (!String.IsNullOrEmpty(src.UserName))
				dest.UserName = src.UserName;
			if (!String.IsNullOrEmpty(src.NationalId))
				dest.NationalId = src.NationalId;
			if (src.LastLogin != null && src.LastLogin != DateTime.MinValue)
				dest.LastLogin = src.LastLogin;
			if (src.RegisterDate != null && src.RegisterDate != DateTime.MinValue)
				dest.RegisterDate = src.RegisterDate;
			dest.IsActive = src.IsActive;
			dest.IsMobileActivated = src.IsMobileActivated;
			if (!String.IsNullOrEmpty(src.EmployeeCode))
				dest.EmployeeCode = src.EmployeeCode;
			if (!String.IsNullOrEmpty(src.FullName))
				dest.FullName = src.FullName;
			if (!String.IsNullOrEmpty(src.VerCode))
				dest.VerCode = src.VerCode;
			if (src.VerCodeExpireDate != null && src.VerCodeExpireDate != DateTime.MinValue)
				dest.VerCodeExpireDate = src.VerCodeExpireDate;
			dest.AccountType = src.AccountType;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
			if (src.ServiceCountryId > 0)
				dest.ServiceCountryId = src.ServiceCountryId;
			if (src.Points > 0)
				dest.Points = src.Points;
			dest.PartnerId = src.PartnerId;
			dest.CharityId = src.CharityId;
		}

	}
}
