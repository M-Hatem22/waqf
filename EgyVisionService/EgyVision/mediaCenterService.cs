using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ImediaCenterService
	{
		List<mediaCenterVM> Search(mediaCenterVM model);
		bool Insert(mediaCenterVM vm);
		bool Update(mediaCenterVM vm);
		bool Delete(mediaCenterVM vm);
		mediaCenterVM GetById(int id);
	}

	public class mediaCenterService : ImediaCenterService
	{
		private IEgyVisionRepository<mediaCenter> _mediaCenterRepo = null;
		public mediaCenterService()
		{
			_mediaCenterRepo = new EgyVisionRepository<mediaCenter>();
		}

		public bool Insert(mediaCenterVM vm)
		{
			mediaCenter model = new mediaCenter();
			copyToModel(vm,model);
			bool success = _mediaCenterRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(mediaCenterVM vm)
		{
			mediaCenter model = _mediaCenterRepo.GetById(vm.id);
			copyToModel(vm,model);
			return _mediaCenterRepo.Update(model);
		}

		public bool Delete(mediaCenterVM vm)
		{
			mediaCenter model = _mediaCenterRepo.GetById(vm.id);
			model.isDeleted = DateTime.Now;
			return true;
		}

		public List<mediaCenterVM> Search(mediaCenterVM model)
		{
			List<mediaCenterVM> returned = new List<mediaCenterVM>();
			var predicate = PredicateBuilder.New<mediaCenter>(true);

			//if (model.id > 0)
			//{
			//predicate = predicate.And(p => p.id == model.id);
			//}
			//if (!String.IsNullOrEmpty(model.title))
			//{
			//predicate = predicate.And(p => p.title == model.title);
			//}
			//predicate = predicate.And(p => p.image == model.image);
			//if (!String.IsNullOrEmpty(model.article))
			//{
			//predicate = predicate.And(p => p.article == model.article);
			//}
			//if (!String.IsNullOrEmpty(model.link))
			//{
			//predicate = predicate.And(p => p.link == model.link);
			//}
			//predicate = predicate.And(p => p.dateAdded == model.dateAdded);
			//predicate = predicate.And(p => p.isDeleted == model.isDeleted);

			IQueryable<mediaCenter> query = _mediaCenterRepo.Table.AsExpandable().Where(predicate).Where(a => a.isDeleted == null);
			IQueryable<mediaCenter> queryCount = _mediaCenterRepo.Table.AsExpandable().Where(predicate).Where(a => a.isDeleted == null);

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
			else if (model.OrderBy == "title" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.title).Where(predicate);
			else if (model.OrderBy == "title" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.title).Where(predicate);
			else if (model.OrderBy == "image" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.image).Where(predicate);
			else if (model.OrderBy == "image" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.image).Where(predicate);
			else if (model.OrderBy == "article" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.article).Where(predicate);
			else if (model.OrderBy == "article" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.article).Where(predicate);
			else if (model.OrderBy == "link" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.link).Where(predicate);
			else if (model.OrderBy == "link" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.link).Where(predicate);
			else if (model.OrderBy == "dateAdded" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.dateAdded).Where(predicate);
			else if (model.OrderBy == "dateAdded" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.dateAdded).Where(predicate);
			else if (model.OrderBy == "isDeleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.isDeleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.isDeleted).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (mediaCenter record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					mediaCenterVM vm = new mediaCenterVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public mediaCenterVM GetById(int id)
		{
			mediaCenter model = _mediaCenterRepo.GetById(id);
			mediaCenterVM vm = new mediaCenterVM();
			copyToVM(model,vm);
            if (model.image != null && model.image.Length > 0)
             vm.image = model.image;
			return vm;
		}

		private void copyToModel(mediaCenterVM src, mediaCenter dest)
		{
			if (src.id > 0)
				dest.id = src.id;
			if (!String.IsNullOrEmpty(src.title))
				dest.title = src.title;
			if (src.image != null && src.image.Length > 0)
				dest.image = src.image;
			if (!String.IsNullOrEmpty(src.article))
				dest.article = src.article;
			if (!String.IsNullOrEmpty(src.link))
				dest.link = src.link;
			if (src.dateAdded != null && src.dateAdded != DateTime.MinValue)
				dest.dateAdded = src.dateAdded;
			if (src.isDeleted != null && src.isDeleted != DateTime.MinValue)
				dest.isDeleted = src.isDeleted;
		}

		private void copyToVM(mediaCenter src, mediaCenterVM dest)
		{
			if (src.id > 0)
				dest.id = src.id;
			if (!String.IsNullOrEmpty(src.title))
				dest.title = src.title;
			if (src.image != null && src.image.Length > 0)
				dest.image = new byte[2];
			if (!String.IsNullOrEmpty(src.article))
				dest.article = src.article;
			if (!String.IsNullOrEmpty(src.link))
				dest.link = src.link;
			if (src.dateAdded != null && src.dateAdded != DateTime.MinValue)
				dest.dateAdded = src.dateAdded;
			if (src.isDeleted != null && src.isDeleted != DateTime.MinValue)
				dest.isDeleted = src.isDeleted;
		}

	}
}
