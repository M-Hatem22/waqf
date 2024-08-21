using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ISQLSessionsService
	{
		List<SQLSessionsVM> Search(SQLSessionsVM model);
		bool Insert(SQLSessionsVM vm);
		bool Update(SQLSessionsVM vm);
		bool Delete(SQLSessionsVM vm);
		SQLSessionsVM GetById(string Id);
	}

	public class SQLSessionsService : ISQLSessionsService
	{
		private IEgyVisionRepository<SQLSessions> _SQLSessionsRepo = null;
		public SQLSessionsService()
		{
			_SQLSessionsRepo = new EgyVisionRepository<SQLSessions>();
		}

		public bool Insert(SQLSessionsVM vm)
		{
			SQLSessions model = new SQLSessions();
			copyToModel(vm,model);
			bool success = _SQLSessionsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(SQLSessionsVM vm)
		{
			SQLSessions model = _SQLSessionsRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _SQLSessionsRepo.Update(model);
		}

		public bool Delete(SQLSessionsVM vm)
		{
			SQLSessions model = _SQLSessionsRepo.GetById(vm.Id);
			return _SQLSessionsRepo.Delete(model);
		}

		public List<SQLSessionsVM> Search(SQLSessionsVM model)
		{
			List<SQLSessionsVM> returned = new List<SQLSessionsVM>();
			var predicate = PredicateBuilder.New<SQLSessions>(true);

			//if (!String.IsNullOrEmpty(model.Id))
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
				//predicate = predicate.And(p => p.Value == model.Value);
				//predicate = predicate.And(p => p.ExpiresAtTime == model.ExpiresAtTime);
			//if (model.SlidingExpirationInSeconds > 0)
			//{
				//predicate = predicate.And(p => p.SlidingExpirationInSeconds == model.SlidingExpirationInSeconds);
			//}
				//predicate = predicate.And(p => p.AbsoluteExpiration == model.AbsoluteExpiration);

			IQueryable<SQLSessions> query = _SQLSessionsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "Id";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "Id" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Id).Where(predicate);
			else if (model.OrderBy == "Id" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Id).Where(predicate);
			else if (model.OrderBy == "Value" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Value).Where(predicate);
			else if (model.OrderBy == "Value" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Value).Where(predicate);
			else if (model.OrderBy == "ExpiresAtTime" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ExpiresAtTime).Where(predicate);
			else if (model.OrderBy == "ExpiresAtTime" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ExpiresAtTime).Where(predicate);
			else if (model.OrderBy == "SlidingExpirationInSeconds" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.SlidingExpirationInSeconds).Where(predicate);
			else if (model.OrderBy == "SlidingExpirationInSeconds" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.SlidingExpirationInSeconds).Where(predicate);
			else if (model.OrderBy == "AbsoluteExpiration" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AbsoluteExpiration).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.AbsoluteExpiration).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (SQLSessions record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					SQLSessionsVM vm = new SQLSessionsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public SQLSessionsVM GetById(string Id)
		{
			SQLSessions model = _SQLSessionsRepo.GetById(Id);
			SQLSessionsVM vm = new SQLSessionsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(SQLSessionsVM src, SQLSessions dest)
		{
			if (!String.IsNullOrEmpty(src.Id))
				dest.Id = src.Id;
			if (src.Value != null && src.Value.Length > 0)
				dest.Value = src.Value;
			dest.ExpiresAtTime = src.ExpiresAtTime;
			dest.SlidingExpirationInSeconds = src.SlidingExpirationInSeconds;
			dest.AbsoluteExpiration = src.AbsoluteExpiration;
		}

		private void copyToVM(SQLSessions src, SQLSessionsVM dest)
		{
			if (!String.IsNullOrEmpty(src.Id))
				dest.Id = src.Id;
			if (src.Value != null && src.Value.Length > 0)
				dest.Value = src.Value;
			dest.ExpiresAtTime = src.ExpiresAtTime;
			dest.SlidingExpirationInSeconds = src.SlidingExpirationInSeconds;
			dest.AbsoluteExpiration = src.AbsoluteExpiration;
		}

	}
}
