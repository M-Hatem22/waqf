using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EgyVisionService.EgyVision
{
	public interface IToaaBasicDataService
	{
		List<ToaaBasicDataVM> Search(ToaaBasicDataVM model);
		bool Insert(ToaaBasicDataVM vm);
		bool Update(ToaaBasicDataVM vm);
		bool Delete(ToaaBasicDataVM vm);
		ToaaBasicDataVM GetById(int id);
	}

	public class ToaaBasicDataService : IToaaBasicDataService
	{
		private IEgyVisionRepository<ToaaBasicData> _ToaaBasicDataRepo = null;

		public ToaaBasicDataService()
		{
			_ToaaBasicDataRepo = new EgyVisionRepository<ToaaBasicData>();

        }
		

		public bool Insert(ToaaBasicDataVM vm)
		{
			ToaaBasicData model = new ToaaBasicData();
			copyToModel(vm,model);
			bool success = _ToaaBasicDataRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(ToaaBasicDataVM vm)
		{
			ToaaBasicData model = _ToaaBasicDataRepo.GetById(vm.id);
			copyToModel(vm,model);
			return _ToaaBasicDataRepo.Update(model);
		}

		public bool Delete(ToaaBasicDataVM vm)
		{
			ToaaBasicData model = _ToaaBasicDataRepo.GetById(vm.id);
			return _ToaaBasicDataRepo.Delete(model);
		}

		public List<ToaaBasicDataVM> Search(ToaaBasicDataVM model)
		{
			List<ToaaBasicDataVM> returned = new List<ToaaBasicDataVM>();
			var predicate = PredicateBuilder.New<ToaaBasicData>(true);

			//if (model.id > 0)
			//{
				//predicate = predicate.And(p => p.id == model.id);
			//}
			//if (!String.IsNullOrEmpty(model.description))
			//{
				//predicate = predicate.And(p => p.description == model.description);
			//}
			//if (!String.IsNullOrEmpty(model.data))
			//{
				//predicate = predicate.And(p => p.data == model.data);
			//}
				//predicate = predicate.And(p => p.lastChange == model.lastChange);

			IQueryable<ToaaBasicData> query = _ToaaBasicDataRepo.Table.AsExpandable().Where(predicate);
			IQueryable<ToaaBasicData> queryCount = _ToaaBasicDataRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "id";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "id" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.id).Where(predicate);
			else if (model.OrderBy == "id" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.id).Where(predicate);
			else if (model.OrderBy == "description" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.description).Where(predicate);
			else if (model.OrderBy == "description" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.description).Where(predicate);
			else if (model.OrderBy == "data" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.data).Where(predicate);
			else if (model.OrderBy == "data" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.data).Where(predicate);
			else if (model.OrderBy == "lastChange" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.lastChange).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.lastChange).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (ToaaBasicData record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					ToaaBasicDataVM vm = new ToaaBasicDataVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public ToaaBasicDataVM GetById(int id)
		{
			ToaaBasicData model = _ToaaBasicDataRepo.GetById(id);
			ToaaBasicDataVM vm = new ToaaBasicDataVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(ToaaBasicDataVM src, ToaaBasicData dest)
		{
			if (src.id > 0)
				dest.id = src.id;
			if (!String.IsNullOrEmpty(src.description))
				dest.description = src.description;
			if (!String.IsNullOrEmpty(src.data))
				dest.data = src.data;
			if (src.lastChange != null && src.lastChange != DateTime.MinValue)
				dest.lastChange = src.lastChange;
		}

		private void copyToVM(ToaaBasicData src, ToaaBasicDataVM dest)
		{
			if (src.id > 0)
				dest.id = src.id;
			if (!String.IsNullOrEmpty(src.description))
				dest.description = src.description;
			if (!String.IsNullOrEmpty(src.data))
				dest.data = src.data;
			if (src.lastChange != null && src.lastChange != DateTime.MinValue)
				dest.lastChange = src.lastChange;
		}

	}
}
