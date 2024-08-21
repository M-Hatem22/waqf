using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKAttachmentKeyTypesService
	{
		List<LKAttachmentKeyTypesVM> Search(LKAttachmentKeyTypesVM model);
		bool Insert(LKAttachmentKeyTypesVM vm);
		bool Update(LKAttachmentKeyTypesVM vm);
		bool Delete(LKAttachmentKeyTypesVM vm);
		LKAttachmentKeyTypesVM GetById(int LKKeyTypeId);
	}

	public class LKAttachmentKeyTypesService : ILKAttachmentKeyTypesService
	{
		private IEgyVisionRepository<LKAttachmentKeyTypes> _LKAttachmentKeyTypesRepo = null;
		public LKAttachmentKeyTypesService()
		{
			_LKAttachmentKeyTypesRepo = new EgyVisionRepository<LKAttachmentKeyTypes>();
		}

		public bool Insert(LKAttachmentKeyTypesVM vm)
		{
			LKAttachmentKeyTypes model = new LKAttachmentKeyTypes();
			copyToModel(vm,model);
			bool success = _LKAttachmentKeyTypesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKAttachmentKeyTypesVM vm)
		{
			LKAttachmentKeyTypes model = _LKAttachmentKeyTypesRepo.GetById(vm.LKKeyTypeId);
			copyToModel(vm,model);
			return _LKAttachmentKeyTypesRepo.Update(model);
		}

		public bool Delete(LKAttachmentKeyTypesVM vm)
		{
			LKAttachmentKeyTypes model = _LKAttachmentKeyTypesRepo.GetById(vm.LKKeyTypeId);
			return _LKAttachmentKeyTypesRepo.Delete(model);
		}

		public List<LKAttachmentKeyTypesVM> Search(LKAttachmentKeyTypesVM model)
		{
			List<LKAttachmentKeyTypesVM> returned = new List<LKAttachmentKeyTypesVM>();
			var predicate = PredicateBuilder.New<LKAttachmentKeyTypes>(true);

			//if (model.LKKeyTypeId > 0)
			//{
				//predicate = predicate.And(p => p.LKKeyTypeId == model.LKKeyTypeId);
			//}
			//if (!String.IsNullOrEmpty(model.LKKeyTypeNameAr))
			//{
				//predicate = predicate.And(p => p.LKKeyTypeNameAr == model.LKKeyTypeNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.LKKeyTypeNameEn))
			//{
				//predicate = predicate.And(p => p.LKKeyTypeNameEn == model.LKKeyTypeNameEn);
			//}

			IQueryable<LKAttachmentKeyTypes> query = _LKAttachmentKeyTypesRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKKeyTypeId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKKeyTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKKeyTypeId).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKKeyTypeId).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKKeyTypeNameAr).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKKeyTypeNameAr).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKKeyTypeNameEn).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.LKKeyTypeNameEn).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKAttachmentKeyTypes record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKAttachmentKeyTypesVM vm = new LKAttachmentKeyTypesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKAttachmentKeyTypesVM GetById(int LKKeyTypeId)
		{
			LKAttachmentKeyTypes model = _LKAttachmentKeyTypesRepo.GetById(LKKeyTypeId);
			LKAttachmentKeyTypesVM vm = new LKAttachmentKeyTypesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKAttachmentKeyTypesVM src, LKAttachmentKeyTypes dest)
		{
			if (src.LKKeyTypeId > 0)
				dest.LKKeyTypeId = src.LKKeyTypeId;
			if (!String.IsNullOrEmpty(src.LKKeyTypeNameAr))
				dest.LKKeyTypeNameAr = src.LKKeyTypeNameAr;
			if (!String.IsNullOrEmpty(src.LKKeyTypeNameEn))
				dest.LKKeyTypeNameEn = src.LKKeyTypeNameEn;
		}

		private void copyToVM(LKAttachmentKeyTypes src, LKAttachmentKeyTypesVM dest)
		{
			if (src.LKKeyTypeId > 0)
				dest.LKKeyTypeId = src.LKKeyTypeId;
			if (!String.IsNullOrEmpty(src.LKKeyTypeNameAr))
				dest.LKKeyTypeNameAr = src.LKKeyTypeNameAr;
			if (!String.IsNullOrEmpty(src.LKKeyTypeNameEn))
				dest.LKKeyTypeNameEn = src.LKKeyTypeNameEn;
		}

	}
}
