using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IProjectsService
	{
		List<ProjectsVM> Search(ProjectsVM model);
		bool Insert(ProjectsVM vm);
		ProjectsVM InsertAndReturn(ProjectsVM vm);
		bool Update(ProjectsVM vm);
		bool Delete(ProjectsVM vm);
		ProjectsVM GetById(long ProjectId);
	}

	public class ProjectsService : IProjectsService
	{
		private IEgyVisionRepository<Projects> _ProjectsRepo = null;
		public ProjectsService()
		{
			_ProjectsRepo = new EgyVisionRepository<Projects>();
		}

		public bool Insert(ProjectsVM vm)
		{
			Projects model = new Projects();
			copyToModel(vm,model);
			bool success = _ProjectsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}  
		public ProjectsVM InsertAndReturn(ProjectsVM vm)
		{
			Projects model = new Projects();
			copyToModel(vm,model);
			var insertedModel = _ProjectsRepo.InsertAndReturn(model);
			copyToVM(insertedModel, vm);
			return vm;
		}

		public bool Update(ProjectsVM vm)
		{
			Projects model = _ProjectsRepo.GetById(vm.ProjectId);
			copyToModel(vm,model);
			return _ProjectsRepo.Update(model);
		}

		public bool Delete(ProjectsVM vm)
		{
			Projects model = _ProjectsRepo.GetById(vm.ProjectId);
			return _ProjectsRepo.Delete(model);
		}

		public List<ProjectsVM> Search(ProjectsVM model)
		{
			List<ProjectsVM> returned = new List<ProjectsVM>();
			var predicate = PredicateBuilder.New<Projects>(true);

            if (model.ProjectId > 0)
            {
                predicate = predicate.And(p => p.ProjectId == model.ProjectId);
            }
            if (!String.IsNullOrEmpty(model.ProjectTitleAr))
            {
                predicate = predicate.And(p => p.ProjectTitleAr == model.ProjectTitleAr);
            }
            if (!String.IsNullOrEmpty(model.ProjectTitleEn))
            {
                predicate = predicate.And(p => p.ProjectTitleEn == model.ProjectTitleEn);
            }
            if (!String.IsNullOrEmpty(model.ContentAr))
            {
                predicate = predicate.And(p => p.ContentAr == model.ContentAr);
            }
            if (!String.IsNullOrEmpty(model.ContentEn))
            {
                predicate = predicate.And(p => p.ContentEn == model.ContentEn);
            }

            IQueryable<Projects> query = _ProjectsRepo.Table.AsExpandable().Where(predicate);

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

			foreach (Projects record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					ProjectsVM vm = new ProjectsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public ProjectsVM GetById(long ProjectId)
		{
			Projects model = _ProjectsRepo.GetById(ProjectId);
			ProjectsVM vm = new ProjectsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(ProjectsVM src, Projects dest)
		{
			dest.ProjectId = src.ProjectId;
			if (!String.IsNullOrEmpty(src.ProjectTitleAr))
				dest.ProjectTitleAr = src.ProjectTitleAr;
			if (!String.IsNullOrEmpty(src.ProjectTitleEn))
				dest.ProjectTitleEn = src.ProjectTitleEn;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
		}

		private void copyToVM(Projects src, ProjectsVM dest)
		{
			dest.ProjectId = src.ProjectId;
			if (!String.IsNullOrEmpty(src.ProjectTitleAr))
				dest.ProjectTitleAr = src.ProjectTitleAr;
			if (!String.IsNullOrEmpty(src.ProjectTitleEn))
				dest.ProjectTitleEn = src.ProjectTitleEn;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
		}

	}
}
