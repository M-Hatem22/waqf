using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IContentAttachmentsService
	{
		List<ContentAttachmentsVM> Search(ContentAttachmentsVM model);
		bool Insert(ContentAttachmentsVM vm);
		bool Update(ContentAttachmentsVM vm);
		bool Delete(ContentAttachmentsVM vm);
		ContentAttachmentsVM GetById(long id);
	}

	public class ContentAttachmentsService : IContentAttachmentsService
	{
		private IEgyVisionRepository<ContentAttachments> _ContentAttachmentsRepo = null;
		public ContentAttachmentsService()
		{
			_ContentAttachmentsRepo = new EgyVisionRepository<ContentAttachments>();
		}

		public bool Insert(ContentAttachmentsVM vm)
		{
			ContentAttachments model = new ContentAttachments();
			copyToModel(vm,model);
			bool success = _ContentAttachmentsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(ContentAttachmentsVM vm)
		{
			ContentAttachments model = _ContentAttachmentsRepo.GetById(vm.ContentAttachmentId);
			copyToModel(vm,model);
			return _ContentAttachmentsRepo.Update(model);
		}

		public bool Delete(ContentAttachmentsVM vm)
		{
			ContentAttachments model = _ContentAttachmentsRepo.GetById(vm.ContentAttachmentId);
			return _ContentAttachmentsRepo.Delete(model);
		}

		public List<ContentAttachmentsVM> Search(ContentAttachmentsVM model)
		{
			List<ContentAttachmentsVM> returned = new List<ContentAttachmentsVM>();
			var predicate = PredicateBuilder.New<ContentAttachments>(true);

			//if (model.ContentAttachmentId > 0)
			//{
				//predicate = predicate.And(p => p.ContentAttachmentId == model.ContentAttachmentId);
			//}
			//if (model.ContentId > 0)
			//{
				//predicate = predicate.And(p => p.ContentId == model.ContentId);
			//}
				//predicate = predicate.And(p => p.AttachmentFile == model.AttachmentFile);
			//if (!String.IsNullOrEmpty(model.AttachmentContent))
			//{
				//predicate = predicate.And(p => p.AttachmentContent == model.AttachmentContent);
			//}
			//if (!String.IsNullOrEmpty(model.AttachmentName))
			//{
				//predicate = predicate.And(p => p.AttachmentName == model.AttachmentName);
			//}
				//predicate = predicate.And(p => p.UploadedDate == model.UploadedDate);
			//if (model.DisplayOrder > 0)
			//{
				//predicate = predicate.And(p => p.DisplayOrder == model.DisplayOrder);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);

			IQueryable<ContentAttachments> query = _ContentAttachmentsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "ContentAttachmentId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "ContentAttachmentId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ContentAttachmentId).Where(predicate);
			else if (model.OrderBy == "ContentAttachmentId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ContentAttachmentId).Where(predicate);
			else if (model.OrderBy == "ContentId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ContentId).Where(predicate);
			else if (model.OrderBy == "ContentId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ContentId).Where(predicate);
			else if (model.OrderBy == "AttachmentFile" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentFile).Where(predicate);
			else if (model.OrderBy == "AttachmentFile" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentFile).Where(predicate);
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
			else if (model.OrderBy == "DisplayOrder" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.DisplayOrder).Where(predicate);
			else if (model.OrderBy == "DisplayOrder" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.DisplayOrder).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (ContentAttachments record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					ContentAttachmentsVM vm = new ContentAttachmentsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public ContentAttachmentsVM GetById(long id)
		{
			ContentAttachments model = _ContentAttachmentsRepo.GetById(id);
			ContentAttachmentsVM vm = new ContentAttachmentsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(ContentAttachmentsVM src, ContentAttachments dest)
		{
			dest.ContentAttachmentId = src.ContentAttachmentId;
			dest.ContentId = src.ContentId;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			if (!String.IsNullOrEmpty(src.AttachmentContent))
				dest.AttachmentContent = src.AttachmentContent;
			if (!String.IsNullOrEmpty(src.AttachmentName))
				dest.AttachmentName = src.AttachmentName;
			if (src.UploadedDate != null && src.UploadedDate != DateTime.MinValue)
				dest.UploadedDate = src.UploadedDate;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

		private void copyToVM(ContentAttachments src, ContentAttachmentsVM dest)
		{
			dest.ContentAttachmentId = src.ContentAttachmentId;
			dest.ContentId = src.ContentId;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			if (!String.IsNullOrEmpty(src.AttachmentContent))
				dest.AttachmentContent = src.AttachmentContent;
			if (!String.IsNullOrEmpty(src.AttachmentName))
				dest.AttachmentName = src.AttachmentName;
			if (src.UploadedDate != null && src.UploadedDate != DateTime.MinValue)
				dest.UploadedDate = src.UploadedDate;
			if (src.DisplayOrder > 0)
				dest.DisplayOrder = src.DisplayOrder;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

	}
}
