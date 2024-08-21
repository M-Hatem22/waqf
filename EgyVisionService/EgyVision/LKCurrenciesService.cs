using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKCurrenciesService
	{
		List<LKCurrenciesVM> Search(LKCurrenciesVM model);
		bool Insert(LKCurrenciesVM vm);
		bool Update(LKCurrenciesVM vm);
		bool Delete(LKCurrenciesVM vm);
		LKCurrenciesVM GetById(int LKCurrencyId);
	}

	public class LKCurrenciesService : ILKCurrenciesService
	{
		private IEgyVisionRepository<LKCurrencies> _LKCurrenciesRepo = null;
		public LKCurrenciesService()
		{
			_LKCurrenciesRepo = new EgyVisionRepository<LKCurrencies>();
		}

		public bool Insert(LKCurrenciesVM vm)
		{
			LKCurrencies model = new LKCurrencies();
			copyToModel(vm,model);
			bool success = _LKCurrenciesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKCurrenciesVM vm)
		{
			LKCurrencies model = _LKCurrenciesRepo.GetById(vm.LKCurrencyId);
			copyToModel(vm,model);
			return _LKCurrenciesRepo.Update(model);
		}

		public bool Delete(LKCurrenciesVM vm)
		{
			LKCurrencies model = _LKCurrenciesRepo.GetById(vm.LKCurrencyId);
			return _LKCurrenciesRepo.Delete(model);
		}

		public List<LKCurrenciesVM> Search(LKCurrenciesVM model)
		{
			List<LKCurrenciesVM> returned = new List<LKCurrenciesVM>();
			var predicate = PredicateBuilder.New<LKCurrencies>(true);

			//if (model.LKCurrencyId > 0)
			//{
				//predicate = predicate.And(p => p.LKCurrencyId == model.LKCurrencyId);
			//}
			//if (!String.IsNullOrEmpty(model.LKCurrencyNameAr))
			//{
				//predicate = predicate.And(p => p.LKCurrencyNameAr == model.LKCurrencyNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.LKCurrencyNameEn))
			//{
				//predicate = predicate.And(p => p.LKCurrencyNameEn == model.LKCurrencyNameEn);
			//}
			//if (!String.IsNullOrEmpty(model.Symbol))
			//{
				//predicate = predicate.And(p => p.Symbol == model.Symbol);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);

			IQueryable<LKCurrencies> query = _LKCurrenciesRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "LKCurrencyId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "LKCurrencyId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCurrencyId).Where(predicate);
			else if (model.OrderBy == "LKCurrencyId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCurrencyId).Where(predicate);
			else if (model.OrderBy == "LKCurrencyNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCurrencyNameAr).Where(predicate);
			else if (model.OrderBy == "LKCurrencyNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCurrencyNameAr).Where(predicate);
			else if (model.OrderBy == "LKCurrencyNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKCurrencyNameEn).Where(predicate);
			else if (model.OrderBy == "LKCurrencyNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKCurrencyNameEn).Where(predicate);
			else if (model.OrderBy == "Symbol" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Symbol).Where(predicate);
			else if (model.OrderBy == "Symbol" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Symbol).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKCurrencies record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKCurrenciesVM vm = new LKCurrenciesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKCurrenciesVM GetById(int LKCurrencyId)
		{
			LKCurrencies model = _LKCurrenciesRepo.GetById(LKCurrencyId);
			LKCurrenciesVM vm = new LKCurrenciesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKCurrenciesVM src, LKCurrencies dest)
		{
			if (src.LKCurrencyId > 0)
				dest.LKCurrencyId = src.LKCurrencyId;
			if (!String.IsNullOrEmpty(src.LKCurrencyNameAr))
				dest.LKCurrencyNameAr = src.LKCurrencyNameAr;
			if (!String.IsNullOrEmpty(src.LKCurrencyNameEn))
				dest.LKCurrencyNameEn = src.LKCurrencyNameEn;
			if (!String.IsNullOrEmpty(src.Symbol))
				dest.Symbol = src.Symbol;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

		private void copyToVM(LKCurrencies src, LKCurrenciesVM dest)
		{
			if (src.LKCurrencyId > 0)
				dest.LKCurrencyId = src.LKCurrencyId;
			if (!String.IsNullOrEmpty(src.LKCurrencyNameAr))
				dest.LKCurrencyNameAr = src.LKCurrencyNameAr;
			if (!String.IsNullOrEmpty(src.LKCurrencyNameEn))
				dest.LKCurrencyNameEn = src.LKCurrencyNameEn;
			if (!String.IsNullOrEmpty(src.Symbol))
				dest.Symbol = src.Symbol;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

	}
}
