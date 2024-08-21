using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IDbScriptsService
	{
		List<DbScriptsVM> Search(DbScriptsVM model);
		bool Insert(DbScriptsVM vm);
		bool Update(DbScriptsVM vm);
		bool Delete(DbScriptsVM vm);
		DbScriptsVM GetById(int Id);
	}

	public class DbScriptsService : IDbScriptsService
	{
		private IEgyVisionRepository<DbScripts> _DbScriptsRepo = null;
		public DbScriptsService()
		{
			_DbScriptsRepo = new EgyVisionRepository<DbScripts>();
		}

		public bool Insert(DbScriptsVM vm)
		{
			DbScripts model = new DbScripts();
			copyToModel(vm,model);
			bool success = _DbScriptsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(DbScriptsVM vm)
		{
			DbScripts model = _DbScriptsRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _DbScriptsRepo.Update(model);
		}

		public bool Delete(DbScriptsVM vm)
		{
			DbScripts model = _DbScriptsRepo.GetById(vm.Id);
			return _DbScriptsRepo.Delete(model);
		}

		public List<DbScriptsVM> Search(DbScriptsVM model)
		{
			List<DbScriptsVM> returned = new List<DbScriptsVM>();
			var predicate = PredicateBuilder.New<DbScripts>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
				//predicate = predicate.And(p => p.ExecuteDate == model.ExecuteDate);
			//if (!String.IsNullOrEmpty(model.FileName))
			//{
				//predicate = predicate.And(p => p.FileName == model.FileName);
			//}
			//if (!String.IsNullOrEmpty(model.ScriptContent))
			//{
				//predicate = predicate.And(p => p.ScriptContent == model.ScriptContent);
			//}

			IQueryable<DbScripts> query = _DbScriptsRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "ExecuteDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ExecuteDate).Where(predicate);
			else if (model.OrderBy == "ExecuteDate" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ExecuteDate).Where(predicate);
			else if (model.OrderBy == "FileName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.FileName).Where(predicate);
			else if (model.OrderBy == "FileName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.FileName).Where(predicate);
			else if (model.OrderBy == "ScriptContent" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ScriptContent).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.ScriptContent).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (DbScripts record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					DbScriptsVM vm = new DbScriptsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public DbScriptsVM GetById(int Id)
		{
			DbScripts model = _DbScriptsRepo.GetById(Id);
			DbScriptsVM vm = new DbScriptsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(DbScriptsVM src, DbScripts dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (src.ExecuteDate != null && src.ExecuteDate != DateTime.MinValue)
				dest.ExecuteDate = src.ExecuteDate;
			if (!String.IsNullOrEmpty(src.FileName))
				dest.FileName = src.FileName;
			if (!String.IsNullOrEmpty(src.ScriptContent))
				dest.ScriptContent = src.ScriptContent;
		}

		private void copyToVM(DbScripts src, DbScriptsVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (src.ExecuteDate != null && src.ExecuteDate != DateTime.MinValue)
				dest.ExecuteDate = src.ExecuteDate;
			if (!String.IsNullOrEmpty(src.FileName))
				dest.FileName = src.FileName;
			if (!String.IsNullOrEmpty(src.ScriptContent))
				dest.ScriptContent = src.ScriptContent;
		}

	}
}
