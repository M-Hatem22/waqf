using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EgyVisionService.EgyVision
{
	public interface IFile_last_viewService
	{
		List<File_last_viewVM> Search(File_last_viewVM model);
		bool Insert(File_last_viewVM vm);
		bool Update(File_last_viewVM vm);
		bool Delete(File_last_viewVM vm);
		File_last_viewVM GetById(int id);
		File_last_viewVM GetByUserIdandFileId(string userId, int fileId);
		List<File_last_viewVM> Getlastread(int fileId);
		bool recordDownloaded(int Fileid, string useriId);

		bool recordOpened(int FileId, string userId);
    }

	public class File_last_viewService : IFile_last_viewService
	{
		private IEgyVisionRepository<File_last_view> _File_last_viewRepo = null;
        private EgyVisionContext context = null;
        public File_last_viewService()
		{
			_File_last_viewRepo = new EgyVisionRepository<File_last_view>();
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            var Configuration = builder.Build();
            string conn = Configuration.GetSection("ApplicationSettings:EgyVisionContext").Value.ToString();
            context = new EgyVisionContext(conn);
        }

		public bool Insert(File_last_viewVM vm)
		{
			File_last_view model = new File_last_view();
			copyToModel(vm,model);
			bool success = _File_last_viewRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(File_last_viewVM vm)
		{
			File_last_view model = _File_last_viewRepo.GetById(vm.id);
			copyToModel(vm,model);
			return _File_last_viewRepo.Update(model);
		}
		public bool recordOpened(int Fileid , string useriId)
		{
			File_last_view model = new File_last_view();
			model.userId = useriId;
			model.fileId= Fileid;
			model.lastView = DateTime.Now;

			return _File_last_viewRepo.Insert(model); ;
        }
        public bool recordDownloaded(int Fileid, string useriId)
        {
            File_last_view model = new File_last_view();
            model.userId = useriId;
            model.fileId = Fileid;
            model.lastDownload = DateTime.Now;
            return _File_last_viewRepo.Insert(model); ;
        }

        public bool Delete(File_last_viewVM vm)
		{
			File_last_view model = _File_last_viewRepo.GetById(vm.id);
			return _File_last_viewRepo.Delete(model);
		}

		public List<File_last_viewVM> Search(File_last_viewVM model)
		{
			List<File_last_viewVM> returned = new List<File_last_viewVM>();
			var predicate = PredicateBuilder.New<File_last_view>(true);

			//if (!String.IsNullOrEmpty(model.userId))
			//{
				//predicate = predicate.And(p => p.userId == model.userId);
			//}
			//if (!String.IsNullOrEmpty(model.userMail))
			//{
				//predicate = predicate.And(p => p.userMail == model.userMail);
			//}
			//if (model.fileId > 0)
			//{
				//predicate = predicate.And(p => p.fileId == model.fileId);
			//}
			//if (!String.IsNullOrEmpty(model.fileNameAr))
			//{
				//predicate = predicate.And(p => p.fileNameAr == model.fileNameAr);
			//}
				//predicate = predicate.And(p => p.lastView == model.lastView);
				//predicate = predicate.And(p => p.lastDownload == model.lastDownload);
			//if (model.id > 0)
			//{
				//predicate = predicate.And(p => p.id == model.id);
			//}

			IQueryable<File_last_view> query = _File_last_viewRepo.Table.AsExpandable().Where(predicate);
			IQueryable<File_last_view> queryCount = _File_last_viewRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "userId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "userId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.userId).Where(predicate);
			else if (model.OrderBy == "userId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.userId).Where(predicate);
			else if (model.OrderBy == "userMail" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.userMail).Where(predicate);
			else if (model.OrderBy == "userMail" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.userMail).Where(predicate);
			else if (model.OrderBy == "fileId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.fileId).Where(predicate);
			else if (model.OrderBy == "fileId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.fileId).Where(predicate);
			else if (model.OrderBy == "fileNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.fileNameAr).Where(predicate);
			else if (model.OrderBy == "fileNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.fileNameAr).Where(predicate);
			else if (model.OrderBy == "lastView" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.lastView).Where(predicate);
			else if (model.OrderBy == "lastView" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.lastView).Where(predicate);
			else if (model.OrderBy == "lastDownload" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.lastDownload).Where(predicate);
			else if (model.OrderBy == "lastDownload" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.lastDownload).Where(predicate);
			else if (model.OrderBy == "id" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.id).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.id).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (File_last_view record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					File_last_viewVM vm = new File_last_viewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public File_last_viewVM GetById(int id)
		{
			File_last_view model = _File_last_viewRepo.GetById(id);
			File_last_viewVM vm = new File_last_viewVM();
			copyToVM(model,vm);
			return vm;
		}
        public File_last_viewVM GetByUserIdandFileId(string userId, int fileId)
        {
            File_last_view model = context.File_last_view
				.Where(i=>i.userId ==userId &&  i.fileId ==fileId)
				.FirstOrDefault();
			if (model == null) return null;
            File_last_viewVM vm = new File_last_viewVM();
            copyToVM(model, vm);
            return vm;
        }
		public List<File_last_viewVM>Getlastread(int fileId)
		{
			List<File_last_view> model = context.File_last_view.Where(i => i.fileId == fileId).ToList();
			if(model != null && model.Count > 0)
			{
                List<File_last_viewVM> vmList = new List<File_last_viewVM>();
                foreach (var item in model)
                {
                    File_last_viewVM vm = new File_last_viewVM();
                    copyToVM(item, vm);
					vmList.Add(vm);
                }
				return vmList;
            }
			return null;
		}

        private void copyToModel(File_last_viewVM src, File_last_view dest)
		{
			if (!String.IsNullOrEmpty(src.userId))
				dest.userId = src.userId;
			if (!String.IsNullOrEmpty(src.userMail))
				dest.userMail = src.userMail;
			dest.fileId = src.fileId;
			if (!String.IsNullOrEmpty(src.fileNameAr))
				dest.fileNameAr = src.fileNameAr;
			if (src.lastView != null && src.lastView != DateTime.MinValue)
				dest.lastView = src.lastView;
			if (src.lastDownload != null && src.lastDownload != DateTime.MinValue)
				dest.lastDownload = src.lastDownload;
			if (src.id > 0)
				dest.id = src.id;
		}

		private void copyToVM(File_last_view src, File_last_viewVM dest)
		{
			if (!String.IsNullOrEmpty(src.userId))
				dest.userId = src.userId;
			if (!String.IsNullOrEmpty(src.userMail))
				dest.userMail = src.userMail;
			dest.fileId = src.fileId;
			if (!String.IsNullOrEmpty(src.fileNameAr))
				dest.fileNameAr = src.fileNameAr;
			if (src.lastView != null && src.lastView != DateTime.MinValue)
				dest.lastView = src.lastView;
			if (src.lastDownload != null && src.lastDownload != DateTime.MinValue)
				dest.lastDownload = src.lastDownload;
			if (src.id > 0)
				dest.id = src.id;
		}

        
    }
}
