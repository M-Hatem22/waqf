using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IAspNetRolesService
	{
		List<AspNetRolesVM> Search(AspNetRolesVM model);
		bool Insert(AspNetRolesVM vm);
		bool Update(AspNetRolesVM vm);
		bool Delete(AspNetRolesVM vm);
		AspNetRolesVM GetById(string Id);
	}

	public class AspNetRolesService : IAspNetRolesService
	{
		private IEgyVisionRepository<AspNetRoles> _AspNetRolesRepo = null;
		public AspNetRolesService()
		{
			_AspNetRolesRepo = new EgyVisionRepository<AspNetRoles>();
		}

		public bool Insert(AspNetRolesVM vm)
		{
			AspNetRoles model = new AspNetRoles();
			copyToModel(vm,model);
			bool success = _AspNetRolesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AspNetRolesVM vm)
		{
			AspNetRoles model = _AspNetRolesRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AspNetRolesRepo.Update(model);
		}

		public bool Delete(AspNetRolesVM vm)
		{
			AspNetRoles model = _AspNetRolesRepo.GetById(vm.Id);
			return _AspNetRolesRepo.Delete(model);
		}

		public List<AspNetRolesVM> Search(AspNetRolesVM model)
		{
			List<AspNetRolesVM> returned = new List<AspNetRolesVM>();
			var predicate = PredicateBuilder.New<AspNetRoles>(true);

			//if (!String.IsNullOrEmpty(model.Id))
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (!String.IsNullOrEmpty(model.Name))
			//{
				//predicate = predicate.And(p => p.Name == model.Name);
			//}
			//if (!String.IsNullOrEmpty(model.ConcurrencyStamp))
			//{
				//predicate = predicate.And(p => p.ConcurrencyStamp == model.ConcurrencyStamp);
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
			//if (model.DisplayOrder > 0)
			//{
				//predicate = predicate.And(p => p.DisplayOrder == model.DisplayOrder);
			//}

			IQueryable<AspNetRoles> query = _AspNetRolesRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "Name" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Name).Where(predicate);
			else if (model.OrderBy == "ConcurrencyStamp" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ConcurrencyStamp).Where(predicate);
			else if (model.OrderBy == "ConcurrencyStamp" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ConcurrencyStamp).Where(predicate);
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
			else if (model.OrderBy == "RequestTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.RequestTypeId).Where(predicate);
			else if (model.OrderBy == "DisplayOrder" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.DisplayOrder).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.DisplayOrder).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (AspNetRoles record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AspNetRolesVM vm = new AspNetRolesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AspNetRolesVM GetById(string Id)
		{
			AspNetRoles model = _AspNetRolesRepo.GetById(Id);
			AspNetRolesVM vm = new AspNetRolesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AspNetRolesVM src, AspNetRoles dest)
		{
			if (!String.IsNullOrEmpty(src.Id))
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.Name))
				dest.Name = src.Name;
			if (!String.IsNullOrEmpty(src.ConcurrencyStamp))
				dest.ConcurrencyStamp = src.ConcurrencyStamp;
			if (!String.IsNullOrEmpty(src.NormalizedName))
				dest.NormalizedName = src.NormalizedName;
			if (!String.IsNullOrEmpty(src.Description))
				dest.Description = src.Description;
			if (src.RequestTypeId > 0)
				dest.RequestTypeId = src.RequestTypeId;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
		}

		private void copyToVM(AspNetRoles src, AspNetRolesVM dest)
		{
			if (!String.IsNullOrEmpty(src.Id))
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.Name))
				dest.Name = src.Name;
			if (!String.IsNullOrEmpty(src.ConcurrencyStamp))
				dest.ConcurrencyStamp = src.ConcurrencyStamp;
			if (!String.IsNullOrEmpty(src.NormalizedName))
				dest.NormalizedName = src.NormalizedName;
			if (!String.IsNullOrEmpty(src.Description))
				dest.Description = src.Description;
			if (src.RequestTypeId > 0)
				dest.RequestTypeId = src.RequestTypeId;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
		}

	}
}
