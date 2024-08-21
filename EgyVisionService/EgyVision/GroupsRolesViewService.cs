using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IGroupsRolesViewService
	{
		List<GroupsRolesViewVM> Search(GroupsRolesViewVM model);
	}

	public class GroupsRolesViewService : IGroupsRolesViewService
	{
		private IEgyVisionRepository<GroupsRolesView> _GroupsRolesViewRepo = null;
		public GroupsRolesViewService()
		{
			_GroupsRolesViewRepo = new EgyVisionRepository<GroupsRolesView>();
		}

		public List<GroupsRolesViewVM> Search(GroupsRolesViewVM model)
		{
			List<GroupsRolesViewVM> returned = new List<GroupsRolesViewVM>();
			var predicate = PredicateBuilder.New<GroupsRolesView>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (model.GroupId > 0)
			//{
				//predicate = predicate.And(p => p.GroupId == model.GroupId);
			//}
			//if (!String.IsNullOrEmpty(model.RoleId))
			//{
				//predicate = predicate.And(p => p.RoleId == model.RoleId);
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
			//if (model.DisplayOrder > 0)
			//{
				//predicate = predicate.And(p => p.DisplayOrder == model.DisplayOrder);
			//}
			IQueryable<GroupsRolesView> query = _GroupsRolesViewRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "GroupId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GroupId).Where(predicate);
			else if (model.OrderBy == "GroupId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GroupId).Where(predicate);
			else if (model.OrderBy == "RoleId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RoleId).Where(predicate);
			else if (model.OrderBy == "RoleId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.RoleId).Where(predicate);
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
			else if (model.OrderBy == "DisplayOrder" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.DisplayOrder).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.DisplayOrder).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (GroupsRolesView record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					GroupsRolesViewVM vm = new GroupsRolesViewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		private void copyToModel(GroupsRolesViewVM src, GroupsRolesView dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (src.GroupId > 0)
				dest.GroupId = src.GroupId;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
			if (!String.IsNullOrEmpty(src.GroupName))
				dest.GroupName = src.GroupName;
			if (!String.IsNullOrEmpty(src.RoleDescription))
				dest.RoleDescription = src.RoleDescription;
			if (!String.IsNullOrEmpty(src.RoleName))
				dest.RoleName = src.RoleName;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
		}

		private void copyToVM(GroupsRolesView src, GroupsRolesViewVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (src.GroupId > 0)
				dest.GroupId = src.GroupId;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
			if (!String.IsNullOrEmpty(src.GroupName))
				dest.GroupName = src.GroupName;
			if (!String.IsNullOrEmpty(src.RoleDescription))
				dest.RoleDescription = src.RoleDescription;
			if (!String.IsNullOrEmpty(src.RoleName))
				dest.RoleName = src.RoleName;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
		}

	}
}
