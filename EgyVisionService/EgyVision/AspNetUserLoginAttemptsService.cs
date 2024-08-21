using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IAspNetUserLoginAttemptsService
	{
		List<AspNetUserLoginAttemptsVM> Search(AspNetUserLoginAttemptsVM model);
		bool Insert(AspNetUserLoginAttemptsVM vm);
		bool Update(AspNetUserLoginAttemptsVM vm);
		bool Delete(AspNetUserLoginAttemptsVM vm);
		AspNetUserLoginAttemptsVM GetById(long Id);
	}

	public class AspNetUserLoginAttemptsService : IAspNetUserLoginAttemptsService
	{
		private IEgyVisionRepository<AspNetUserLoginAttempts> _AspNetUserLoginAttemptsRepo = null;
		public AspNetUserLoginAttemptsService()
		{
			_AspNetUserLoginAttemptsRepo = new EgyVisionRepository<AspNetUserLoginAttempts>();
		}

		public bool Insert(AspNetUserLoginAttemptsVM vm)
		{
			AspNetUserLoginAttempts model = new AspNetUserLoginAttempts();
			copyToModel(vm,model);
			bool success = _AspNetUserLoginAttemptsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AspNetUserLoginAttemptsVM vm)
		{
			AspNetUserLoginAttempts model = _AspNetUserLoginAttemptsRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AspNetUserLoginAttemptsRepo.Update(model);
		}

		public bool Delete(AspNetUserLoginAttemptsVM vm)
		{
			AspNetUserLoginAttempts model = _AspNetUserLoginAttemptsRepo.GetById(vm.Id);
			return _AspNetUserLoginAttemptsRepo.Delete(model);
		}

		public List<AspNetUserLoginAttemptsVM> Search(AspNetUserLoginAttemptsVM model)
		{
			List<AspNetUserLoginAttemptsVM> returned = new List<AspNetUserLoginAttemptsVM>();
			var predicate = PredicateBuilder.New<AspNetUserLoginAttempts>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (!String.IsNullOrEmpty(model.UserId))
			//{
				//predicate = predicate.And(p => p.UserId == model.UserId);
			//}
			//if (model.AttemptsCount > 0)
			//{
				//predicate = predicate.And(p => p.AttemptsCount == model.AttemptsCount);
			//}
				//predicate = predicate.And(p => p.LastAttempt == model.LastAttempt);

			IQueryable<AspNetUserLoginAttempts> query = _AspNetUserLoginAttemptsRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "UserId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "UserId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "AttemptsCount" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttemptsCount).Where(predicate);
			else if (model.OrderBy == "AttemptsCount" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttemptsCount).Where(predicate);
			else if (model.OrderBy == "LastAttempt" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LastAttempt).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.LastAttempt).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (AspNetUserLoginAttempts record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AspNetUserLoginAttemptsVM vm = new AspNetUserLoginAttemptsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AspNetUserLoginAttemptsVM GetById(long Id)
		{
			AspNetUserLoginAttempts model = _AspNetUserLoginAttemptsRepo.GetById(Id);
			AspNetUserLoginAttemptsVM vm = new AspNetUserLoginAttemptsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AspNetUserLoginAttemptsVM src, AspNetUserLoginAttempts dest)
		{
			dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			dest.AttemptsCount = src.AttemptsCount;
			if (src.LastAttempt != null && src.LastAttempt != DateTime.MinValue)
				dest.LastAttempt = src.LastAttempt;
		}

		private void copyToVM(AspNetUserLoginAttempts src, AspNetUserLoginAttemptsVM dest)
		{
			dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			dest.AttemptsCount = src.AttemptsCount;
			if (src.LastAttempt != null && src.LastAttempt != DateTime.MinValue)
				dest.LastAttempt = src.LastAttempt;
		}

	}
}
