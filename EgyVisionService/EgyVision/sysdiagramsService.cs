using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IsysdiagramsService
	{
		List<sysdiagramsVM> Search(sysdiagramsVM model);
		bool Insert(sysdiagramsVM vm);
		bool Update(sysdiagramsVM vm);
		bool Delete(sysdiagramsVM vm);
		sysdiagramsVM GetById(int diagram_id);
	}

	public class sysdiagramsService : IsysdiagramsService
	{
		private IEgyVisionRepository<sysdiagrams> _sysdiagramsRepo = null;
		public sysdiagramsService()
		{
			_sysdiagramsRepo = new EgyVisionRepository<sysdiagrams>();
		}

		public bool Insert(sysdiagramsVM vm)
		{
			sysdiagrams model = new sysdiagrams();
			copyToModel(vm,model);
			bool success = _sysdiagramsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(sysdiagramsVM vm)
		{
			sysdiagrams model = _sysdiagramsRepo.GetById(vm.diagram_id);
			copyToModel(vm,model);
			return _sysdiagramsRepo.Update(model);
		}

		public bool Delete(sysdiagramsVM vm)
		{
			sysdiagrams model = _sysdiagramsRepo.GetById(vm.diagram_id);
			return _sysdiagramsRepo.Delete(model);
		}

		public List<sysdiagramsVM> Search(sysdiagramsVM model)
		{
			List<sysdiagramsVM> returned = new List<sysdiagramsVM>();
			var predicate = PredicateBuilder.New<sysdiagrams>(true);

				//predicate = predicate.And(p => p.name == model.name);
			//if (model.principal_id > 0)
			//{
				//predicate = predicate.And(p => p.principal_id == model.principal_id);
			//}
			//if (model.diagram_id > 0)
			//{
				//predicate = predicate.And(p => p.diagram_id == model.diagram_id);
			//}
			//if (model.version > 0)
			//{
				//predicate = predicate.And(p => p.version == model.version);
			//}
				//predicate = predicate.And(p => p.definition == model.definition);

			IQueryable<sysdiagrams> query = _sysdiagramsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "name";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "name" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.name).Where(predicate);
			else if (model.OrderBy == "name" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.name).Where(predicate);
			else if (model.OrderBy == "principal_id" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.principal_id).Where(predicate);
			else if (model.OrderBy == "principal_id" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.principal_id).Where(predicate);
			else if (model.OrderBy == "diagram_id" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.diagram_id).Where(predicate);
			else if (model.OrderBy == "diagram_id" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.diagram_id).Where(predicate);
			else if (model.OrderBy == "version" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.version).Where(predicate);
			else if (model.OrderBy == "version" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.version).Where(predicate);
			else if (model.OrderBy == "definition" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.definition).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.definition).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (sysdiagrams record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					sysdiagramsVM vm = new sysdiagramsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public sysdiagramsVM GetById(int diagram_id)
		{
			sysdiagrams model = _sysdiagramsRepo.GetById(diagram_id);
			sysdiagramsVM vm = new sysdiagramsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(sysdiagramsVM src, sysdiagrams dest)
		{
			dest.name = src.name;
			if (src.principal_id > 0)
				dest.principal_id = src.principal_id;
			if (src.diagram_id > 0)
				dest.diagram_id = src.diagram_id;
			if (src.version > 0)
				dest.version = src.version;
			if (src.definition != null && src.definition.Length > 0)
				dest.definition = src.definition;
		}

		private void copyToVM(sysdiagrams src, sysdiagramsVM dest)
		{
			dest.name = src.name;
			if (src.principal_id > 0)
				dest.principal_id = src.principal_id;
			if (src.diagram_id > 0)
				dest.diagram_id = src.diagram_id;
			if (src.version > 0)
				dest.version = src.version;
			if (src.definition != null && src.definition.Length > 0)
				dest.definition = src.definition;
		}

	}
}
