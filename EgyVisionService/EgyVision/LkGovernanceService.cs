using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILkGovernanceService
	{
		List<LkGovernanceVM> Search(LkGovernanceVM model);
		bool Insert(LkGovernanceVM vm);
		bool Update(LkGovernanceVM vm);
		bool Delete(LkGovernanceVM vm);
		LkGovernanceVM GetById(int LkGovernanceId);
	}

	public class LkGovernanceService : ILkGovernanceService
	{
		private IEgyVisionRepository<LkGovernance> _LkGovernanceRepo = null;
		public LkGovernanceService()
		{
			_LkGovernanceRepo = new EgyVisionRepository<LkGovernance>();
		}

		public bool Insert(LkGovernanceVM vm)
		{
			LkGovernance model = new LkGovernance();
			copyToModel(vm,model);
			bool success = _LkGovernanceRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LkGovernanceVM vm)
		{
			LkGovernance model = _LkGovernanceRepo.GetById(vm.LkGovernanceId);
			copyToModel(vm,model);
			return _LkGovernanceRepo.Update(model);
		}

		public bool Delete(LkGovernanceVM vm)
		{
			LkGovernance model = _LkGovernanceRepo.GetById(vm.LkGovernanceId);
			return _LkGovernanceRepo.Delete(model);
		}

		public List<LkGovernanceVM> Search(LkGovernanceVM model)
		{
			List<LkGovernanceVM> returned = new List<LkGovernanceVM>();
			var predicate = PredicateBuilder.New<LkGovernance>(true);

            if (model.LkGovernanceId > 0)
            {
                predicate = predicate.And(p => p.LkGovernanceId == model.LkGovernanceId);
            }
            //if (!String.IsNullOrEmpty(model.LkGovernanceNameAr))
            //{
            //predicate = predicate.And(p => p.LkGovernanceNameAr == model.LkGovernanceNameAr);
            //}
            //if (!String.IsNullOrEmpty(model.LkGovernanceNameEn))
            //{
            //predicate = predicate.And(p => p.LkGovernanceNameEn == model.LkGovernanceNameEn);
            //}

            IQueryable<LkGovernance> query = _LkGovernanceRepo.Table.AsExpandable().Where(predicate);
			IQueryable<LkGovernance> queryCount = _LkGovernanceRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LkGovernanceId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LkGovernanceId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LkGovernanceId).Where(predicate);
			else if (model.OrderBy == "LkGovernanceId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LkGovernanceId).Where(predicate);
			else if (model.OrderBy == "LkGovernanceNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LkGovernanceNameAr).Where(predicate);
			else if (model.OrderBy == "LkGovernanceNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LkGovernanceNameAr).Where(predicate);
			else if (model.OrderBy == "LkGovernanceNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LkGovernanceNameEn).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.LkGovernanceNameEn).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LkGovernance record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LkGovernanceVM vm = new LkGovernanceVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LkGovernanceVM GetById(int LkGovernanceId)
		{
			LkGovernance model = _LkGovernanceRepo.GetById(LkGovernanceId);
			LkGovernanceVM vm = new LkGovernanceVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LkGovernanceVM src, LkGovernance dest)
		{
			if (src.LkGovernanceId > 0)
				dest.LkGovernanceId = src.LkGovernanceId;
			if (!String.IsNullOrEmpty(src.LkGovernanceNameAr))
				dest.LkGovernanceNameAr = src.LkGovernanceNameAr;
			if (!String.IsNullOrEmpty(src.LkGovernanceNameEn))
				dest.LkGovernanceNameEn = src.LkGovernanceNameEn;
		}

		private void copyToVM(LkGovernance src, LkGovernanceVM dest)
		{
			if (src.LkGovernanceId > 0)
				dest.LkGovernanceId = src.LkGovernanceId;
			if (!String.IsNullOrEmpty(src.LkGovernanceNameAr))
				dest.LkGovernanceNameAr = src.LkGovernanceNameAr;
			if (!String.IsNullOrEmpty(src.LkGovernanceNameEn))
				dest.LkGovernanceNameEn = src.LkGovernanceNameEn;
		}

	}
}
