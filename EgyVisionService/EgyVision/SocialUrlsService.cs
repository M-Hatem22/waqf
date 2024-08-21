using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ISocialUrlsService
	{
		List<SocialUrlsVM> Search(SocialUrlsVM model);
		bool Insert(SocialUrlsVM vm);
		bool Update(SocialUrlsVM vm);
		bool Delete(SocialUrlsVM vm);
		SocialUrlsVM GetById(int SocialUrlId);
	}

	public class SocialUrlsService : ISocialUrlsService
	{
		private IEgyVisionRepository<SocialUrls> _SocialUrlsRepo = null;
		public SocialUrlsService()
		{
			_SocialUrlsRepo = new EgyVisionRepository<SocialUrls>();
		}

		public bool Insert(SocialUrlsVM vm)
		{
			SocialUrls model = new SocialUrls();
			copyToModel(vm,model);
			bool success = _SocialUrlsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(SocialUrlsVM vm)
		{
			SocialUrls model = _SocialUrlsRepo.GetById(vm.SocialUrlId);
			copyToModel(vm,model);
			return _SocialUrlsRepo.Update(model);
		}

		public bool Delete(SocialUrlsVM vm)
		{
			SocialUrls model = _SocialUrlsRepo.GetById(vm.SocialUrlId);
			return _SocialUrlsRepo.Delete(model);
		}

		public List<SocialUrlsVM> Search(SocialUrlsVM model)
		{
			List<SocialUrlsVM> returned = new List<SocialUrlsVM>();
			var predicate = PredicateBuilder.New<SocialUrls>(true);

			//if (model.SocialUrlId > 0)
			//{
				//predicate = predicate.And(p => p.SocialUrlId == model.SocialUrlId);
			//}
			//if (!String.IsNullOrEmpty(model.NameAr))
			//{
				//predicate = predicate.And(p => p.NameAr == model.NameAr);
			//}
			//if (!String.IsNullOrEmpty(model.NameEn))
			//{
				//predicate = predicate.And(p => p.NameEn == model.NameEn);
			//}
			//if (!String.IsNullOrEmpty(model.Url))
			//{
				//predicate = predicate.And(p => p.Url == model.Url);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);

			IQueryable<SocialUrls> query = _SocialUrlsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "SocialUrlId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "SocialUrlId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.SocialUrlId).Where(predicate);
			else if (model.OrderBy == "SocialUrlId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.SocialUrlId).Where(predicate);
			else if (model.OrderBy == "NameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NameAr).Where(predicate);
			else if (model.OrderBy == "NameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NameAr).Where(predicate);
			else if (model.OrderBy == "NameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NameEn).Where(predicate);
			else if (model.OrderBy == "NameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NameEn).Where(predicate);
			else if (model.OrderBy == "Url" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Url).Where(predicate);
			else if (model.OrderBy == "Url" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Url).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (SocialUrls record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					SocialUrlsVM vm = new SocialUrlsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public SocialUrlsVM GetById(int SocialUrlId)
		{
			SocialUrls model = _SocialUrlsRepo.GetById(SocialUrlId);
			SocialUrlsVM vm = new SocialUrlsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(SocialUrlsVM src, SocialUrls dest)
		{
			if (src.SocialUrlId > 0)
				dest.SocialUrlId = src.SocialUrlId;
			if (!String.IsNullOrEmpty(src.NameAr))
				dest.NameAr = src.NameAr;
			if (!String.IsNullOrEmpty(src.NameEn))
				dest.NameEn = src.NameEn;
			if (!String.IsNullOrEmpty(src.Url))
				dest.Url = src.Url;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

		private void copyToVM(SocialUrls src, SocialUrlsVM dest)
		{
			if (src.SocialUrlId > 0)
				dest.SocialUrlId = src.SocialUrlId;
			if (!String.IsNullOrEmpty(src.NameAr))
				dest.NameAr = src.NameAr;
			if (!String.IsNullOrEmpty(src.NameEn))
				dest.NameEn = src.NameEn;
			if (!String.IsNullOrEmpty(src.Url))
				dest.Url = src.Url;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

	}
}
