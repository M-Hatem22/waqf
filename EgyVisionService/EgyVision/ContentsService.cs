using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IContentsService
	{
		List<ContentsVM> Search(ContentsVM model);
		bool Insert(ContentsVM vm);
		bool Update(ContentsVM vm);
		bool Delete(ContentsVM vm);
		ContentsVM GetById(long ContentId);
	}

	public class ContentsService : IContentsService
	{
		private IEgyVisionRepository<Contents> _ContentsRepo = null;
		public ContentsService()
		{
			_ContentsRepo = new EgyVisionRepository<Contents>();
		}

		public bool Insert(ContentsVM vm)
		{
			Contents model = new Contents();
			copyToModel(vm,model);
			bool success = _ContentsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(ContentsVM vm)
		{
			Contents model = _ContentsRepo.GetById(vm.ContentId);
			copyToModel(vm,model);
			return _ContentsRepo.Update(model);
		}

		public bool Delete(ContentsVM vm)
		{
			Contents model = _ContentsRepo.GetById(vm.ContentId);
			return _ContentsRepo.Delete(model);
		}

		public List<ContentsVM> Search(ContentsVM model)
		{
			List<ContentsVM> returned = new List<ContentsVM>();
			var predicate = PredicateBuilder.New<Contents>(true);

			//if (model.ContentId > 0)
			//{
				//predicate = predicate.And(p => p.ContentId == model.ContentId);
			//}
			//if (model.LKMenuId > 0)
			//{
				//predicate = predicate.And(p => p.LKMenuId == model.LKMenuId);
			//}
			//if (!String.IsNullOrEmpty(model.TitleAr))
			//{
				//predicate = predicate.And(p => p.TitleAr == model.TitleAr);
			//}
			//if (!String.IsNullOrEmpty(model.TitleEn))
			//{
				//predicate = predicate.And(p => p.TitleEn == model.TitleEn);
			//}
			//if (!String.IsNullOrEmpty(model.ContentAr))
			//{
				//predicate = predicate.And(p => p.ContentAr == model.ContentAr);
			//}
			//if (!String.IsNullOrEmpty(model.ContentEn))
			//{
				//predicate = predicate.And(p => p.ContentEn == model.ContentEn);
			//}
			//if (model.DisplayOrder > 0)
			//{
				//predicate = predicate.And(p => p.DisplayOrder == model.DisplayOrder);
			//}
				//predicate = predicate.And(p => p.TimInsert == model.TimInsert);
			//if (!String.IsNullOrEmpty(model.UserInsert))
			//{
				//predicate = predicate.And(p => p.UserInsert == model.UserInsert);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);
			//if (!String.IsNullOrEmpty(model.UserDeleted))
			//{
				//predicate = predicate.And(p => p.UserDeleted == model.UserDeleted);
			//}
				//predicate = predicate.And(p => p.IsActive == model.IsActive);

			IQueryable<Contents> query = _ContentsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "ContentId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "ContentId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ContentId).Where(predicate);
			else if (model.OrderBy == "ContentId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ContentId).Where(predicate);
			else if (model.OrderBy == "LKMenuId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKMenuId).Where(predicate);
			else if (model.OrderBy == "LKMenuId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKMenuId).Where(predicate);
			else if (model.OrderBy == "TitleAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TitleAr).Where(predicate);
			else if (model.OrderBy == "TitleAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TitleAr).Where(predicate);
			else if (model.OrderBy == "TitleEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TitleEn).Where(predicate);
			else if (model.OrderBy == "TitleEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TitleEn).Where(predicate);
			else if (model.OrderBy == "ContentAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ContentAr).Where(predicate);
			else if (model.OrderBy == "ContentAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ContentAr).Where(predicate);
			else if (model.OrderBy == "ContentEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ContentEn).Where(predicate);
			else if (model.OrderBy == "ContentEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ContentEn).Where(predicate);
			else if (model.OrderBy == "DisplayOrder" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.DisplayOrder).Where(predicate);
			else if (model.OrderBy == "DisplayOrder" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.DisplayOrder).Where(predicate);
			else if (model.OrderBy == "TimInsert" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TimInsert).Where(predicate);
			else if (model.OrderBy == "TimInsert" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TimInsert).Where(predicate);
			else if (model.OrderBy == "UserInsert" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserInsert).Where(predicate);
			else if (model.OrderBy == "UserInsert" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserInsert).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "UserDeleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserDeleted).Where(predicate);
			else if (model.OrderBy == "UserDeleted" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserDeleted).Where(predicate);
			else if (model.OrderBy == "IsActive" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.IsActive).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.IsActive).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (Contents record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					ContentsVM vm = new ContentsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public ContentsVM GetById(long ContentId)
		{
			Contents model = _ContentsRepo.GetById(ContentId);
			ContentsVM vm = new ContentsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(ContentsVM src, Contents dest)
		{
			dest.ContentId = src.ContentId;
			if (src.LKMenuId > 0)
				dest.LKMenuId = src.LKMenuId;
			if (!String.IsNullOrEmpty(src.TitleAr))
				dest.TitleAr = src.TitleAr;
			if (!String.IsNullOrEmpty(src.TitleEn))
				dest.TitleEn = src.TitleEn;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
			if (src.TimInsert != null && src.TimInsert != DateTime.MinValue)
				dest.TimInsert = src.TimInsert;
			if (!String.IsNullOrEmpty(src.UserInsert))
				dest.UserInsert = src.UserInsert;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (!String.IsNullOrEmpty(src.UserDeleted))
				dest.UserDeleted = src.UserDeleted;
			dest.IsActive = src.IsActive;
		}

		private void copyToVM(Contents src, ContentsVM dest)
		{
			dest.ContentId = src.ContentId;
			if (src.LKMenuId > 0)
				dest.LKMenuId = src.LKMenuId;
			if (!String.IsNullOrEmpty(src.TitleAr))
				dest.TitleAr = src.TitleAr;
			if (!String.IsNullOrEmpty(src.TitleEn))
				dest.TitleEn = src.TitleEn;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
			if (src.TimInsert != null && src.TimInsert != DateTime.MinValue)
				dest.TimInsert = src.TimInsert;
			if (!String.IsNullOrEmpty(src.UserInsert))
				dest.UserInsert = src.UserInsert;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (!String.IsNullOrEmpty(src.UserDeleted))
				dest.UserDeleted = src.UserDeleted;
			dest.IsActive = src.IsActive;
		}

	}
}
