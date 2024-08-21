using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyVisionService.EgyVision
{
    public interface IAddressesService
	{
		List<AddressesVM> Search(AddressesVM model);
		bool Insert(AddressesVM vm);
		bool Update(AddressesVM vm);
		bool Delete(AddressesVM vm);
		AddressesVM GetById(int AddressId);
	}

	public class AddressesService : IAddressesService
	{
		private IEgyVisionRepository<Addresses> _AddressesRepo = null;
		public AddressesService()
		{
			_AddressesRepo = new EgyVisionRepository<Addresses>();
		}

		public bool Insert(AddressesVM vm)
		{
			Addresses model = new Addresses();
			copyToModel(vm,model);
			bool success = _AddressesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AddressesVM vm)
		{
			Addresses model = _AddressesRepo.GetById(vm.AddressId);
			copyToModel(vm,model);
			return _AddressesRepo.Update(model);
		}

		public bool Delete(AddressesVM vm)
		{
			Addresses model = _AddressesRepo.GetById(vm.AddressId);
			return _AddressesRepo.Delete(model);
		}

		public List<AddressesVM> Search(AddressesVM model)
		{
			List<AddressesVM> returned = new List<AddressesVM>();
			var predicate = PredicateBuilder.New<Addresses>(true);

			//if (model.AddressId > 0)
			//{
				//predicate = predicate.And(p => p.AddressId == model.AddressId);
			//}
			//if (!String.IsNullOrEmpty(model.Mobile))
			//{
				//predicate = predicate.And(p => p.Mobile == model.Mobile);
			//}
			//if (!String.IsNullOrEmpty(model.Phone1))
			//{
				//predicate = predicate.And(p => p.Phone1 == model.Phone1);
			//}
			//if (!String.IsNullOrEmpty(model.Phone2))
			//{
				//predicate = predicate.And(p => p.Phone2 == model.Phone2);
			//}
			//if (!String.IsNullOrEmpty(model.Fax))
			//{
				//predicate = predicate.And(p => p.Fax == model.Fax);
			//}
			//if (!String.IsNullOrEmpty(model.Email1))
			//{
				//predicate = predicate.And(p => p.Email1 == model.Email1);
			//}
			//if (!String.IsNullOrEmpty(model.Email2))
			//{
				//predicate = predicate.And(p => p.Email2 == model.Email2);
			//}
			//if (!String.IsNullOrEmpty(model.Email3))
			//{
				//predicate = predicate.And(p => p.Email3 == model.Email3);
			//}
			//if (!String.IsNullOrEmpty(model.AddressAr))
			//{
				//predicate = predicate.And(p => p.AddressAr == model.AddressAr);
			//}
			//if (!String.IsNullOrEmpty(model.AddressEn))
			//{
				//predicate = predicate.And(p => p.AddressEn == model.AddressEn);
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

			IQueryable<Addresses> query = _AddressesRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "Mobile" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Mobile).Where(predicate);
			else if (model.OrderBy == "Mobile" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Mobile).Where(predicate);
			else if (model.OrderBy == "Phone1" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Phone1).Where(predicate);
			else if (model.OrderBy == "Phone1" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Phone1).Where(predicate);
			else if (model.OrderBy == "Phone2" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Phone2).Where(predicate);
			else if (model.OrderBy == "Phone2" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Phone2).Where(predicate);
			else if (model.OrderBy == "Fax" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Fax).Where(predicate);
			else if (model.OrderBy == "Fax" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Fax).Where(predicate);
			else if (model.OrderBy == "Email1" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Email1).Where(predicate);
			else if (model.OrderBy == "Email1" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Email1).Where(predicate);
			else if (model.OrderBy == "Email2" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Email2).Where(predicate);
			else if (model.OrderBy == "Email2" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Email2).Where(predicate);
			else if (model.OrderBy == "Email3" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Email3).Where(predicate);
			else if (model.OrderBy == "Email3" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Email3).Where(predicate);
			else if (model.OrderBy == "AddressAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AddressAr).Where(predicate);
			else if (model.OrderBy == "AddressAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AddressAr).Where(predicate);
			else if (model.OrderBy == "AddressEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AddressEn).Where(predicate);
			else if (model.OrderBy == "AddressEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AddressEn).Where(predicate);
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
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (Addresses record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AddressesVM vm = new AddressesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AddressesVM GetById(int AddressId)
		{
			Addresses model = _AddressesRepo.GetById(AddressId);
			AddressesVM vm = new AddressesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AddressesVM src, Addresses dest)
		{
			if (src.AddressId > 0)
				dest.AddressId = src.AddressId;
			if (!String.IsNullOrEmpty(src.Mobile))
				dest.Mobile = src.Mobile;
			if (!String.IsNullOrEmpty(src.Phone1))
				dest.Phone1 = src.Phone1;
			if (!String.IsNullOrEmpty(src.Phone2))
				dest.Phone2 = src.Phone2;
			if (!String.IsNullOrEmpty(src.Fax))
				dest.Fax = src.Fax;
			if (!String.IsNullOrEmpty(src.Email1))
				dest.Email1 = src.Email1;
			if (!String.IsNullOrEmpty(src.Email2))
				dest.Email2 = src.Email2;
			if (!String.IsNullOrEmpty(src.Email3))
				dest.Email3 = src.Email3;
			if (!String.IsNullOrEmpty(src.AddressAr))
				dest.AddressAr = src.AddressAr;
			if (!String.IsNullOrEmpty(src.AddressEn))
				dest.AddressEn = src.AddressEn;
			if (src.Lng > 0)
				dest.Lng = src.Lng;
			if (src.Lat > 0)
				dest.Lat = src.Lat;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

		private void copyToVM(Addresses src, AddressesVM dest)
		{
			if (src.AddressId > 0)
				dest.AddressId = src.AddressId;
			if (!String.IsNullOrEmpty(src.Mobile))
				dest.Mobile = src.Mobile;
			if (!String.IsNullOrEmpty(src.Phone1))
				dest.Phone1 = src.Phone1;
			if (!String.IsNullOrEmpty(src.Phone2))
				dest.Phone2 = src.Phone2;
			if (!String.IsNullOrEmpty(src.Fax))
				dest.Fax = src.Fax;
			if (!String.IsNullOrEmpty(src.Email1))
				dest.Email1 = src.Email1;
			if (!String.IsNullOrEmpty(src.Email2))
				dest.Email2 = src.Email2;
			if (!String.IsNullOrEmpty(src.Email3))
				dest.Email3 = src.Email3;
			if (!String.IsNullOrEmpty(src.AddressAr))
				dest.AddressAr = src.AddressAr;
			if (!String.IsNullOrEmpty(src.AddressEn))
				dest.AddressEn = src.AddressEn;
			if (src.Lng > 0)
				dest.Lng = src.Lng;
			if (src.Lat > 0)
				dest.Lat = src.Lat;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

	}
}
