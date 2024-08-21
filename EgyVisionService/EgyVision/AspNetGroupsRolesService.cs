using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IAspNetGroupsRolesService
	{
		List<AspNetGroupsRolesVM> Search(AspNetGroupsRolesVM model);
		bool Insert(AspNetGroupsRolesVM vm);
		bool Update(AspNetGroupsRolesVM vm);
		bool Delete(AspNetGroupsRolesVM vm);
		AspNetGroupsRolesVM GetById(int Id);
	}

	public class AspNetGroupsRolesService : IAspNetGroupsRolesService
	{
		private IEgyVisionRepository<AspNetGroupsRoles> _AspNetGroupsRolesRepo = null;
		public AspNetGroupsRolesService()
		{
			_AspNetGroupsRolesRepo = new EgyVisionRepository<AspNetGroupsRoles>();
		}

		public bool Insert(AspNetGroupsRolesVM vm)
		{
			AspNetGroupsRoles model = new AspNetGroupsRoles();
			copyToModel(vm,model);
			bool success = _AspNetGroupsRolesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AspNetGroupsRolesVM vm)
		{
			AspNetGroupsRoles model = _AspNetGroupsRolesRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AspNetGroupsRolesRepo.Update(model);
		}

		public bool Delete(AspNetGroupsRolesVM vm)
		{
			AspNetGroupsRoles model = _AspNetGroupsRolesRepo.GetById(vm.Id);
			return _AspNetGroupsRolesRepo.Delete(model);
		}

		public List<AspNetGroupsRolesVM> Search(AspNetGroupsRolesVM model)
		{
			List<AspNetGroupsRolesVM> returned = new List<AspNetGroupsRolesVM>();
			var predicate = PredicateBuilder.New<AspNetGroupsRoles>(true);

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

			IQueryable<AspNetGroupsRoles> query = _AspNetGroupsRolesRepo.Table.AsExpandable().Where(predicate);

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
			else
				query = query.AsExpandable().OrderBy(x => x.RoleId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (AspNetGroupsRoles record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AspNetGroupsRolesVM vm = new AspNetGroupsRolesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AspNetGroupsRolesVM GetById(int Id)
		{
			AspNetGroupsRoles model = _AspNetGroupsRolesRepo.GetById(Id);
			AspNetGroupsRolesVM vm = new AspNetGroupsRolesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AspNetGroupsRolesVM src, AspNetGroupsRoles dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (src.GroupId > 0)
				dest.GroupId = src.GroupId;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
		}

		private void copyToVM(AspNetGroupsRoles src, AspNetGroupsRolesVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (src.GroupId > 0)
				dest.GroupId = src.GroupId;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
		}

	}
}
