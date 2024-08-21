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
	public interface IHomeAboutUsService
	{
		List<HomeAboutUsVM> Search(HomeAboutUsVM model);
		bool Insert(HomeAboutUsVM vm);
		bool Update(HomeAboutUsVM vm);
		bool Delete(HomeAboutUsVM vm);
		HomeAboutUsVM GetById(long HomeAboutUs);
	}

	public class HomeAboutUsService : IHomeAboutUsService
	{
		private IEgyVisionRepository<HomeAboutUs> _HomeAboutUsRepo = null;

		public HomeAboutUsService()
		{
			_HomeAboutUsRepo = new EgyVisionRepository<HomeAboutUs>();

        }


        public bool Insert(HomeAboutUsVM vm)
		{
			HomeAboutUs model = new HomeAboutUs();
			copyToModel(vm,model);
			bool success = _HomeAboutUsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(HomeAboutUsVM vm)
		{
			HomeAboutUs model = _HomeAboutUsRepo.GetById(vm.HomeAboutUsId);
			copyToModel(vm,model);
			return _HomeAboutUsRepo.Update(model);
		}

		public bool Delete(HomeAboutUsVM vm)
		{
			HomeAboutUs model = _HomeAboutUsRepo.GetById(vm.HomeAboutUsId);
			return _HomeAboutUsRepo.Delete(model);
		}

		public List<HomeAboutUsVM> Search(HomeAboutUsVM model)
		{
			List<HomeAboutUsVM> returned = new List<HomeAboutUsVM>();
			var predicate = PredicateBuilder.New<HomeAboutUs>(true);

			if (model.HomeAboutUsId > 0)
			{
				predicate = predicate.And(p => p.HomeAboutUsId == model.HomeAboutUsId);
			}
			//if (!String.IsNullOrEmpty(model.HomeAboutUsAr))
			//{
			//predicate = predicate.And(p => p.HomeAboutUsAr == model.HomeAboutUsAr);
			//}
			//if (!String.IsNullOrEmpty(model.HomeAboutUEn))
			//{
			//predicate = predicate.And(p => p.HomeAboutUEn == model.HomeAboutUEn);
			//}
			//predicate = predicate.And(p => p.TimeInsert == model.TimeInsert);
			predicate = predicate.And(p => p.Deleted == model.Deleted);

			IQueryable<HomeAboutUs> query = _HomeAboutUsRepo.Table.AsExpandable().Where(predicate);
			IQueryable<HomeAboutUs> queryCount = _HomeAboutUsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "HomeAboutUsId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "HomeAboutUsId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.HomeAboutUsId).Where(predicate);
			else if (model.OrderBy == "HomeAboutUsId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.HomeAboutUsId).Where(predicate);
			else if (model.OrderBy == "HomeAboutUsAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.HomeAboutUsAr).Where(predicate);
			else if (model.OrderBy == "HomeAboutUsAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.HomeAboutUsAr).Where(predicate);
			else if (model.OrderBy == "HomeAboutUEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.HomeAboutUEn).Where(predicate);
			else if (model.OrderBy == "HomeAboutUEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.HomeAboutUEn).Where(predicate);
			else if (model.OrderBy == "TimeInsert" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TimeInsert).Where(predicate);
			else if (model.OrderBy == "TimeInsert" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TimeInsert).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (HomeAboutUs record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					HomeAboutUsVM vm = new HomeAboutUsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public HomeAboutUsVM GetById(long HomeAboutUs)
		{
			HomeAboutUs model = _HomeAboutUsRepo.GetById(HomeAboutUs);
			HomeAboutUsVM vm = new HomeAboutUsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(HomeAboutUsVM src, HomeAboutUs dest)
		{

			dest.HomeAboutUsId = src.HomeAboutUsId;
			if (!String.IsNullOrEmpty(src.HomeAboutUsAr))
				dest.HomeAboutUsAr = src.HomeAboutUsAr;
			if (!String.IsNullOrEmpty(src.HomeAboutUEn))
				dest.HomeAboutUEn = src.HomeAboutUEn;
			if (src.TimeInsert != null && src.TimeInsert != DateTime.MinValue)
				dest.TimeInsert = src.TimeInsert;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

		private void copyToVM(HomeAboutUs src, HomeAboutUsVM dest)
		{
			dest.HomeAboutUsId = src.HomeAboutUsId;
			if (!String.IsNullOrEmpty(src.HomeAboutUsAr))
				dest.HomeAboutUsAr = src.HomeAboutUsAr;
			if (!String.IsNullOrEmpty(src.HomeAboutUEn))
				dest.HomeAboutUEn = src.HomeAboutUEn;
			if (src.TimeInsert != null && src.TimeInsert != DateTime.MinValue)
				dest.TimeInsert = src.TimeInsert;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

	}
}
