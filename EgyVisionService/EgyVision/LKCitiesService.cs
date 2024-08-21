using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKCitiesService
	{
		List<LKCitiesVM> Search(LKCitiesVM model);
		bool Insert(LKCitiesVM vm);
		bool Update(LKCitiesVM vm);
		bool Delete(LKCitiesVM vm);
		LKCitiesVM GetById(long LKCityId);
	}

	public class LKCitiesService : ILKCitiesService
	{
		private IEgyVisionRepository<LKCities> _LKCitiesRepo = null;
		public LKCitiesService()
		{
			_LKCitiesRepo = new EgyVisionRepository<LKCities>();
		}

		public bool Insert(LKCitiesVM vm)
		{
			LKCities model = new LKCities();
			copyToModel(vm,model);
			bool success = _LKCitiesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKCitiesVM vm)
		{
			LKCities model = _LKCitiesRepo.GetById(vm.LKCityId);
			copyToModel(vm,model);
			return _LKCitiesRepo.Update(model);
		}

		public bool Delete(LKCitiesVM vm)
		{
			LKCities model = _LKCitiesRepo.GetById(vm.LKCityId);
			return _LKCitiesRepo.Delete(model);
		}

		public List<LKCitiesVM> Search(LKCitiesVM model)
		{
			List<LKCitiesVM> returned = new List<LKCitiesVM>();
			var predicate = PredicateBuilder.New<LKCities>(true);

			//if (model.LKCityId > 0)
			//{
				//predicate = predicate.And(p => p.LKCityId == model.LKCityId);
			//}
			//if (!String.IsNullOrEmpty(model.LKCityNameAr))
			//{
				//predicate = predicate.And(p => p.LKCityNameAr == model.LKCityNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.LKCityNameEn))
			//{
				//predicate = predicate.And(p => p.LKCityNameEn == model.LKCityNameEn);
			//}
			//if (model.LKRegionId > 0)
			//{
				//predicate = predicate.And(p => p.LKRegionId == model.LKRegionId);
			//}
			//if (model.LKCountryId > 0)
			//{
				//predicate = predicate.And(p => p.LKCountryId == model.LKCountryId);
			//}

			IQueryable<LKCities> query = _LKCitiesRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKCityId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKCityId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCityId).Where(predicate);
			else if (model.OrderBy == "LKCityId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCityId).Where(predicate);
			else if (model.OrderBy == "LKCityNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCityNameAr).Where(predicate);
			else if (model.OrderBy == "LKCityNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCityNameAr).Where(predicate);
			else if (model.OrderBy == "LKCityNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCityNameEn).Where(predicate);
			else if (model.OrderBy == "LKCityNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCityNameEn).Where(predicate);
			else if (model.OrderBy == "LKRegionId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKRegionId).Where(predicate);
			else if (model.OrderBy == "LKRegionId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKRegionId).Where(predicate);
			else if (model.OrderBy == "LKCountryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCountryId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.LKCountryId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKCities record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKCitiesVM vm = new LKCitiesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKCitiesVM GetById(long LKCityId)
		{
			LKCities model = _LKCitiesRepo.GetById(LKCityId);
			LKCitiesVM vm = new LKCitiesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKCitiesVM src, LKCities dest)
		{
			dest.LKCityId = src.LKCityId;
			if (!String.IsNullOrEmpty(src.LKCityNameAr))
				dest.LKCityNameAr = src.LKCityNameAr;
			if (!String.IsNullOrEmpty(src.LKCityNameEn))
				dest.LKCityNameEn = src.LKCityNameEn;
			dest.LKRegionId = src.LKRegionId;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
		}

		private void copyToVM(LKCities src, LKCitiesVM dest)
		{
			dest.LKCityId = src.LKCityId;
			if (!String.IsNullOrEmpty(src.LKCityNameAr))
				dest.LKCityNameAr = src.LKCityNameAr;
			if (!String.IsNullOrEmpty(src.LKCityNameEn))
				dest.LKCityNameEn = src.LKCityNameEn;
			dest.LKRegionId = src.LKRegionId;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
		}

	}
}
