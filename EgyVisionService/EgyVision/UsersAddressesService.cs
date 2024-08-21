using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IUsersAddressesService
	{
		List<UsersAddressesVM> Search(UsersAddressesVM model);
		bool Insert(UsersAddressesVM vm);
		bool Update(UsersAddressesVM vm);
		bool Delete(UsersAddressesVM vm);
		UsersAddressesVM GetById(long AddressId);
	}

	public class UsersAddressesService : IUsersAddressesService
	{
		private IEgyVisionRepository<UsersAddresses> _UsersAddressesRepo = null;
		public UsersAddressesService()
		{
			_UsersAddressesRepo = new EgyVisionRepository<UsersAddresses>();
		}

		public bool Insert(UsersAddressesVM vm)
		{
			UsersAddresses model = new UsersAddresses();
			copyToModel(vm,model);
			bool success = _UsersAddressesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(UsersAddressesVM vm)
		{
			UsersAddresses model = _UsersAddressesRepo.GetById(vm.AddressId);
			copyToModel(vm,model);
			return _UsersAddressesRepo.Update(model);
		}

		public bool Delete(UsersAddressesVM vm)
		{
			UsersAddresses model = _UsersAddressesRepo.GetById(vm.AddressId);
			return _UsersAddressesRepo.Delete(model);
		}

		public List<UsersAddressesVM> Search(UsersAddressesVM model)
		{
			List<UsersAddressesVM> returned = new List<UsersAddressesVM>();
			var predicate = PredicateBuilder.New<UsersAddresses>(true);

			//if (model.AddressId > 0)
			//{
				//predicate = predicate.And(p => p.AddressId == model.AddressId);
			//}
			//if (!String.IsNullOrEmpty(model.UserId))
			//{
				//predicate = predicate.And(p => p.UserId == model.UserId);
			//}
				//predicate = predicate.And(p => p.IsMain == model.IsMain);
			//if (model.LKCountryId > 0)
			//{
				//predicate = predicate.And(p => p.LKCountryId == model.LKCountryId);
			//}
			//if (model.LKRegionId > 0)
			//{
				//predicate = predicate.And(p => p.LKRegionId == model.LKRegionId);
			//}
			//if (model.LKCityId > 0)
			//{
				//predicate = predicate.And(p => p.LKCityId == model.LKCityId);
			//}
			//if (model.LKDistrictId > 0)
			//{
				//predicate = predicate.And(p => p.LKDistrictId == model.LKDistrictId);
			//}
			//if (!String.IsNullOrEmpty(model.FullAddressAr))
			//{
				//predicate = predicate.And(p => p.FullAddressAr == model.FullAddressAr);
			//}
			//if (!String.IsNullOrEmpty(model.FullAddressEn))
			//{
				//predicate = predicate.And(p => p.FullAddressEn == model.FullAddressEn);
			//}
			//if (!String.IsNullOrEmpty(model.StreetAr))
			//{
				//predicate = predicate.And(p => p.StreetAr == model.StreetAr);
			//}
			//if (!String.IsNullOrEmpty(model.StreetEn))
			//{
				//predicate = predicate.And(p => p.StreetEn == model.StreetEn);
			//}
			//if (!String.IsNullOrEmpty(model.PostalCode))
			//{
				//predicate = predicate.And(p => p.PostalCode == model.PostalCode);
			//}
			//if (!String.IsNullOrEmpty(model.AdditionalCode))
			//{
				//predicate = predicate.And(p => p.AdditionalCode == model.AdditionalCode);
			//}
			//if (!String.IsNullOrEmpty(model.BuildingNo))
			//{
				//predicate = predicate.And(p => p.BuildingNo == model.BuildingNo);
			//}
			//if (!String.IsNullOrEmpty(model.UnitNo))
			//{
				//predicate = predicate.And(p => p.UnitNo == model.UnitNo);
			//}
			//if (model.Lng > 0)
			//{
				//predicate = predicate.And(p => p.Lng == model.Lng);
			//}
			//if (model.Lat > 0)
			//{
				//predicate = predicate.And(p => p.Lat == model.Lat);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);
			//if (!String.IsNullOrEmpty(model.TitleAr))
			//{
				//predicate = predicate.And(p => p.TitleAr == model.TitleAr);
			//}
			//if (!String.IsNullOrEmpty(model.TitleEn))
			//{
				//predicate = predicate.And(p => p.TitleEn == model.TitleEn);
			//}

			IQueryable<UsersAddresses> query = _UsersAddressesRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "AddressId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "AddressId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AddressId).Where(predicate);
			else if (model.OrderBy == "AddressId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AddressId).Where(predicate);
			else if (model.OrderBy == "UserId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "UserId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "IsMain" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.IsMain).Where(predicate);
			else if (model.OrderBy == "IsMain" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.IsMain).Where(predicate);
			else if (model.OrderBy == "LKCountryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCountryId).Where(predicate);
			else if (model.OrderBy == "LKCountryId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCountryId).Where(predicate);
			else if (model.OrderBy == "LKRegionId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKRegionId).Where(predicate);
			else if (model.OrderBy == "LKRegionId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKRegionId).Where(predicate);
			else if (model.OrderBy == "LKCityId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCityId).Where(predicate);
			else if (model.OrderBy == "LKCityId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCityId).Where(predicate);
			else if (model.OrderBy == "LKDistrictId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKDistrictId).Where(predicate);
			else if (model.OrderBy == "LKDistrictId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKDistrictId).Where(predicate);
			else if (model.OrderBy == "FullAddressAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.FullAddressAr).Where(predicate);
			else if (model.OrderBy == "FullAddressAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.FullAddressAr).Where(predicate);
			else if (model.OrderBy == "FullAddressEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.FullAddressEn).Where(predicate);
			else if (model.OrderBy == "FullAddressEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.FullAddressEn).Where(predicate);
			else if (model.OrderBy == "StreetAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.StreetAr).Where(predicate);
			else if (model.OrderBy == "StreetAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.StreetAr).Where(predicate);
			else if (model.OrderBy == "StreetEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.StreetEn).Where(predicate);
			else if (model.OrderBy == "StreetEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.StreetEn).Where(predicate);
			else if (model.OrderBy == "PostalCode" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.PostalCode).Where(predicate);
			else if (model.OrderBy == "PostalCode" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.PostalCode).Where(predicate);
			else if (model.OrderBy == "AdditionalCode" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AdditionalCode).Where(predicate);
			else if (model.OrderBy == "AdditionalCode" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AdditionalCode).Where(predicate);
			else if (model.OrderBy == "BuildingNo" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.BuildingNo).Where(predicate);
			else if (model.OrderBy == "BuildingNo" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.BuildingNo).Where(predicate);
			else if (model.OrderBy == "UnitNo" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UnitNo).Where(predicate);
			else if (model.OrderBy == "UnitNo" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UnitNo).Where(predicate);
			else if (model.OrderBy == "Lng" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Lng).Where(predicate);
			else if (model.OrderBy == "Lng" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Lng).Where(predicate);
			else if (model.OrderBy == "Lat" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Lat).Where(predicate);
			else if (model.OrderBy == "Lat" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Lat).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "TitleAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TitleAr).Where(predicate);
			else if (model.OrderBy == "TitleAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TitleAr).Where(predicate);
			else if (model.OrderBy == "TitleEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TitleEn).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.TitleEn).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (UsersAddresses record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					UsersAddressesVM vm = new UsersAddressesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public UsersAddressesVM GetById(long AddressId)
		{
			UsersAddresses model = _UsersAddressesRepo.GetById(AddressId);
			UsersAddressesVM vm = new UsersAddressesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(UsersAddressesVM src, UsersAddresses dest)
		{
			dest.AddressId = src.AddressId;
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			dest.IsMain = src.IsMain;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
			dest.LKRegionId = src.LKRegionId;
			dest.LKCityId = src.LKCityId;
			dest.LKDistrictId = src.LKDistrictId;
			if (!String.IsNullOrEmpty(src.FullAddressAr))
				dest.FullAddressAr = src.FullAddressAr;
			if (!String.IsNullOrEmpty(src.FullAddressEn))
				dest.FullAddressEn = src.FullAddressEn;
			if (!String.IsNullOrEmpty(src.StreetAr))
				dest.StreetAr = src.StreetAr;
			if (!String.IsNullOrEmpty(src.StreetEn))
				dest.StreetEn = src.StreetEn;
			if (!String.IsNullOrEmpty(src.PostalCode))
				dest.PostalCode = src.PostalCode;
			if (!String.IsNullOrEmpty(src.AdditionalCode))
				dest.AdditionalCode = src.AdditionalCode;
			if (!String.IsNullOrEmpty(src.BuildingNo))
				dest.BuildingNo = src.BuildingNo;
			if (!String.IsNullOrEmpty(src.UnitNo))
				dest.UnitNo = src.UnitNo;
			if (src.Lng > 0)
				dest.Lng = src.Lng;
			if (src.Lat > 0)
				dest.Lat = src.Lat;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (!String.IsNullOrEmpty(src.TitleAr))
				dest.TitleAr = src.TitleAr;
			if (!String.IsNullOrEmpty(src.TitleEn))
				dest.TitleEn = src.TitleEn;
		}

		private void copyToVM(UsersAddresses src, UsersAddressesVM dest)
		{
			dest.AddressId = src.AddressId;
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			dest.IsMain = src.IsMain;
			if (src.LKCountryId > 0)
				dest.LKCountryId = src.LKCountryId;
			dest.LKRegionId = src.LKRegionId;
			dest.LKCityId = src.LKCityId;
			dest.LKDistrictId = src.LKDistrictId;
			if (!String.IsNullOrEmpty(src.FullAddressAr))
				dest.FullAddressAr = src.FullAddressAr;
			if (!String.IsNullOrEmpty(src.FullAddressEn))
				dest.FullAddressEn = src.FullAddressEn;
			if (!String.IsNullOrEmpty(src.StreetAr))
				dest.StreetAr = src.StreetAr;
			if (!String.IsNullOrEmpty(src.StreetEn))
				dest.StreetEn = src.StreetEn;
			if (!String.IsNullOrEmpty(src.PostalCode))
				dest.PostalCode = src.PostalCode;
			if (!String.IsNullOrEmpty(src.AdditionalCode))
				dest.AdditionalCode = src.AdditionalCode;
			if (!String.IsNullOrEmpty(src.BuildingNo))
				dest.BuildingNo = src.BuildingNo;
			if (!String.IsNullOrEmpty(src.UnitNo))
				dest.UnitNo = src.UnitNo;
			if (src.Lng > 0)
				dest.Lng = src.Lng;
			if (src.Lat > 0)
				dest.Lat = src.Lat;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (!String.IsNullOrEmpty(src.TitleAr))
				dest.TitleAr = src.TitleAr;
			if (!String.IsNullOrEmpty(src.TitleEn))
				dest.TitleEn = src.TitleEn;
		}

	}
}
