using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKNotificationsTemplatesService
	{
		List<LKNotificationsTemplatesVM> Search(LKNotificationsTemplatesVM model);
		bool Insert(LKNotificationsTemplatesVM vm);
		bool Update(LKNotificationsTemplatesVM vm);
		bool Delete(LKNotificationsTemplatesVM vm);
		LKNotificationsTemplatesVM GetById(int TemplateId);
	}

	public class LKNotificationsTemplatesService : ILKNotificationsTemplatesService
	{
		private IEgyVisionRepository<LKNotificationsTemplates> _LKNotificationsTemplatesRepo = null;
		public LKNotificationsTemplatesService()
		{
			_LKNotificationsTemplatesRepo = new EgyVisionRepository<LKNotificationsTemplates>();
		}

		public bool Insert(LKNotificationsTemplatesVM vm)
		{
			LKNotificationsTemplates model = new LKNotificationsTemplates();
			copyToModel(vm,model);
			bool success = _LKNotificationsTemplatesRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKNotificationsTemplatesVM vm)
		{
			LKNotificationsTemplates model = _LKNotificationsTemplatesRepo.GetById(vm.TemplateId);
			copyToModel(vm,model);
			return _LKNotificationsTemplatesRepo.Update(model);
		}

		public bool Delete(LKNotificationsTemplatesVM vm)
		{
			LKNotificationsTemplates model = _LKNotificationsTemplatesRepo.GetById(vm.TemplateId);
			return _LKNotificationsTemplatesRepo.Delete(model);
		}

		public List<LKNotificationsTemplatesVM> Search(LKNotificationsTemplatesVM model)
		{
			List<LKNotificationsTemplatesVM> returned = new List<LKNotificationsTemplatesVM>();
			var predicate = PredicateBuilder.New<LKNotificationsTemplates>(true);

			//if (model.TemplateId > 0)
			//{
				//predicate = predicate.And(p => p.TemplateId == model.TemplateId);
			//}
			//if (!String.IsNullOrEmpty(model.TemplateTXTAr))
			//{
				//predicate = predicate.And(p => p.TemplateTXTAr == model.TemplateTXTAr);
			//}
			//if (!String.IsNullOrEmpty(model.TemplateTXTEn))
			//{
				//predicate = predicate.And(p => p.TemplateTXTEn == model.TemplateTXTEn);
			//}
				//predicate = predicate.And(p => p.IsActive == model.IsActive);

			IQueryable<LKNotificationsTemplates> query = _LKNotificationsTemplatesRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "TemplateId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "TemplateId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TemplateId).Where(predicate);
			else if (model.OrderBy == "TemplateId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TemplateId).Where(predicate);
			else if (model.OrderBy == "TemplateTXTAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TemplateTXTAr).Where(predicate);
			else if (model.OrderBy == "TemplateTXTAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TemplateTXTAr).Where(predicate);
			else if (model.OrderBy == "TemplateTXTEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TemplateTXTEn).Where(predicate);
			else if (model.OrderBy == "TemplateTXTEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TemplateTXTEn).Where(predicate);
			else if (model.OrderBy == "IsActive" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.IsActive).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.IsActive).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKNotificationsTemplates record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKNotificationsTemplatesVM vm = new LKNotificationsTemplatesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKNotificationsTemplatesVM GetById(int TemplateId)
		{
			LKNotificationsTemplates model = _LKNotificationsTemplatesRepo.GetById(TemplateId);
			LKNotificationsTemplatesVM vm = new LKNotificationsTemplatesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKNotificationsTemplatesVM src, LKNotificationsTemplates dest)
		{
			if (src.TemplateId > 0)
				dest.TemplateId = src.TemplateId;
			if (!String.IsNullOrEmpty(src.TemplateTXTAr))
				dest.TemplateTXTAr = src.TemplateTXTAr;
			if (!String.IsNullOrEmpty(src.TemplateTXTEn))
				dest.TemplateTXTEn = src.TemplateTXTEn;
			dest.IsActive = src.IsActive;
		}

		private void copyToVM(LKNotificationsTemplates src, LKNotificationsTemplatesVM dest)
		{
			if (src.TemplateId > 0)
				dest.TemplateId = src.TemplateId;
			if (!String.IsNullOrEmpty(src.TemplateTXTAr))
				dest.TemplateTXTAr = src.TemplateTXTAr;
			if (!String.IsNullOrEmpty(src.TemplateTXTEn))
				dest.TemplateTXTEn = src.TemplateTXTEn;
			dest.IsActive = src.IsActive;
		}

	}
}
