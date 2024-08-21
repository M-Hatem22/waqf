using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKRegionsService
	{
		List<LKRegionsVM> Search(LKRegionsVM model);
		bool Insert(LKRegionsVM vm);
		bool Update(LKRegionsVM vm);
		bool Delete(LKRegionsVM vm);
		LKRegionsVM GetById(long LKRegionId);
	}

	public class LKRegionsService : ILKRegionsService
	{
		private IEgyVisionRepository<LKRegions> _LKRegionsRepo = null;
		public LKRegionsService()
		{
			_LKRegionsRepo = new EgyVisionRepository<LKRegions>();
		}

		public bool Insert(LKRegionsVM vm)
		{
			LKRegions model = new LKRegions();
			copyToModel(vm,model);
			bool success = _LKRegionsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKRegionsVM vm)
		{
			LKRegions model = _LKRegionsRepo.GetById(vm.LKRegionId);
			copyToModel(vm,model);
			return _LKRegionsRepo.Update(model);
		}

		public bool Delete(LKRegionsVM vm)
		{
			LKRegions model = _LKRegionsRepo.GetById(vm.LKRegionId);
			return _LKRegionsRepo.Delete(model);
		}

		public List<LKRegionsVM> Search(LKRegionsVM model)
		{
			List<LKRegionsVM> returned = new List<LKRegionsVM>();
			var predicate = PredicateBuilder.New<LKRegions>(true);

			//if (model.LKRegionId > 0)
			//{
				//predicate = predicate.And(p => p.LKRegionId == model.LKRegionId);
			//}
			//if (!String.IsNullOrEmpty(model.LKRegionNameAr))
			//{
				//predicate = predicate.And(p => p.LKRegionNameAr == model.LKRegionNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.LKRegionNameEn))
			//{
				//predicate = predicate.And(p => p.LKRegionNameEn == model.LKRegionNameEn);
			//}
			//if (model.LKCountryId > 0)
			//{
				//predicate = predicate.And(p => p.LKCountryId == model.LKCountryId);
			//}

			IQueryable<LKRegions> query = _LKRegionsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKRegionId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKRegionId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKRegionId).Where(predicate);
			else if (model.OrderBy == "LKRegionId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKRegionId).Where(predicate);
			else if (model.OrderBy == "LKRegionNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKRegionNameAr).Where(predicate);
			else if (model.OrderBy == "LKRegionNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKRegionNameAr).Where(predicate);
			else if (model.OrderBy == "LKRegionNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKRegionNameEn).Where(predicate);
			else if (model.OrderBy == "LKRegionNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKRegionNameEn).Where(predicate);
			else if (model.OrderBy == "LKCountryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCountryId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.LKCountryId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKRegions record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKRegionsVM vm = new LKRegionsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKRegionsVM GetById(long LKRegionId)
		{
			LKRegions model = _LKRegionsRepo.GetById(LKRegionId);
			LKRegionsVM vm = new LKRegionsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKRegionsVM src, LKRegions dest)
		{
			dest.LKRegionId = src.LKRegionId;
			if (!String.IsNullOrEmpty(src.LKRegionNameAr))
				dest.LKRegionNameAr = src.LKRegionNameAr;
			if (!String.IsNullOrEmpty(src.LKRegionNameEn))
				dest.LKRegionNameEn = src.LKRegionNameEn;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
		}

		private void copyToVM(LKRegions src, LKRegionsVM dest)
		{
			dest.LKRegionId = src.LKRegionId;
			if (!String.IsNullOrEmpty(src.LKRegionNameAr))
				dest.LKRegionNameAr = src.LKRegionNameAr;
			if (!String.IsNullOrEmpty(src.LKRegionNameEn))
				dest.LKRegionNameEn = src.LKRegionNameEn;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
		}

	}
}
