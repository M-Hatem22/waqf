using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKMenusService
	{
		List<LKMenusVM> Search(LKMenusVM model);
		bool Insert(LKMenusVM vm);
		bool Update(LKMenusVM vm);
		bool Delete(LKMenusVM vm);
		LKMenusVM GetById(int LKMenuId);
	}

	public class LKMenusService : ILKMenusService
	{
		private IEgyVisionRepository<LKMenus> _LKMenusRepo = null;
		public LKMenusService()
		{
			_LKMenusRepo = new EgyVisionRepository<LKMenus>();
		}

		public bool Insert(LKMenusVM vm)
		{
			LKMenus model = new LKMenus();
			copyToModel(vm,model);
			bool success = _LKMenusRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKMenusVM vm)
		{
			LKMenus model = _LKMenusRepo.GetById(vm.LKMenuId);
			copyToModel(vm,model);
			return _LKMenusRepo.Update(model);
		}

		public bool Delete(LKMenusVM vm)
		{
			LKMenus model = _LKMenusRepo.GetById(vm.LKMenuId);
			return _LKMenusRepo.Delete(model);
		}

		public List<LKMenusVM> Search(LKMenusVM model)
		{
			List<LKMenusVM> returned = new List<LKMenusVM>();
			var predicate = PredicateBuilder.New<LKMenus>(true);

			//if (model.LKMenuId > 0)
			//{
				//predicate = predicate.And(p => p.LKMenuId == model.LKMenuId);
			//}
			//if (model.ParentId > 0)
			//{
				//predicate = predicate.And(p => p.ParentId == model.ParentId);
			//}
			//if (!String.IsNullOrEmpty(model.MenuNameAr))
			//{
				//predicate = predicate.And(p => p.MenuNameAr == model.MenuNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.MenuNameEn))
			//{
				//predicate = predicate.And(p => p.MenuNameEn == model.MenuNameEn);
			//}
			//if (model.DisplayOrder > 0)
			//{
				//predicate = predicate.And(p => p.DisplayOrder == model.DisplayOrder);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);

			IQueryable<LKMenus> query = _LKMenusRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKMenuId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKMenuId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKMenuId).Where(predicate);
			else if (model.OrderBy == "LKMenuId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKMenuId).Where(predicate);
			else if (model.OrderBy == "ParentId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ParentId).Where(predicate);
			else if (model.OrderBy == "ParentId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ParentId).Where(predicate);
			else if (model.OrderBy == "MenuNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.MenuNameAr).Where(predicate);
			else if (model.OrderBy == "MenuNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.MenuNameAr).Where(predicate);
			else if (model.OrderBy == "MenuNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.MenuNameEn).Where(predicate);
			else if (model.OrderBy == "MenuNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.MenuNameEn).Where(predicate);
			else if (model.OrderBy == "DisplayOrder" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.DisplayOrder).Where(predicate);
			else if (model.OrderBy == "DisplayOrder" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.DisplayOrder).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKMenus record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKMenusVM vm = new LKMenusVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKMenusVM GetById(int LKMenuId)
		{
			LKMenus model = _LKMenusRepo.GetById(LKMenuId);
			LKMenusVM vm = new LKMenusVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKMenusVM src, LKMenus dest)
		{
			if (src.LKMenuId > 0)
				dest.LKMenuId = src.LKMenuId;
			if (src.ParentId > 0)
				dest.ParentId = src.ParentId;
			if (!String.IsNullOrEmpty(src.MenuNameAr))
				dest.MenuNameAr = src.MenuNameAr;
			if (!String.IsNullOrEmpty(src.MenuNameEn))
				dest.MenuNameEn = src.MenuNameEn;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

		private void copyToVM(LKMenus src, LKMenusVM dest)
		{
			if (src.LKMenuId > 0)
				dest.LKMenuId = src.LKMenuId;
			if (src.ParentId > 0)
				dest.ParentId = src.ParentId;
			if (!String.IsNullOrEmpty(src.MenuNameAr))
				dest.MenuNameAr = src.MenuNameAr;
			if (!String.IsNullOrEmpty(src.MenuNameEn))
				dest.MenuNameEn = src.MenuNameEn;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

	}
}
