using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IProjectGalleryAttachmentViewService
	{
		List<ProjectGalleryAttachmentViewVM> Search(ProjectGalleryAttachmentViewVM model);
	}

	public class ProjectGalleryAttachmentViewService : IProjectGalleryAttachmentViewService
	{
		private IEgyVisionRepository<ProjectGalleryAttachmentView> _ProjectGalleryAttachmentViewRepo = null;
		public ProjectGalleryAttachmentViewService()
		{
			_ProjectGalleryAttachmentViewRepo = new EgyVisionRepository<ProjectGalleryAttachmentView>();
		}

		public List<ProjectGalleryAttachmentViewVM> Search(ProjectGalleryAttachmentViewVM model)
		{
			List<ProjectGalleryAttachmentViewVM> returned = new List<ProjectGalleryAttachmentViewVM>();
			var predicate = PredicateBuilder.New<ProjectGalleryAttachmentView>(true);

            if (model.ProjectId > 0)
            {
                predicate = predicate.And(p => p.ProjectId == model.ProjectId);
            }
            //if (!String.IsNullOrEmpty(model.ProjectTitleAr))
            //{
            //predicate = predicate.And(p => p.ProjectTitleAr == model.ProjectTitleAr);
            //}
            //if (!String.IsNullOrEmpty(model.ProjectTitleEn))
            //{
            //predicate = predicate.And(p => p.ProjectTitleEn == model.ProjectTitleEn);
            //}
            //predicate = predicate.And(p => p.AttachmentFile == model.AttachmentFile);
            //if (model.AttachmentId > 0)
            //{
            //predicate = predicate.And(p => p.AttachmentId == model.AttachmentId);
            //}
            //if (model.LKKeyTypeId > 0)
            //{
            //predicate = predicate.And(p => p.LKKeyTypeId == model.LKKeyTypeId);
            //}
            //if (model.LKAttachmentTypeId > 0)
            //{
            //predicate = predicate.And(p => p.LKAttachmentTypeId == model.LKAttachmentTypeId);
            //}
            //if (!String.IsNullOrEmpty(model.AttachmentName))
            //{
            //predicate = predicate.And(p => p.AttachmentName == model.AttachmentName);
            //}
            //if (!String.IsNullOrEmpty(model.AttachmentContent))
            //{
            //predicate = predicate.And(p => p.AttachmentContent == model.AttachmentContent);
            //}
            //if (!String.IsNullOrEmpty(model.KeyIdStr))
            //{
            //predicate = predicate.And(p => p.KeyIdStr == model.KeyIdStr);
            //}
            //if (!String.IsNullOrEmpty(model.ContentAr))
            //{
            //predicate = predicate.And(p => p.ContentAr == model.ContentAr);
            //}
            //if (!String.IsNullOrEmpty(model.ContentEn))
            //{
            //predicate = predicate.And(p => p.ContentEn == model.ContentEn);
            //}
            IQueryable<ProjectGalleryAttachmentView> query = _ProjectGalleryAttachmentViewRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "ProjectId";
					model.OrderByReversed = false;
			}

			if (model.OrderBy == "ProjectId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ProjectId).Where(predicate);
			else if (model.OrderBy == "ProjectId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ProjectId).Where(predicate);
			else if (model.OrderBy == "ProjectTitleAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ProjectTitleAr).Where(predicate);
			else if (model.OrderBy == "ProjectTitleAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ProjectTitleAr).Where(predicate);
			else if (model.OrderBy == "ProjectTitleEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ProjectTitleEn).Where(predicate);
			else if (model.OrderBy == "ProjectTitleEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ProjectTitleEn).Where(predicate);
			else if (model.OrderBy == "AttachmentFile" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentFile).Where(predicate);
			else if (model.OrderBy == "AttachmentFile" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentFile).Where(predicate);
			else if (model.OrderBy == "AttachmentId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentId).Where(predicate);
			else if (model.OrderBy == "AttachmentId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentId).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKKeyTypeId).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKKeyTypeId).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAttachmentTypeId).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAttachmentTypeId).Where(predicate);
			else if (model.OrderBy == "AttachmentName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentName).Where(predicate);
			else if (model.OrderBy == "AttachmentName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentName).Where(predicate);
			else if (model.OrderBy == "AttachmentContent" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentContent).Where(predicate);
			else if (model.OrderBy == "AttachmentContent" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentContent).Where(predicate);
			else if (model.OrderBy == "KeyIdStr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.KeyIdStr).Where(predicate);
			else if (model.OrderBy == "KeyIdStr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.KeyIdStr).Where(predicate);
			else if (model.OrderBy == "ContentAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ContentAr).Where(predicate);
			else if (model.OrderBy == "ContentAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ContentAr).Where(predicate);
			else if (model.OrderBy == "ContentEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ContentEn).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.ContentEn).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (ProjectGalleryAttachmentView record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					ProjectGalleryAttachmentViewVM vm = new ProjectGalleryAttachmentViewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		private void copyToModel(ProjectGalleryAttachmentViewVM src, ProjectGalleryAttachmentView dest)
		{
			dest.ProjectId = src.ProjectId;
			if (!String.IsNullOrEmpty(src.ProjectTitleAr))
				dest.ProjectTitleAr = src.ProjectTitleAr;
			if (!String.IsNullOrEmpty(src.ProjectTitleEn))
				dest.ProjectTitleEn = src.ProjectTitleEn;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			dest.AttachmentId = src.AttachmentId;
			if (src.LKKeyTypeId > 0)
				dest.LKKeyTypeId = src.LKKeyTypeId;
			if (src.LKAttachmentTypeId > 0)
				dest.LKAttachmentTypeId = src.LKAttachmentTypeId;
			if (!String.IsNullOrEmpty(src.AttachmentName))
				dest.AttachmentName = src.AttachmentName;
			if (!String.IsNullOrEmpty(src.AttachmentContent))
				dest.AttachmentContent = src.AttachmentContent;
			if (!String.IsNullOrEmpty(src.KeyIdStr))
				dest.KeyIdStr = src.KeyIdStr;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
		}

		private void copyToVM(ProjectGalleryAttachmentView src, ProjectGalleryAttachmentViewVM dest)
		{
			dest.ProjectId = src.ProjectId;
			if (!String.IsNullOrEmpty(src.ProjectTitleAr))
				dest.ProjectTitleAr = src.ProjectTitleAr;
			if (!String.IsNullOrEmpty(src.ProjectTitleEn))
				dest.ProjectTitleEn = src.ProjectTitleEn;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			dest.AttachmentId = src.AttachmentId;
			if (src.LKKeyTypeId > 0)
				dest.LKKeyTypeId = src.LKKeyTypeId;
			if (src.LKAttachmentTypeId > 0)
				dest.LKAttachmentTypeId = src.LKAttachmentTypeId;
			if (!String.IsNullOrEmpty(src.AttachmentName))
				dest.AttachmentName = src.AttachmentName;
			if (!String.IsNullOrEmpty(src.AttachmentContent))
				dest.AttachmentContent = src.AttachmentContent;
			if (!String.IsNullOrEmpty(src.KeyIdStr))
				dest.KeyIdStr = src.KeyIdStr;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
		}

	}
}
