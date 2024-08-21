using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyVisionService.EgyVision
{
    public interface IAspNetGroupsService
	{
		List<AspNetGroupsVM> Search(AspNetGroupsVM model);
		bool Insert(AspNetGroupsVM vm);
		bool Update(AspNetGroupsVM vm);
		bool Delete(AspNetGroupsVM vm);
		AspNetGroupsVM GetById(int Id);
	}

	public class AspNetGroupsService : IAspNetGroupsService
	{
		private IEgyVisionRepository<AspNetGroups> _AspNetGroupsRepo = null;
		public AspNetGroupsService()
		{
			_AspNetGroupsRepo = new EgyVisionRepository<AspNetGroups>();
		}

		public bool Insert(AspNetGroupsVM vm)
		{
			AspNetGroups model = new AspNetGroups();
			copyToModel(vm,model);
			bool success = _AspNetGroupsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AspNetGroupsVM vm)
		{
			AspNetGroups model = _AspNetGroupsRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AspNetGroupsRepo.Update(model);
		}

		public bool Delete(AspNetGroupsVM vm)
		{
			AspNetGroups model = _AspNetGroupsRepo.GetById(vm.Id);
			return _AspNetGroupsRepo.Delete(model);
		}

		public List<AspNetGroupsVM> Search(AspNetGroupsVM model)
		{
			List<AspNetGroupsVM> returned = new List<AspNetGroupsVM>();
			var predicate = PredicateBuilder.New<AspNetGroups>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (!String.IsNullOrEmpty(model.Name))
			//{
				//predicate = predicate.And(p => p.Name == model.Name);
			//}

			IQueryable<AspNetGroups> query = _AspNetGroupsRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "Name" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Name).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Name).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (AspNetGroups record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AspNetGroupsVM vm = new AspNetGroupsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AspNetGroupsVM GetById(int Id)
		{
			AspNetGroups model = _AspNetGroupsRepo.GetById(Id);
			AspNetGroupsVM vm = new AspNetGroupsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AspNetGroupsVM src, AspNetGroups dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.Name))
				dest.Name = src.Name;
		}

		private void copyToVM(AspNetGroups src, AspNetGroupsVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.Name))
				dest.Name = src.Name;
		}

	}
}
