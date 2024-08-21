using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKMenuCatContentService
	{
		List<LKMenuCatContentVM> Search(LKMenuCatContentVM model);
		bool Insert(LKMenuCatContentVM vm);
		bool Update(LKMenuCatContentVM vm);
		bool Delete(LKMenuCatContentVM vm);
		LKMenuCatContentVM GetById(int ID);
	}

	public class LKMenuCatContentService : ILKMenuCatContentService
	{
		private IEgyVisionRepository<LKMenuCatContent> _LKMenuCatContentRepo = null;
		public LKMenuCatContentService()
		{
			_LKMenuCatContentRepo = new EgyVisionRepository<LKMenuCatContent>();
		}

		public bool Insert(LKMenuCatContentVM vm)
		{
			LKMenuCatContent model = new LKMenuCatContent();
			copyToModel(vm,model);
			bool success = _LKMenuCatContentRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKMenuCatContentVM vm)
		{
			LKMenuCatContent model = _LKMenuCatContentRepo.GetById(vm.ID);
			copyToModel(vm,model);
			return _LKMenuCatContentRepo.Update(model);
		}

		public bool Delete(LKMenuCatContentVM vm)
		{
			LKMenuCatContent model = _LKMenuCatContentRepo.GetById(vm.ID);
			return _LKMenuCatContentRepo.Delete(model);
		}

		public List<LKMenuCatContentVM> Search(LKMenuCatContentVM model)
		{
			List<LKMenuCatContentVM> returned = new List<LKMenuCatContentVM>();
			var predicate = PredicateBuilder.New<LKMenuCatContent>(true);

            if (model.ID > 0)
            {
                predicate = predicate.And(p => p.ID == model.ID);
            }
            if (!String.IsNullOrEmpty(model.TitleAr))
            {
                predicate = predicate.And(p => p.TitleAr == model.TitleAr);
            }
            if (!String.IsNullOrEmpty(model.TitleEn))
            {
                predicate = predicate.And(p => p.TitleEn == model.TitleEn);
            }
            if (!String.IsNullOrEmpty(model.ContentAr))
            {
                predicate = predicate.And(p => p.ContentAr == model.ContentAr);
            }
            if (!String.IsNullOrEmpty(model.ContentEn))
            {
                predicate = predicate.And(p => p.ContentEn == model.ContentEn);
            }
			if (model.MenuCatId > 0)
			{
				predicate = predicate.And(p => p.MenuCatId == model.MenuCatId);
			}

			IQueryable<LKMenuCatContent> query = _LKMenuCatContentRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "ID";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "ID" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ID).Where(predicate);
			else if (model.OrderBy == "ID" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ID).Where(predicate);
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
			else
				query = query.AsExpandable().OrderBy(x => x.ContentEn).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKMenuCatContent record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKMenuCatContentVM vm = new LKMenuCatContentVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKMenuCatContentVM GetById(int ID)
		{
			LKMenuCatContent model = _LKMenuCatContentRepo.GetById(ID);
			LKMenuCatContentVM vm = new LKMenuCatContentVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKMenuCatContentVM src, LKMenuCatContent dest)
		{
			if (src.ID > 0)
				dest.ID = src.ID; 
			if (src.MenuCatId > 0)
				dest.MenuCatId = src.MenuCatId;
			if (!String.IsNullOrEmpty(src.TitleAr))
				dest.TitleAr = src.TitleAr;
			if (!String.IsNullOrEmpty(src.TitleEn))
				dest.TitleEn = src.TitleEn;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
		}

		private void copyToVM(LKMenuCatContent src, LKMenuCatContentVM dest)
		{
			if (src.ID > 0)
				dest.ID = src.ID;
			if (src.MenuCatId > 0)
				dest.MenuCatId = src.MenuCatId;
			if (!String.IsNullOrEmpty(src.TitleAr))
				dest.TitleAr = src.TitleAr;
			if (!String.IsNullOrEmpty(src.TitleEn))
				dest.TitleEn = src.TitleEn;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
		}

	}
}
