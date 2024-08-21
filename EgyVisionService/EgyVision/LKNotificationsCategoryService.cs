using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKNotificationsCategoryService
	{
		List<LKNotificationsCategoryVM> Search(LKNotificationsCategoryVM model);
		bool Insert(LKNotificationsCategoryVM vm);
		bool Update(LKNotificationsCategoryVM vm);
		bool Delete(LKNotificationsCategoryVM vm);
		LKNotificationsCategoryVM GetById(int CategoryId);
	}

	public class LKNotificationsCategoryService : ILKNotificationsCategoryService
	{
		private IEgyVisionRepository<LKNotificationsCategory> _LKNotificationsCategoryRepo = null;
		public LKNotificationsCategoryService()
		{
			_LKNotificationsCategoryRepo = new EgyVisionRepository<LKNotificationsCategory>();
		}

		public bool Insert(LKNotificationsCategoryVM vm)
		{
			LKNotificationsCategory model = new LKNotificationsCategory();
			copyToModel(vm,model);
			bool success = _LKNotificationsCategoryRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKNotificationsCategoryVM vm)
		{
			LKNotificationsCategory model = _LKNotificationsCategoryRepo.GetById(vm.CategoryId);
			copyToModel(vm,model);
			return _LKNotificationsCategoryRepo.Update(model);
		}

		public bool Delete(LKNotificationsCategoryVM vm)
		{
			LKNotificationsCategory model = _LKNotificationsCategoryRepo.GetById(vm.CategoryId);
			return _LKNotificationsCategoryRepo.Delete(model);
		}

		public List<LKNotificationsCategoryVM> Search(LKNotificationsCategoryVM model)
		{
			List<LKNotificationsCategoryVM> returned = new List<LKNotificationsCategoryVM>();
			var predicate = PredicateBuilder.New<LKNotificationsCategory>(true);

			//if (model.CategoryId > 0)
			//{
				//predicate = predicate.And(p => p.CategoryId == model.CategoryId);
			//}
			//if (!String.IsNullOrEmpty(model.CategoryTxt))
			//{
				//predicate = predicate.And(p => p.CategoryTxt == model.CategoryTxt);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);

			IQueryable<LKNotificationsCategory> query = _LKNotificationsCategoryRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "CategoryId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "CategoryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.CategoryId).Where(predicate);
			else if (model.OrderBy == "CategoryId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.CategoryId).Where(predicate);
			else if (model.OrderBy == "CategoryTxt" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.CategoryTxt).Where(predicate);
			else if (model.OrderBy == "CategoryTxt" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.CategoryTxt).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKNotificationsCategory record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKNotificationsCategoryVM vm = new LKNotificationsCategoryVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKNotificationsCategoryVM GetById(int CategoryId)
		{
			LKNotificationsCategory model = _LKNotificationsCategoryRepo.GetById(CategoryId);
			LKNotificationsCategoryVM vm = new LKNotificationsCategoryVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKNotificationsCategoryVM src, LKNotificationsCategory dest)
		{
			if (src.CategoryId > 0)
				dest.CategoryId = src.CategoryId;
			if (!String.IsNullOrEmpty(src.CategoryTxt))
				dest.CategoryTxt = src.CategoryTxt;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

		private void copyToVM(LKNotificationsCategory src, LKNotificationsCategoryVM dest)
		{
			if (src.CategoryId > 0)
				dest.CategoryId = src.CategoryId;
			if (!String.IsNullOrEmpty(src.CategoryTxt))
				dest.CategoryTxt = src.CategoryTxt;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

	}
}
