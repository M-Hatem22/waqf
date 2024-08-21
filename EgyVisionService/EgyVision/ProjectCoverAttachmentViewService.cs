using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IProjectCoverAttachmentViewService
	{
		List<ProjectCoverAttachmentViewVM> Search(ProjectCoverAttachmentViewVM model);
	}

	public class ProjectCoverAttachmentViewService : IProjectCoverAttachmentViewService
	{
		private IEgyVisionRepository<ProjectCoverAttachmentView> _ProjectCoverAttachmentViewRepo = null;
		public ProjectCoverAttachmentViewService()
		{
			_ProjectCoverAttachmentViewRepo = new EgyVisionRepository<ProjectCoverAttachmentView>();
		}

		public List<ProjectCoverAttachmentViewVM> Search(ProjectCoverAttachmentViewVM model)
		{
			List<ProjectCoverAttachmentViewVM> returned = new List<ProjectCoverAttachmentViewVM>();
			var predicate = PredicateBuilder.New<ProjectCoverAttachmentView>(true);

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
            IQueryable<ProjectCoverAttachmentView> query = _ProjectCoverAttachmentViewRepo.Table.AsExpandable().Where(predicate);

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
			else
				query = query.AsExpandable().OrderBy(x => x.KeyIdStr).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (ProjectCoverAttachmentView record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					ProjectCoverAttachmentViewVM vm = new ProjectCoverAttachmentViewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		private void copyToModel(ProjectCoverAttachmentViewVM src, ProjectCoverAttachmentView dest)
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
		}

		private void copyToVM(ProjectCoverAttachmentView src, ProjectCoverAttachmentViewVM dest)
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
		}

	}
}
