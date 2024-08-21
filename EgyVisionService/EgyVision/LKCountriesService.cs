using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKCountriesService
	{
		List<LKCountriesVM> Search(LKCountriesVM model);
		bool Insert(LKCountriesVM vm);
		bool Update(LKCountriesVM vm);
		bool Delete(LKCountriesVM vm);
		LKCountriesVM GetById(int LKCountryId);
	}

	public class LKCountriesService : ILKCountriesService
	{
		private IEgyVisionRepository<LKCountries> _LKCountriesRepo = null;
		public LKCountriesService()
		{
			_LKCountriesRepo = new EgyVisionRepository<LKCountries>();
		}

		public bool Insert(LKCountriesVM vm)
		{
			LKCountries model = new LKCountries();
			copyToModel(vm,model);
			bool success = _LKCountriesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKCountriesVM vm)
		{
			LKCountries model = _LKCountriesRepo.GetById(vm.LKCountryId);
			copyToModel(vm,model);
			return _LKCountriesRepo.Update(model);
		}

		public bool Delete(LKCountriesVM vm)
		{
			LKCountries model = _LKCountriesRepo.GetById(vm.LKCountryId);
			return _LKCountriesRepo.Delete(model);
		}

		public List<LKCountriesVM> Search(LKCountriesVM model)
		{
			List<LKCountriesVM> returned = new List<LKCountriesVM>();
			var predicate = PredicateBuilder.New<LKCountries>(true);

			//if (model.LKCountryId > 0)
			//{
				//predicate = predicate.And(p => p.LKCountryId == model.LKCountryId);
			//}
			//if (!String.IsNullOrEmpty(model.LKCountryNameAr))
			//{
				//predicate = predicate.And(p => p.LKCountryNameAr == model.LKCountryNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.LKCountryNameEn))
			//{
				//predicate = predicate.And(p => p.LKCountryNameEn == model.LKCountryNameEn);
			//}
			//if (model.Lng > 0)
			//{
				//predicate = predicate.And(p => p.Lng == model.Lng);
			//}
			//if (model.Lat > 0)
			//{
				//predicate = predicate.And(p => p.Lat == model.Lat);
			//}
			//if (model.AdminDevisionsLvlsCount > 0)
			//{
				//predicate = predicate.And(p => p.AdminDevisionsLvlsCount == model.AdminDevisionsLvlsCount);
			//}
			//if (model.LKCurrencyId > 0)
			//{
				//predicate = predicate.And(p => p.LKCurrencyId == model.LKCurrencyId);
			//}
			//if (!String.IsNullOrEmpty(model.CountryKey))
			//{
				//predicate = predicate.And(p => p.CountryKey == model.CountryKey);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);
			//if (model.TaxNumber > 0)
			//{
				//predicate = predicate.And(p => p.TaxNumber == model.TaxNumber);
			//}

			IQueryable<LKCountries> query = _LKCountriesRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKCountryId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKCountryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCountryId).Where(predicate);
			else if (model.OrderBy == "LKCountryId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCountryId).Where(predicate);
			else if (model.OrderBy == "LKCountryNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCountryNameAr).Where(predicate);
			else if (model.OrderBy == "LKCountryNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCountryNameAr).Where(predicate);
			else if (model.OrderBy == "LKCountryNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCountryNameEn).Where(predicate);
			else if (model.OrderBy == "LKCountryNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCountryNameEn).Where(predicate);
			else if (model.OrderBy == "Lng" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Lng).Where(predicate);
			else if (model.OrderBy == "Lng" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Lng).Where(predicate);
			else if (model.OrderBy == "Lat" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Lat).Where(predicate);
			else if (model.OrderBy == "Lat" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Lat).Where(predicate);
			else if (model.OrderBy == "AdminDevisionsLvlsCount" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AdminDevisionsLvlsCount).Where(predicate);
			else if (model.OrderBy == "AdminDevisionsLvlsCount" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AdminDevisionsLvlsCount).Where(predicate);
			else if (model.OrderBy == "LKCurrencyId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCurrencyId).Where(predicate);
			else if (model.OrderBy == "LKCurrencyId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCurrencyId).Where(predicate);
			else if (model.OrderBy == "CountryKey" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.CountryKey).Where(predicate);
			else if (model.OrderBy == "CountryKey" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.CountryKey).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "TaxNumber" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TaxNumber).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.TaxNumber).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKCountries record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKCountriesVM vm = new LKCountriesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKCountriesVM GetById(int LKCountryId)
		{
			LKCountries model = _LKCountriesRepo.GetById(LKCountryId);
			LKCountriesVM vm = new LKCountriesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKCountriesVM src, LKCountries dest)
		{
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
			if (!String.IsNullOrEmpty(src.LKCountryNameAr))
				dest.LKCountryNameAr = src.LKCountryNameAr;
			if (!String.IsNullOrEmpty(src.LKCountryNameEn))
				dest.LKCountryNameEn = src.LKCountryNameEn;
			if (src.Lng > 0)
				dest.Lng = src.Lng;
			if (src.Lat > 0)
				dest.Lat = src.Lat;
			dest.AdminDevisionsLvlsCount = src.AdminDevisionsLvlsCount;
			if (src.LKCurrencyId > 0)
				dest.LKCurrencyId = src.LKCurrencyId;
			if (!String.IsNullOrEmpty(src.CountryKey))
				dest.CountryKey = src.CountryKey;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (src.TaxNumber > 0)
				dest.TaxNumber = src.TaxNumber;
		}

		private void copyToVM(LKCountries src, LKCountriesVM dest)
		{
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
			if (!String.IsNullOrEmpty(src.LKCountryNameAr))
				dest.LKCountryNameAr = src.LKCountryNameAr;
			if (!String.IsNullOrEmpty(src.LKCountryNameEn))
				dest.LKCountryNameEn = src.LKCountryNameEn;
			if (src.Lng > 0)
				dest.Lng = src.Lng;
			if (src.Lat > 0)
				dest.Lat = src.Lat;
			dest.AdminDevisionsLvlsCount = src.AdminDevisionsLvlsCount;
			if (src.LKCurrencyId > 0)
				dest.LKCurrencyId = src.LKCurrencyId;
			if (!String.IsNullOrEmpty(src.CountryKey))
				dest.CountryKey = src.CountryKey;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (src.TaxNumber > 0)
				dest.TaxNumber = src.TaxNumber;
		}

	}
}
