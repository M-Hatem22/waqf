using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IToaaFilesService
	{
		List<ToaaFilesVM> Search(ToaaFilesVM model);
		bool Insert(ToaaFilesVM vm);
		bool Update(ToaaFilesVM vm);
		bool Delete(ToaaFilesVM vm);
		ToaaFilesVM GetById(int id);
		List<ToaaFilesVM> GetBasicFiles(ToaaFilesVM model);
	}

	public class ToaaFilesService : IToaaFilesService
	{
		private IEgyVisionRepository<ToaaFiles> _ToaaFilesRepo = null;
		public ToaaFilesService()
		{
			_ToaaFilesRepo = new EgyVisionRepository<ToaaFiles>();
		}

		public bool Insert(ToaaFilesVM vm)
		{
			ToaaFiles model = new ToaaFiles();
			copyToModel(vm,model);
			bool success = _ToaaFilesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(ToaaFilesVM vm)
		{
			ToaaFiles model = _ToaaFilesRepo.GetById(vm.id);
			copyToModel(vm,model);
			return _ToaaFilesRepo.Update(model);
		}

		public bool Delete(ToaaFilesVM vm)
		{
			ToaaFiles model = _ToaaFilesRepo.GetById(vm.id);
			return _ToaaFilesRepo.Delete(model);
		}

		public List<ToaaFilesVM> Search(ToaaFilesVM model)
		{
			List<ToaaFilesVM> returned = new List<ToaaFilesVM>();
			var predicate = PredicateBuilder.New<ToaaFiles>(true);

			//if (model.id > 0)
			//{
			//predicate = predicate.And(p => p.id == model.id);
			//}
			//if (!String.IsNullOrEmpty(model.fileNameAr))
			//{
			//predicate = predicate.And(p => p.fileNameAr == model.fileNameAr);
			//}
			//if (!String.IsNullOrEmpty(model.fileNameEn))
			//{
			//predicate = predicate.And(p => p.fileNameEn == model.fileNameEn);
			//}
			//predicate = predicate.And(p => p.fileContent == model.fileContent);
			//if (!String.IsNullOrEmpty(model.fileType))
			//{
			//predicate = predicate.And(p => p.fileType == model.fileType);
			//}
			//predicate = predicate.And(p => p.uploadDate == model.uploadDate);
			//predicate = predicate.And(p => p.isDeleted == model.isDeleted);

			IQueryable<ToaaFiles> query = _ToaaFilesRepo.Table.AsExpandable().Where(predicate).Where(a => a.isDeleted == null).Where(a => a.id != 14 && a.id != 15 && a.id != 16);
			IQueryable<ToaaFiles> queryCount = _ToaaFilesRepo.Table.AsExpandable().Where(predicate).Where(a => a.isDeleted == null).Where(a => a.id != 14 && a.id != 15 && a.id != 16);

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
			else if (model.OrderBy == "fileNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.fileNameAr).Where(predicate);
			else if (model.OrderBy == "fileNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.fileNameAr).Where(predicate);
			else if (model.OrderBy == "fileNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.fileNameEn).Where(predicate);
			else if (model.OrderBy == "fileNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.fileNameEn).Where(predicate);
			else if (model.OrderBy == "fileContent" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.fileContent).Where(predicate);
			else if (model.OrderBy == "fileContent" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.fileContent).Where(predicate);
			else if (model.OrderBy == "fileType" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.fileType).Where(predicate);
			else if (model.OrderBy == "fileType" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.fileType).Where(predicate);
			else if (model.OrderBy == "uploadDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.uploadDate).Where(predicate);
			else if (model.OrderBy == "uploadDate" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.uploadDate).Where(predicate);
			else if (model.OrderBy == "isDeleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.isDeleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.isDeleted).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (ToaaFiles record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					ToaaFilesVM vm = new ToaaFilesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public ToaaFilesVM GetById(int id)
		{
			ToaaFiles model = _ToaaFilesRepo.GetById(id);
			ToaaFilesVM vm = new ToaaFilesVM();
			copyToVM(model,vm);
			vm.fileContent = model.fileContent;
			return vm;
		}
        public List<ToaaFilesVM> GetBasicFiles(ToaaFilesVM model)
        {
            IQueryable<ToaaFiles> query = _ToaaFilesRepo.Table.AsExpandable().Where(a=> a.id ==14 | a.id ==15 | a.id == 16);
            IQueryable<ToaaFiles> queryCount = _ToaaFilesRepo.Table.AsExpandable().Where(a => a.id == 14 | a.id == 15 | a.id == 16);
            int index = 0;
            List<ToaaFilesVM> returned = new List<ToaaFilesVM>();
            foreach (ToaaFiles record in query)
			{ 
                    ToaaFilesVM vm = new ToaaFilesVM();
                    copyToVM(record, vm);
                    returned.Add(vm);
            }
            model.TotalRecordCount = queryCount.Count();
            return returned;
        }

        private void copyToModel(ToaaFilesVM src, ToaaFiles dest)
		{
			if (src.id > 0)
				dest.id = src.id;
			if (!String.IsNullOrEmpty(src.fileNameAr))
				dest.fileNameAr = src.fileNameAr;
			if (!String.IsNullOrEmpty(src.fileNameEn))
				dest.fileNameEn = src.fileNameEn;
			if (src.fileContent != null && src.fileContent.Length > 0)
				dest.fileContent = src.fileContent;
			if (!String.IsNullOrEmpty(src.fileType))
				dest.fileType = src.fileType;
			if (src.uploadDate != null && src.uploadDate != DateTime.MinValue)
				dest.uploadDate = src.uploadDate;
			if (src.isDeleted != null && src.isDeleted != DateTime.MinValue)
				dest.isDeleted = src.isDeleted;
		}

		private void copyToVM(ToaaFiles src, ToaaFilesVM dest)
		{
			if (src.id > 0)
				dest.id = src.id;
			if (!String.IsNullOrEmpty(src.fileNameAr))
				dest.fileNameAr = src.fileNameAr;
			if (!String.IsNullOrEmpty(src.fileNameEn))
				dest.fileNameEn = src.fileNameEn;
			if (src.fileContent != null && src.fileContent.Length > 0)
				dest.fileContent = new byte[1];
			if (!String.IsNullOrEmpty(src.fileType))
				dest.fileType = src.fileType;
			if (src.uploadDate != null && src.uploadDate != DateTime.MinValue)
				dest.uploadDate = src.uploadDate;
			if (src.isDeleted != null && src.isDeleted != DateTime.MinValue)
				dest.isDeleted = src.isDeleted;
		}

        
    }
}
