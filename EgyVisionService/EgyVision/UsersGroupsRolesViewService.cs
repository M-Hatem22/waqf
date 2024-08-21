using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IUsersGroupsRolesViewService
	{
		List<UsersGroupsRolesViewVM> Search(UsersGroupsRolesViewVM model);
	}

	public class UsersGroupsRolesViewService : IUsersGroupsRolesViewService
	{
		private IEgyVisionRepository<UsersGroupsRolesView> _UsersGroupsRolesViewRepo = null;
		public UsersGroupsRolesViewService()
		{
			_UsersGroupsRolesViewRepo = new EgyVisionRepository<UsersGroupsRolesView>();
		}

		public List<UsersGroupsRolesViewVM> Search(UsersGroupsRolesViewVM model)
		{
			List<UsersGroupsRolesViewVM> returned = new List<UsersGroupsRolesViewVM>();
			var predicate = PredicateBuilder.New<UsersGroupsRolesView>(true);

			//if (!String.IsNullOrEmpty(model.UserId))
			//{
				//predicate = predicate.And(p => p.UserId == model.UserId);
			//}
			//if (!String.IsNullOrEmpty(model.EmployeeCode))
			//{
				//predicate = predicate.And(p => p.EmployeeCode == model.EmployeeCode);
			//}
			//if (!String.IsNullOrEmpty(model.GroupName))
			//{
				//predicate = predicate.And(p => p.GroupName == model.GroupName);
			//}
			//if (!String.IsNullOrEmpty(model.RoleDescription))
			//{
				//predicate = predicate.And(p => p.RoleDescription == model.RoleDescription);
			//}
			//if (!String.IsNullOrEmpty(model.RoleName))
			//{
				//predicate = predicate.And(p => p.RoleName == model.RoleName);
			//}
			//if (model.GroupId > 0)
			//{
				//predicate = predicate.And(p => p.GroupId == model.GroupId);
			//}
			//if (!String.IsNullOrEmpty(model.RoleId))
			//{
				//predicate = predicate.And(p => p.RoleId == model.RoleId);
			//}
			IQueryable<UsersGroupsRolesView> query = _UsersGroupsRolesViewRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "EmployeeCode" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.EmployeeCode).Where(predicate);
			else if (model.OrderBy == "EmployeeCode" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.EmployeeCode).Where(predicate);
			else if (model.OrderBy == "GroupName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GroupName).Where(predicate);
			else if (model.OrderBy == "GroupName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GroupName).Where(predicate);
			else if (model.OrderBy == "RoleDescription" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RoleDescription).Where(predicate);
			else if (model.OrderBy == "RoleDescription" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.RoleDescription).Where(predicate);
			else if (model.OrderBy == "RoleName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RoleName).Where(predicate);
			else if (model.OrderBy == "RoleName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.RoleName).Where(predicate);
			else if (model.OrderBy == "GroupId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GroupId).Where(predicate);
			else if (model.OrderBy == "GroupId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GroupId).Where(predicate);
			else if (model.OrderBy == "RoleId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RoleId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.RoleId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (UsersGroupsRolesView record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					UsersGroupsRolesViewVM vm = new UsersGroupsRolesViewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		private void copyToModel(UsersGroupsRolesViewVM src, UsersGroupsRolesView dest)
		{
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			if (!String.IsNullOrEmpty(src.EmployeeCode))
				dest.EmployeeCode = src.EmployeeCode;
			if (!String.IsNullOrEmpty(src.GroupName))
				dest.GroupName = src.GroupName;
			if (!String.IsNullOrEmpty(src.RoleDescription))
				dest.RoleDescription = src.RoleDescription;
			if (!String.IsNullOrEmpty(src.RoleName))
				dest.RoleName = src.RoleName;
			if (src.GroupId > 0)
				dest.GroupId = src.GroupId;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
		}

		private void copyToVM(UsersGroupsRolesView src, UsersGroupsRolesViewVM dest)
		{
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			if (!String.IsNullOrEmpty(src.EmployeeCode))
				dest.EmployeeCode = src.EmployeeCode;
			if (!String.IsNullOrEmpty(src.GroupName))
				dest.GroupName = src.GroupName;
			if (!String.IsNullOrEmpty(src.RoleDescription))
				dest.RoleDescription = src.RoleDescription;
			if (!String.IsNullOrEmpty(src.RoleName))
				dest.RoleName = src.RoleName;
			if (src.GroupId > 0)
				dest.GroupId = src.GroupId;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
		}

	}
}
