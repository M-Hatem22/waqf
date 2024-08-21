using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IUsersRolesViewService
	{
		List<UsersRolesViewVM> Search(UsersRolesViewVM model);
	}

	public class UsersRolesViewService : IUsersRolesViewService
	{
		private IEgyVisionRepository<UsersRolesView> _UsersRolesViewRepo = null;
		public UsersRolesViewService()
		{
			_UsersRolesViewRepo = new EgyVisionRepository<UsersRolesView>();
		}

		public List<UsersRolesViewVM> Search(UsersRolesViewVM model)
		{
			List<UsersRolesViewVM> returned = new List<UsersRolesViewVM>();
			var predicate = PredicateBuilder.New<UsersRolesView>(true);

			//if (!String.IsNullOrEmpty(model.UserId))
			//{
				//predicate = predicate.And(p => p.UserId == model.UserId);
			//}
			//if (!String.IsNullOrEmpty(model.RoleId))
			//{
				//predicate = predicate.And(p => p.RoleId == model.RoleId);
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
			//if (!String.IsNullOrEmpty(model.NationalID))
			//{
				//predicate = predicate.And(p => p.NationalID == model.NationalID);
			//}
				//predicate = predicate.And(p => p.LastLogin == model.LastLogin);
				//predicate = predicate.And(p => p.RegisterDate == model.RegisterDate);
				//predicate = predicate.And(p => p.IsActive == model.IsActive);
			//if (!String.IsNullOrEmpty(model.EmployeeCode))
			//{
				//predicate = predicate.And(p => p.EmployeeCode == model.EmployeeCode);
			//}
			//if (!String.IsNullOrEmpty(model.FullName))
			//{
				//predicate = predicate.And(p => p.FullName == model.FullName);
			//}
			//if (!String.IsNullOrEmpty(model.RoleName))
			//{
				//predicate = predicate.And(p => p.RoleName == model.RoleName);
			//}
			//if (!String.IsNullOrEmpty(model.NormalizedName))
			//{
				//predicate = predicate.And(p => p.NormalizedName == model.NormalizedName);
			//}
			//if (!String.IsNullOrEmpty(model.Description))
			//{
				//predicate = predicate.And(p => p.Description == model.Description);
			//}
			//if (model.RequestTypeId > 0)
			//{
				//predicate = predicate.And(p => p.RequestTypeId == model.RequestTypeId);
			//}
			IQueryable<UsersRolesView> query = _UsersRolesViewRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "UserId";
					model.OrderByReversed = false;
			}

			if (model.OrderBy == "UserId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "UserId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "RoleId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RoleId).Where(predicate);
			else if (model.OrderBy == "RoleId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.RoleId).Where(predicate);
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
			else if (model.OrderBy == "NationalID" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NationalID).Where(predicate);
			else if (model.OrderBy == "NationalID" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NationalID).Where(predicate);
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
			else if (model.OrderBy == "EmployeeCode" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.EmployeeCode).Where(predicate);
			else if (model.OrderBy == "EmployeeCode" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.EmployeeCode).Where(predicate);
			else if (model.OrderBy == "FullName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.FullName).Where(predicate);
			else if (model.OrderBy == "FullName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.FullName).Where(predicate);
			else if (model.OrderBy == "RoleName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RoleName).Where(predicate);
			else if (model.OrderBy == "RoleName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.RoleName).Where(predicate);
			else if (model.OrderBy == "NormalizedName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NormalizedName).Where(predicate);
			else if (model.OrderBy == "NormalizedName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NormalizedName).Where(predicate);
			else if (model.OrderBy == "Description" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Description).Where(predicate);
			else if (model.OrderBy == "Description" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Description).Where(predicate);
			else if (model.OrderBy == "RequestTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RequestTypeId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.RequestTypeId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (UsersRolesView record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					UsersRolesViewVM vm = new UsersRolesViewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		private void copyToModel(UsersRolesViewVM src, UsersRolesView dest)
		{
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
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
			if (!String.IsNullOrEmpty(src.NationalID))
				dest.NationalID = src.NationalID;
			if (src.LastLogin != null && src.LastLogin != DateTime.MinValue)
				dest.LastLogin = src.LastLogin;
			if (src.RegisterDate != null && src.RegisterDate != DateTime.MinValue)
				dest.RegisterDate = src.RegisterDate;
			dest.IsActive = src.IsActive;
			if (!String.IsNullOrEmpty(src.EmployeeCode))
				dest.EmployeeCode = src.EmployeeCode;
			if (!String.IsNullOrEmpty(src.FullName))
				dest.FullName = src.FullName;
			if (!String.IsNullOrEmpty(src.RoleName))
				dest.RoleName = src.RoleName;
			if (!String.IsNullOrEmpty(src.NormalizedName))
				dest.NormalizedName = src.NormalizedName;
			if (!String.IsNullOrEmpty(src.Description))
				dest.Description = src.Description;
			if (src.RequestTypeId > 0)
				dest.RequestTypeId = src.RequestTypeId;
		}

		private void copyToVM(UsersRolesView src, UsersRolesViewVM dest)
		{
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
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
			if (!String.IsNullOrEmpty(src.NationalID))
				dest.NationalID = src.NationalID;
			if (src.LastLogin != null && src.LastLogin != DateTime.MinValue)
				dest.LastLogin = src.LastLogin;
			if (src.RegisterDate != null && src.RegisterDate != DateTime.MinValue)
				dest.RegisterDate = src.RegisterDate;
			dest.IsActive = src.IsActive;
			if (!String.IsNullOrEmpty(src.EmployeeCode))
				dest.EmployeeCode = src.EmployeeCode;
			if (!String.IsNullOrEmpty(src.FullName))
				dest.FullName = src.FullName;
			if (!String.IsNullOrEmpty(src.RoleName))
				dest.RoleName = src.RoleName;
			if (!String.IsNullOrEmpty(src.NormalizedName))
				dest.NormalizedName = src.NormalizedName;
			if (!String.IsNullOrEmpty(src.Description))
				dest.Description = src.Description;
			if (src.RequestTypeId > 0)
				dest.RequestTypeId = src.RequestTypeId;
		}

	}
}
