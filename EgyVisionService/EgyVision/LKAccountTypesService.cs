using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKAccountTypesService
	{
		List<LKAccountTypesVM> Search(LKAccountTypesVM model);
		bool Insert(LKAccountTypesVM vm);
		bool Update(LKAccountTypesVM vm);
		bool Delete(LKAccountTypesVM vm);
		LKAccountTypesVM GetById(int LKAccountTypeId);
	}

	public class LKAccountTypesService : ILKAccountTypesService
	{
		private IEgyVisionRepository<LKAccountTypes> _LKAccountTypesRepo = null;
		public LKAccountTypesService()
		{
			_LKAccountTypesRepo = new EgyVisionRepository<LKAccountTypes>();
		}

		public bool Insert(LKAccountTypesVM vm)
		{
			LKAccountTypes model = new LKAccountTypes();
			copyToModel(vm,model);
			bool success = _LKAccountTypesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKAccountTypesVM vm)
		{
			LKAccountTypes model = _LKAccountTypesRepo.GetById(vm.LKAccountTypeId);
			copyToModel(vm,model);
			return _LKAccountTypesRepo.Update(model);
		}

		public bool Delete(LKAccountTypesVM vm)
		{
			LKAccountTypes model = _LKAccountTypesRepo.GetById(vm.LKAccountTypeId);
			return _LKAccountTypesRepo.Delete(model);
		}

		public List<LKAccountTypesVM> Search(LKAccountTypesVM model)
		{
			List<LKAccountTypesVM> returned = new List<LKAccountTypesVM>();
			var predicate = PredicateBuilder.New<LKAccountTypes>(true);

			//if (model.LKAccountTypeId > 0)
			//{
				//predicate = predicate.And(p => p.LKAccountTypeId == model.LKAccountTypeId);
			//}
			//if (!String.IsNullOrEmpty(model.LKAccountTypeNameAr))
			//{
				//predicate = predicate.And(p => p.LKAccountTypeNameAr == model.LKAccountTypeNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.LKAccountTypeNameEn))
			//{
				//predicate = predicate.And(p => p.LKAccountTypeNameEn == model.LKAccountTypeNameEn);
			//}
			//if (model.ParentId > 0)
			//{
				//predicate = predicate.And(p => p.ParentId == model.ParentId);
			//}
			//if (!String.IsNullOrEmpty(model.PrinterName))
			//{
				//predicate = predicate.And(p => p.PrinterName == model.PrinterName);
			//}
			//if (model.PartnerId > 0)
			//{
				//predicate = predicate.And(p => p.PartnerId == model.PartnerId);
			//}

			IQueryable<LKAccountTypes> query = _LKAccountTypesRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKAccountTypeId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKAccountTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAccountTypeId).Where(predicate);
			else if (model.OrderBy == "LKAccountTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAccountTypeId).Where(predicate);
			else if (model.OrderBy == "LKAccountTypeNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAccountTypeNameAr).Where(predicate);
			else if (model.OrderBy == "LKAccountTypeNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAccountTypeNameAr).Where(predicate);
			else if (model.OrderBy == "LKAccountTypeNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAccountTypeNameEn).Where(predicate);
			else if (model.OrderBy == "LKAccountTypeNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAccountTypeNameEn).Where(predicate);
			else if (model.OrderBy == "ParentId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ParentId).Where(predicate);
			else if (model.OrderBy == "ParentId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ParentId).Where(predicate);
			else if (model.OrderBy == "PrinterName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.PrinterName).Where(predicate);
			else if (model.OrderBy == "PrinterName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.PrinterName).Where(predicate);
			else if (model.OrderBy == "PartnerId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.PartnerId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.PartnerId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKAccountTypes record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKAccountTypesVM vm = new LKAccountTypesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKAccountTypesVM GetById(int LKAccountTypeId)
		{
			LKAccountTypes model = _LKAccountTypesRepo.GetById(LKAccountTypeId);
			LKAccountTypesVM vm = new LKAccountTypesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKAccountTypesVM src, LKAccountTypes dest)
		{
			if (src.LKAccountTypeId > 0)
				dest.LKAccountTypeId = src.LKAccountTypeId;
			if (!String.IsNullOrEmpty(src.LKAccountTypeNameAr))
				dest.LKAccountTypeNameAr = src.LKAccountTypeNameAr;
			if (!String.IsNullOrEmpty(src.LKAccountTypeNameEn))
				dest.LKAccountTypeNameEn = src.LKAccountTypeNameEn;
			if (src.ParentId > 0)
				dest.ParentId = src.ParentId;
			if (!String.IsNullOrEmpty(src.PrinterName))
				dest.PrinterName = src.PrinterName;
			dest.PartnerId = src.PartnerId;
		}

		private void copyToVM(LKAccountTypes src, LKAccountTypesVM dest)
		{
			if (src.LKAccountTypeId > 0)
				dest.LKAccountTypeId = src.LKAccountTypeId;
			if (!String.IsNullOrEmpty(src.LKAccountTypeNameAr))
				dest.LKAccountTypeNameAr = src.LKAccountTypeNameAr;
			if (!String.IsNullOrEmpty(src.LKAccountTypeNameEn))
				dest.LKAccountTypeNameEn = src.LKAccountTypeNameEn;
			if (src.ParentId > 0)
				dest.ParentId = src.ParentId;
			if (!String.IsNullOrEmpty(src.PrinterName))
				dest.PrinterName = src.PrinterName;
			dest.PartnerId = src.PartnerId;
		}

	}
}
