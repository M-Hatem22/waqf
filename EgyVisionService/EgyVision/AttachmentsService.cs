using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IAttachmentsService
	{
		List<AttachmentsVM> Search(AttachmentsVM model);
		bool Insert(AttachmentsVM vm);
		bool Update(AttachmentsVM vm);
		bool Delete(AttachmentsVM vm);
		AttachmentsVM GetById(long AttachmentId);
	}

	public class AttachmentsService : IAttachmentsService
	{
		private IEgyVisionRepository<Attachments> _AttachmentsRepo = null;
		public AttachmentsService()
		{
			_AttachmentsRepo = new EgyVisionRepository<Attachments>();
		}

		public bool Insert(AttachmentsVM vm)
		{
			Attachments model = new Attachments();
			copyToModel(vm,model);
			bool success = _AttachmentsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AttachmentsVM vm)
		{
			Attachments model = _AttachmentsRepo.GetById(vm.AttachmentId);
			copyToModel(vm,model);
			return _AttachmentsRepo.Update(model);
		}

		public bool Delete(AttachmentsVM vm)
		{
			Attachments model = _AttachmentsRepo.GetById(vm.AttachmentId);
			return _AttachmentsRepo.Delete(model);
		}

		public List<AttachmentsVM> Search(AttachmentsVM model)
		{
			List<AttachmentsVM> returned = new List<AttachmentsVM>();
			var predicate = PredicateBuilder.New<Attachments>(true);

            if (model.AttachmentId > 0)
            {
                predicate = predicate.And(p => p.AttachmentId == model.AttachmentId);
            }
            if (model.KeyId > 0)
            {
                predicate = predicate.And(p => p.KeyId == model.KeyId);
            }
            if (!String.IsNullOrEmpty(model.KeyIdStr))
            {
                predicate = predicate.And(p => p.KeyIdStr == model.KeyIdStr);
            }
            if (model.LKAttachmentTypeId > 0)
            {
                predicate = predicate.And(p => p.LKAttachmentTypeId == model.LKAttachmentTypeId);
            }
            if (model.LKKeyTypeId > 0)
            {
                predicate = predicate.And(p => p.LKKeyTypeId == model.LKKeyTypeId);
            }
            predicate = predicate.And(p => p.Deleted == model.Deleted);
            IQueryable<Attachments> query = _AttachmentsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "AttachmentId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "AttachmentId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentId).Where(predicate);
			else if (model.OrderBy == "AttachmentId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentId).Where(predicate);
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
			else if (model.OrderBy == "KeyId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.KeyId).Where(predicate);
			else if (model.OrderBy == "KeyId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.KeyId).Where(predicate);
			else if (model.OrderBy == "KeyIdStr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.KeyIdStr).Where(predicate);
			else if (model.OrderBy == "KeyIdStr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.KeyIdStr).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAttachmentTypeId).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAttachmentTypeId).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKKeyTypeId).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKKeyTypeId).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (Attachments record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AttachmentsVM vm = new AttachmentsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}
			model.TotalRecordCount = returned.Count();
			return returned;
		}

		public AttachmentsVM GetById(long AttachmentId)
		{
			Attachments model = _AttachmentsRepo.GetById(AttachmentId);
			AttachmentsVM vm = new AttachmentsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AttachmentsVM src, Attachments dest)
		{
			dest.AttachmentId = src.AttachmentId;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			if (!String.IsNullOrEmpty(src.AttachmentContent))
				dest.AttachmentContent = src.AttachmentContent;
			if (!String.IsNullOrEmpty(src.AttachmentName))
				dest.AttachmentName = src.AttachmentName;
			if (src.UploadedDate != null && src.UploadedDate != DateTime.MinValue)
				dest.UploadedDate = src.UploadedDate;
			dest.KeyId = src.KeyId;
			if (!String.IsNullOrEmpty(src.KeyIdStr))
				dest.KeyIdStr = src.KeyIdStr;
			if (src.LKAttachmentTypeId > 0)
				dest.LKAttachmentTypeId = src.LKAttachmentTypeId;
			if (src.LKKeyTypeId > 0)
				dest.LKKeyTypeId = src.LKKeyTypeId;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

		private void copyToVM(Attachments src, AttachmentsVM dest)
		{
			dest.AttachmentId = src.AttachmentId;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			if (!String.IsNullOrEmpty(src.AttachmentContent))
				dest.AttachmentContent = src.AttachmentContent;
			if (!String.IsNullOrEmpty(src.AttachmentName))
				dest.AttachmentName = src.AttachmentName;
			if (src.UploadedDate != null && src.UploadedDate != DateTime.MinValue)
				dest.UploadedDate = src.UploadedDate;
			dest.KeyId = src.KeyId;
			if (!String.IsNullOrEmpty(src.KeyIdStr))
				dest.KeyIdStr = src.KeyIdStr;
			if (src.LKAttachmentTypeId > 0)
				dest.LKAttachmentTypeId = src.LKAttachmentTypeId;
			if (src.LKKeyTypeId > 0)
				dest.LKKeyTypeId = src.LKKeyTypeId;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
		}

	}
}
