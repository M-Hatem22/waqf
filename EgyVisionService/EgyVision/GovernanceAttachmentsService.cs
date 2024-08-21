using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IGovernanceAttachmentsService
	{
		List<GovernanceAttachmentsVM> Search(GovernanceAttachmentsVM model);
		List<GovernanceAttachmentsVM> GetwithoutAttachments(GovernanceAttachmentsVM model);

		bool Insert(GovernanceAttachmentsVM vm);
		bool Update(GovernanceAttachmentsVM vm);
		bool Delete(GovernanceAttachmentsVM vm);
		GovernanceAttachmentsVM GetById(long GovernanceAttachmentsId);
	}

	public class GovernanceAttachmentsService : IGovernanceAttachmentsService
	{
		private IEgyVisionRepository<GovernanceAttachments> _GovernanceAttachmentsRepo = null;
		public GovernanceAttachmentsService()
		{
			_GovernanceAttachmentsRepo = new EgyVisionRepository<GovernanceAttachments>();
		}

		public bool Insert(GovernanceAttachmentsVM vm)
		{
			GovernanceAttachments model = new GovernanceAttachments();
			copyToModel(vm,model);
			bool success = _GovernanceAttachmentsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(GovernanceAttachmentsVM vm)
		{
			GovernanceAttachments model = _GovernanceAttachmentsRepo.GetById(vm.GovernanceAttachmentsId);
			copyToModel(vm,model);
			return _GovernanceAttachmentsRepo.Update(model);
		}

		public bool Delete(GovernanceAttachmentsVM vm)
		{
			GovernanceAttachments model = _GovernanceAttachmentsRepo.GetById(vm.GovernanceAttachmentsId);
			return _GovernanceAttachmentsRepo.Delete(model);
		}
		public GovernanceAttachmentsVM GetById(long GovernanceAttachmentsId)
		{
			GovernanceAttachments model = _GovernanceAttachmentsRepo.GetById(GovernanceAttachmentsId);
			GovernanceAttachmentsVM vm = new GovernanceAttachmentsVM();
			copyToVM(model, vm);
			return vm;
		}

		public List<GovernanceAttachmentsVM> Search(GovernanceAttachmentsVM model)
		{
			List<GovernanceAttachmentsVM> returned = new List<GovernanceAttachmentsVM>();
			var predicate = PredicateBuilder.New<GovernanceAttachments>(true);

            if (model.GovernanceAttachmentsId > 0)
            {
                predicate = predicate.And(p => p.GovernanceAttachmentsId == model.GovernanceAttachmentsId);
            }
            if (model.LkGovernanceId > 0)
            {
                predicate = predicate.And(p => p.LkGovernanceId == model.LkGovernanceId);
            }
            //if (!String.IsNullOrEmpty(model.GovernanceAttachmentsNameAr))
            //{
            //predicate = predicate.And(p => p.GovernanceAttachmentsNameAr == model.GovernanceAttachmentsNameAr);
            //}
            //if (!String.IsNullOrEmpty(model.GovernanceAttachmentsNameEn))
            //{
            //predicate = predicate.And(p => p.GovernanceAttachmentsNameEn == model.GovernanceAttachmentsNameEn);
            //}
            //if (!String.IsNullOrEmpty(model.AttachmentContent))
            //{
            //predicate = predicate.And(p => p.AttachmentContent == model.AttachmentContent);
            //}
            //if (!String.IsNullOrEmpty(model.AttachmentName))
            //{
            //predicate = predicate.And(p => p.AttachmentName == model.AttachmentName);
            //}
            //predicate = predicate.And(p => p.UploadedDate == model.UploadedDate);
            predicate = predicate.And(p => p.Deleted == model.Deleted);

            IQueryable<GovernanceAttachments> query = _GovernanceAttachmentsRepo.Table.AsExpandable().Where(predicate);
			IQueryable<GovernanceAttachments> queryCount = _GovernanceAttachmentsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "GovernanceAttachmentsId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "GovernanceAttachmentsId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GovernanceAttachmentsId).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GovernanceAttachmentsId).Where(predicate);
			else if (model.OrderBy == "LkGovernanceId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LkGovernanceId).Where(predicate);
			else if (model.OrderBy == "LkGovernanceId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LkGovernanceId).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GovernanceAttachmentsNameAr).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GovernanceAttachmentsNameAr).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GovernanceAttachmentsNameEn).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GovernanceAttachmentsNameEn).Where(predicate);
			else if (model.OrderBy == "AttachmentContent" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentContent).Where(predicate);
			else if (model.OrderBy == "AttachmentContent" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentContent).Where(predicate);
			else if (model.OrderBy == "AttachmentName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentName).Where(predicate);
			else if (model.OrderBy == "AttachmentName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentName).Where(predicate);
			else if (model.OrderBy == "UploadedDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UploadedDate).Where(predicate);
			else if (model.OrderBy == "UploadedDate" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UploadedDate).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (GovernanceAttachments record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					GovernanceAttachmentsVM vm = new GovernanceAttachmentsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}	
		public List<GovernanceAttachmentsVM> GetwithoutAttachments(GovernanceAttachmentsVM model)
		{
			List<GovernanceAttachmentsVM> returned = new List<GovernanceAttachmentsVM>();
			var predicate = PredicateBuilder.New<GovernanceAttachments>(true);

            if (model.GovernanceAttachmentsId > 0)
            {
                predicate = predicate.And(p => p.GovernanceAttachmentsId == model.GovernanceAttachmentsId);
            }
            if (model.LkGovernanceId > 0)
            {
                predicate = predicate.And(p => p.LkGovernanceId == model.LkGovernanceId);
            }
            //if (!String.IsNullOrEmpty(model.GovernanceAttachmentsNameAr))
            //{
            //predicate = predicate.And(p => p.GovernanceAttachmentsNameAr == model.GovernanceAttachmentsNameAr);
            //}
            //if (!String.IsNullOrEmpty(model.GovernanceAttachmentsNameEn))
            //{
            //predicate = predicate.And(p => p.GovernanceAttachmentsNameEn == model.GovernanceAttachmentsNameEn);
            //}
            //if (!String.IsNullOrEmpty(model.AttachmentContent))
            //{
            //predicate = predicate.And(p => p.AttachmentContent == model.AttachmentContent);
            //}
            //if (!String.IsNullOrEmpty(model.AttachmentName))
            //{
            //predicate = predicate.And(p => p.AttachmentName == model.AttachmentName);
            //}
            //predicate = predicate.And(p => p.UploadedDate == model.UploadedDate);
            predicate = predicate.And(p => p.Deleted == model.Deleted);

            IQueryable<GovernanceAttachments> query = _GovernanceAttachmentsRepo.Table.AsExpandable().Where(predicate).Select(s => new GovernanceAttachments { GovernanceAttachmentsId=s.GovernanceAttachmentsId,GovernanceAttachmentsNameAr=s.GovernanceAttachmentsNameAr,GovernanceAttachmentsNameEn=s.GovernanceAttachmentsNameEn,LkGovernanceId=s.LkGovernanceId,AttachmentName=s.AttachmentName,UploadedDate=s.UploadedDate,Deleted=s.Deleted,LkGovernanceNameAr=s.LkGovernanceNameAr});
			IQueryable<GovernanceAttachments> queryCount = _GovernanceAttachmentsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "GovernanceAttachmentsId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "GovernanceAttachmentsId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GovernanceAttachmentsId).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GovernanceAttachmentsId).Where(predicate);
			else if (model.OrderBy == "LkGovernanceId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LkGovernanceId).Where(predicate);
			else if (model.OrderBy == "LkGovernanceId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LkGovernanceId).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GovernanceAttachmentsNameAr).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsNameAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GovernanceAttachmentsNameAr).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsNameEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GovernanceAttachmentsNameEn).Where(predicate);
			else if (model.OrderBy == "GovernanceAttachmentsNameEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GovernanceAttachmentsNameEn).Where(predicate);
			else if (model.OrderBy == "AttachmentContent" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentContent).Where(predicate);
			else if (model.OrderBy == "AttachmentContent" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentContent).Where(predicate);
			else if (model.OrderBy == "AttachmentName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentName).Where(predicate);
			else if (model.OrderBy == "AttachmentName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentName).Where(predicate);
			else if (model.OrderBy == "UploadedDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UploadedDate).Where(predicate);
			else if (model.OrderBy == "UploadedDate" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UploadedDate).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (GovernanceAttachments record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					GovernanceAttachmentsVM vm = new GovernanceAttachmentsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}


		private void copyToModel(GovernanceAttachmentsVM src, GovernanceAttachments dest)
		{
			dest.GovernanceAttachmentsId = src.GovernanceAttachmentsId;
			if (src.LkGovernanceId > 0)
				dest.LkGovernanceId = src.LkGovernanceId;
			if (!String.IsNullOrEmpty(src.GovernanceAttachmentsNameAr))
				dest.GovernanceAttachmentsNameAr = src.GovernanceAttachmentsNameAr;
			if (!String.IsNullOrEmpty(src.GovernanceAttachmentsNameEn))
				dest.GovernanceAttachmentsNameEn = src.GovernanceAttachmentsNameEn;
			
			if (!String.IsNullOrEmpty(src.AttachmentName))
				dest.AttachmentName = src.AttachmentName;
			if (src.UploadedDate != null && src.UploadedDate != DateTime.MinValue)
				dest.UploadedDate = src.UploadedDate;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			if (!String.IsNullOrEmpty(src.AttachmentContent))
				dest.AttachmentContent = src.AttachmentContent;
			if (!String.IsNullOrEmpty(src.LkGovernanceNameAr))
				dest.LkGovernanceNameAr = src.LkGovernanceNameAr;

		}

		private void copyToVM(GovernanceAttachments src, GovernanceAttachmentsVM dest)
		{
			dest.GovernanceAttachmentsId = src.GovernanceAttachmentsId;
			if (src.LkGovernanceId > 0)
				dest.LkGovernanceId = src.LkGovernanceId;
			if (!String.IsNullOrEmpty(src.GovernanceAttachmentsNameAr))
				dest.GovernanceAttachmentsNameAr = src.GovernanceAttachmentsNameAr;
			if (!String.IsNullOrEmpty(src.GovernanceAttachmentsNameEn))
				dest.GovernanceAttachmentsNameEn = src.GovernanceAttachmentsNameEn;
			
			if (!String.IsNullOrEmpty(src.AttachmentName))
				dest.AttachmentName = src.AttachmentName;
			if (src.UploadedDate != null && src.UploadedDate != DateTime.MinValue)
				dest.UploadedDate = src.UploadedDate;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			if (!String.IsNullOrEmpty(src.AttachmentContent))
				dest.AttachmentContent = src.AttachmentContent;
			if (!String.IsNullOrEmpty(src.LkGovernanceNameAr))
				dest.LkGovernanceNameAr = src.LkGovernanceNameAr;
		}

	}
}
