using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IAspNetGroupsUsersService
	{
		List<AspNetGroupsUsersVM> Search(AspNetGroupsUsersVM model);
		bool Insert(AspNetGroupsUsersVM vm);
		bool Update(AspNetGroupsUsersVM vm);
		bool Delete(AspNetGroupsUsersVM vm);
		AspNetGroupsUsersVM GetById(int Id);
	}

	public class AspNetGroupsUsersService : IAspNetGroupsUsersService
	{
		private IEgyVisionRepository<AspNetGroupsUsers> _AspNetGroupsUsersRepo = null;
		public AspNetGroupsUsersService()
		{
			_AspNetGroupsUsersRepo = new EgyVisionRepository<AspNetGroupsUsers>();
		}

		public bool Insert(AspNetGroupsUsersVM vm)
		{
			AspNetGroupsUsers model = new AspNetGroupsUsers();
			copyToModel(vm,model);
			bool success = _AspNetGroupsUsersRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AspNetGroupsUsersVM vm)
		{
			AspNetGroupsUsers model = _AspNetGroupsUsersRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AspNetGroupsUsersRepo.Update(model);
		}

		public bool Delete(AspNetGroupsUsersVM vm)
		{
			AspNetGroupsUsers model = _AspNetGroupsUsersRepo.GetById(vm.Id);
			return _AspNetGroupsUsersRepo.Delete(model);
		}

		public List<AspNetGroupsUsersVM> Search(AspNetGroupsUsersVM model)
		{
			List<AspNetGroupsUsersVM> returned = new List<AspNetGroupsUsersVM>();
			var predicate = PredicateBuilder.New<AspNetGroupsUsers>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (!String.IsNullOrEmpty(model.UserId))
			//{
				//predicate = predicate.And(p => p.UserId == model.UserId);
			//}
			//if (model.GroupId > 0)
			//{
				//predicate = predicate.And(p => p.GroupId == model.GroupId);
			//}

			IQueryable<AspNetGroupsUsers> query = _AspNetGroupsUsersRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "UserId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "UserId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "GroupId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GroupId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.GroupId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (AspNetGroupsUsers record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AspNetGroupsUsersVM vm = new AspNetGroupsUsersVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AspNetGroupsUsersVM GetById(int Id)
		{
			AspNetGroupsUsers model = _AspNetGroupsUsersRepo.GetById(Id);
			AspNetGroupsUsersVM vm = new AspNetGroupsUsersVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AspNetGroupsUsersVM src, AspNetGroupsUsers dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			if (src.GroupId > 0)
				dest.GroupId = src.GroupId;
		}

		private void copyToVM(AspNetGroupsUsers src, AspNetGroupsUsersVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			if (src.GroupId > 0)
				dest.GroupId = src.GroupId;
		}

	}
}
