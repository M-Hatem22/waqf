using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKAttachmentTypesService
	{
		List<LKAttachmentTypesVM> Search(LKAttachmentTypesVM model);
		bool Insert(LKAttachmentTypesVM vm);
		bool Update(LKAttachmentTypesVM vm);
		bool Delete(LKAttachmentTypesVM vm);
		LKAttachmentTypesVM GetById(int LKAttachmentTypeId);
	}

	public class LKAttachmentTypesService : ILKAttachmentTypesService
	{
		private IEgyVisionRepository<LKAttachmentTypes> _LKAttachmentTypesRepo = null;
		public LKAttachmentTypesService()
		{
			_LKAttachmentTypesRepo = new EgyVisionRepository<LKAttachmentTypes>();
		}

		public bool Insert(LKAttachmentTypesVM vm)
		{
			LKAttachmentTypes model = new LKAttachmentTypes();
			copyToModel(vm,model);
			bool success = _LKAttachmentTypesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKAttachmentTypesVM vm)
		{
			LKAttachmentTypes model = _LKAttachmentTypesRepo.GetById(vm.LKAttachmentTypeId);
			copyToModel(vm,model);
			return _LKAttachmentTypesRepo.Update(model);
		}

		public bool Delete(LKAttachmentTypesVM vm)
		{
			LKAttachmentTypes model = _LKAttachmentTypesRepo.GetById(vm.LKAttachmentTypeId);
			return _LKAttachmentTypesRepo.Delete(model);
		}

		public List<LKAttachmentTypesVM> Search(LKAttachmentTypesVM model)
		{
			List<LKAttachmentTypesVM> returned = new List<LKAttachmentTypesVM>();
			var predicate = PredicateBuilder.New<LKAttachmentTypes>(true);

			//if (model.LKAttachmentTypeId > 0)
			//{
				//predicate = predicate.And(p => p.LKAttachmentTypeId == model.LKAttachmentTypeId);
			//}
			//if (!String.IsNullOrEmpty(model.LKAttachmentTypeNameAr))
			//{
				//predicate = predicate.And(p => p.LKAttachmentTypeNameAr == model.LKAttachmentTypeNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.LKAttachmentTypeNameEn))
			//{
				//predicate = predicate.And(p => p.LKAttachmentTypeNameEn == model.LKAttachmentTypeNameEn);
			//}
			//if (model.LKAttachmentKeyTypeId > 0)
			//{
				//predicate = predicate.And(p => p.LKAttachmentKeyTypeId == model.LKAttachmentKeyTypeId);
			//}

			IQueryable<LKAttachmentTypes> query = _LKAttachmentTypesRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKAttachmentTypeId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKAttachmentTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAttachmentTypeId).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAttachmentTypeId).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAttachmentTypeNameAr).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAttachmentTypeNameAr).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAttachmentTypeNameEn).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAttachmentTypeNameEn).Where(predicate);
			else if (model.OrderBy == "LKAttachmentKeyTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAttachmentKeyTypeId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.LKAttachmentKeyTypeId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKAttachmentTypes record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKAttachmentTypesVM vm = new LKAttachmentTypesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKAttachmentTypesVM GetById(int LKAttachmentTypeId)
		{
			LKAttachmentTypes model = _LKAttachmentTypesRepo.GetById(LKAttachmentTypeId);
			LKAttachmentTypesVM vm = new LKAttachmentTypesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKAttachmentTypesVM src, LKAttachmentTypes dest)
		{
			if (src.LKAttachmentTypeId > 0)
				dest.LKAttachmentTypeId = src.LKAttachmentTypeId;
			if (!String.IsNullOrEmpty(src.LKAttachmentTypeNameAr))
				dest.LKAttachmentTypeNameAr = src.LKAttachmentTypeNameAr;
			if (!String.IsNullOrEmpty(src.LKAttachmentTypeNameEn))
				dest.LKAttachmentTypeNameEn = src.LKAttachmentTypeNameEn;
			if (src.LKAttachmentKeyTypeId > 0)
				dest.LKAttachmentKeyTypeId = src.LKAttachmentKeyTypeId;
		}

		private void copyToVM(LKAttachmentTypes src, LKAttachmentTypesVM dest)
		{
			if (src.LKAttachmentTypeId > 0)
				dest.LKAttachmentTypeId = src.LKAttachmentTypeId;
			if (!String.IsNullOrEmpty(src.LKAttachmentTypeNameAr))
				dest.LKAttachmentTypeNameAr = src.LKAttachmentTypeNameAr;
			if (!String.IsNullOrEmpty(src.LKAttachmentTypeNameEn))
				dest.LKAttachmentTypeNameEn = src.LKAttachmentTypeNameEn;
			if (src.LKAttachmentKeyTypeId > 0)
				dest.LKAttachmentKeyTypeId = src.LKAttachmentKeyTypeId;
		}

	}
}
