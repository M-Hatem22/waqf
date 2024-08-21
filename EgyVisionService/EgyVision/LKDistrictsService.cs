using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKDistrictsService
	{
		List<LKDistrictsVM> Search(LKDistrictsVM model);
		bool Insert(LKDistrictsVM vm);
		bool Update(LKDistrictsVM vm);
		bool Delete(LKDistrictsVM vm);
		LKDistrictsVM GetById(long LKDistrictId);
	}

	public class LKDistrictsService : ILKDistrictsService
	{
		private IEgyVisionRepository<LKDistricts> _LKDistrictsRepo = null;
		public LKDistrictsService()
		{
			_LKDistrictsRepo = new EgyVisionRepository<LKDistricts>();
		}

		public bool Insert(LKDistrictsVM vm)
		{
			LKDistricts model = new LKDistricts();
			copyToModel(vm,model);
			bool success = _LKDistrictsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKDistrictsVM vm)
		{
			LKDistricts model = _LKDistrictsRepo.GetById(vm.LKDistrictId);
			copyToModel(vm,model);
			return _LKDistrictsRepo.Update(model);
		}

		public bool Delete(LKDistrictsVM vm)
		{
			LKDistricts model = _LKDistrictsRepo.GetById(vm.LKDistrictId);
			return _LKDistrictsRepo.Delete(model);
		}

		public List<LKDistrictsVM> Search(LKDistrictsVM model)
		{
			List<LKDistrictsVM> returned = new List<LKDistrictsVM>();
			var predicate = PredicateBuilder.New<LKDistricts>(true);

			//if (model.LKDistrictId > 0)
			//{
				//predicate = predicate.And(p => p.LKDistrictId == model.LKDistrictId);
			//}
			//if (!String.IsNullOrEmpty(model.LKDistrictNameAr))
			//{
				//predicate = predicate.And(p => p.LKDistrictNameAr == model.LKDistrictNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.LKDistrictNameEn))
			//{
				//predicate = predicate.And(p => p.LKDistrictNameEn == model.LKDistrictNameEn);
			//}
			//if (model.LKCityId > 0)
			//{
				//predicate = predicate.And(p => p.LKCityId == model.LKCityId);
			//}
			//if (model.LKCountryId > 0)
			//{
				//predicate = predicate.And(p => p.LKCountryId == model.LKCountryId);
			//}

			IQueryable<LKDistricts> query = _LKDistrictsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKDistrictId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKDistrictId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKDistrictId).Where(predicate);
			else if (model.OrderBy == "LKDistrictId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKDistrictId).Where(predicate);
			else if (model.OrderBy == "LKDistrictNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKDistrictNameAr).Where(predicate);
			else if (model.OrderBy == "LKDistrictNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKDistrictNameAr).Where(predicate);
			else if (model.OrderBy == "LKDistrictNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKDistrictNameEn).Where(predicate);
			else if (model.OrderBy == "LKDistrictNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKDistrictNameEn).Where(predicate);
			else if (model.OrderBy == "LKCityId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCityId).Where(predicate);
			else if (model.OrderBy == "LKCityId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCityId).Where(predicate);
			else if (model.OrderBy == "LKCountryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCountryId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.LKCountryId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKDistricts record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKDistrictsVM vm = new LKDistrictsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKDistrictsVM GetById(long LKDistrictId)
		{
			LKDistricts model = _LKDistrictsRepo.GetById(LKDistrictId);
			LKDistrictsVM vm = new LKDistrictsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKDistrictsVM src, LKDistricts dest)
		{
			dest.LKDistrictId = src.LKDistrictId;
			if (!String.IsNullOrEmpty(src.LKDistrictNameAr))
				dest.LKDistrictNameAr = src.LKDistrictNameAr;
			if (!String.IsNullOrEmpty(src.LKDistrictNameEn))
				dest.LKDistrictNameEn = src.LKDistrictNameEn;
			dest.LKCityId = src.LKCityId;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
		}

		private void copyToVM(LKDistricts src, LKDistrictsVM dest)
		{
			dest.LKDistrictId = src.LKDistrictId;
			if (!String.IsNullOrEmpty(src.LKDistrictNameAr))
				dest.LKDistrictNameAr = src.LKDistrictNameAr;
			if (!String.IsNullOrEmpty(src.LKDistrictNameEn))
				dest.LKDistrictNameEn = src.LKDistrictNameEn;
			dest.LKCityId = src.LKCityId;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
		}

	}
}
