using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface INotificationsCatActTempViewService
	{
		List<NotificationsCatActTempViewVM> Search(NotificationsCatActTempViewVM model);
	}

	public class NotificationsCatActTempViewService : INotificationsCatActTempViewService
	{
		private IEgyVisionRepository<NotificationsCatActTempView> _NotificationsCatActTempViewRepo = null;
		public NotificationsCatActTempViewService()
		{
			_NotificationsCatActTempViewRepo = new EgyVisionRepository<NotificationsCatActTempView>();
		}

		public List<NotificationsCatActTempViewVM> Search(NotificationsCatActTempViewVM model)
		{
			List<NotificationsCatActTempViewVM> returned = new List<NotificationsCatActTempViewVM>();
			var predicate = PredicateBuilder.New<NotificationsCatActTempView>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (model.TemplateId > 0)
			//{
				//predicate = predicate.And(p => p.TemplateId == model.TemplateId);
			//}
			//if (model.NotificationCategoryId > 0)
			//{
				//predicate = predicate.And(p => p.NotificationCategoryId == model.NotificationCategoryId);
			//}
			//if (model.NotificationActionId > 0)
			//{
				//predicate = predicate.And(p => p.NotificationActionId == model.NotificationActionId);
			//}
				//predicate = predicate.And(p => p.Deleted == model.Deleted);
				//predicate = predicate.And(p => p.ForSms == model.ForSms);
				//predicate = predicate.And(p => p.ForEmail == model.ForEmail);
				//predicate = predicate.And(p => p.ForNotification == model.ForNotification);
			//if (!String.IsNullOrEmpty(model.TemplateTXTAr))
			//{
				//predicate = predicate.And(p => p.TemplateTXTAr == model.TemplateTXTAr);
			//}
			//if (!String.IsNullOrEmpty(model.TemplateTXTEn))
			//{
				//predicate = predicate.And(p => p.TemplateTXTEn == model.TemplateTXTEn);
			//}
			//if (!String.IsNullOrEmpty(model.NotificationActionDescription))
			//{
				//predicate = predicate.And(p => p.NotificationActionDescription == model.NotificationActionDescription);
			//}
			//if (!String.IsNullOrEmpty(model.System))
			//{
				//predicate = predicate.And(p => p.System == model.System);
			//}
			//if (!String.IsNullOrEmpty(model.Controller))
			//{
				//predicate = predicate.And(p => p.Controller == model.Controller);
			//}
			//if (!String.IsNullOrEmpty(model.ControllerAr))
			//{
				//predicate = predicate.And(p => p.ControllerAr == model.ControllerAr);
			//}
			//if (!String.IsNullOrEmpty(model.Action))
			//{
				//predicate = predicate.And(p => p.Action == model.Action);
			//}
			//if (!String.IsNullOrEmpty(model.ActionAr))
			//{
				//predicate = predicate.And(p => p.ActionAr == model.ActionAr);
			//}
			//if (!String.IsNullOrEmpty(model.ActionCode))
			//{
				//predicate = predicate.And(p => p.ActionCode == model.ActionCode);
			//}
			//if (!String.IsNullOrEmpty(model.CategoryTxt))
			//{
				//predicate = predicate.And(p => p.CategoryTxt == model.CategoryTxt);
			//}
			IQueryable<NotificationsCatActTempView> query = _NotificationsCatActTempViewRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "Id";
					model.OrderByReversed = false;
			}

			if (model.OrderBy == "Id" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Id).Where(predicate);
			else if (model.OrderBy == "Id" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Id).Where(predicate);
			else if (model.OrderBy == "TemplateId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TemplateId).Where(predicate);
			else if (model.OrderBy == "TemplateId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TemplateId).Where(predicate);
			else if (model.OrderBy == "NotificationCategoryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationCategoryId).Where(predicate);
			else if (model.OrderBy == "NotificationCategoryId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationCategoryId).Where(predicate);
			else if (model.OrderBy == "NotificationActionId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationActionId).Where(predicate);
			else if (model.OrderBy == "NotificationActionId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationActionId).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "ForSms" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ForSms).Where(predicate);
			else if (model.OrderBy == "ForSms" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ForSms).Where(predicate);
			else if (model.OrderBy == "ForEmail" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ForEmail).Where(predicate);
			else if (model.OrderBy == "ForEmail" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ForEmail).Where(predicate);
			else if (model.OrderBy == "ForNotification" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ForNotification).Where(predicate);
			else if (model.OrderBy == "ForNotification" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ForNotification).Where(predicate);
			else if (model.OrderBy == "TemplateTXTAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TemplateTXTAr).Where(predicate);
			else if (model.OrderBy == "TemplateTXTAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TemplateTXTAr).Where(predicate);
			else if (model.OrderBy == "TemplateTXTEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TemplateTXTEn).Where(predicate);
			else if (model.OrderBy == "TemplateTXTEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TemplateTXTEn).Where(predicate);
			else if (model.OrderBy == "NotificationActionDescription" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationActionDescription).Where(predicate);
			else if (model.OrderBy == "NotificationActionDescription" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationActionDescription).Where(predicate);
			else if (model.OrderBy == "System" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.System).Where(predicate);
			else if (model.OrderBy == "System" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.System).Where(predicate);
			else if (model.OrderBy == "Controller" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Controller).Where(predicate);
			else if (model.OrderBy == "Controller" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Controller).Where(predicate);
			else if (model.OrderBy == "ControllerAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ControllerAr).Where(predicate);
			else if (model.OrderBy == "ControllerAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ControllerAr).Where(predicate);
			else if (model.OrderBy == "Action" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Action).Where(predicate);
			else if (model.OrderBy == "Action" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Action).Where(predicate);
			else if (model.OrderBy == "ActionAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ActionAr).Where(predicate);
			else if (model.OrderBy == "ActionAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ActionAr).Where(predicate);
			else if (model.OrderBy == "ActionCode" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ActionCode).Where(predicate);
			else if (model.OrderBy == "ActionCode" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ActionCode).Where(predicate);
			else if (model.OrderBy == "CategoryTxt" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.CategoryTxt).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.CategoryTxt).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (NotificationsCatActTempView record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					NotificationsCatActTempViewVM vm = new NotificationsCatActTempViewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		private void copyToModel(NotificationsCatActTempViewVM src, NotificationsCatActTempView dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (src.TemplateId > 0)
				dest.TemplateId = src.TemplateId;
			if (src.NotificationCategoryId > 0)
				dest.NotificationCategoryId = src.NotificationCategoryId;
			if (src.NotificationActionId > 0)
				dest.NotificationActionId = src.NotificationActionId;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			dest.ForSms = src.ForSms;
			dest.ForEmail = src.ForEmail;
			dest.ForNotification = src.ForNotification;
			if (!String.IsNullOrEmpty(src.TemplateTXTAr))
				dest.TemplateTXTAr = src.TemplateTXTAr;
			if (!String.IsNullOrEmpty(src.TemplateTXTEn))
				dest.TemplateTXTEn = src.TemplateTXTEn;
			if (!String.IsNullOrEmpty(src.NotificationActionDescription))
				dest.NotificationActionDescription = src.NotificationActionDescription;
			if (!String.IsNullOrEmpty(src.System))
				dest.System = src.System;
			if (!String.IsNullOrEmpty(src.Controller))
				dest.Controller = src.Controller;
			if (!String.IsNullOrEmpty(src.ControllerAr))
				dest.ControllerAr = src.ControllerAr;
			if (!String.IsNullOrEmpty(src.Action))
				dest.Action = src.Action;
			if (!String.IsNullOrEmpty(src.ActionAr))
				dest.ActionAr = src.ActionAr;
			if (!String.IsNullOrEmpty(src.ActionCode))
				dest.ActionCode = src.ActionCode;
			if (!String.IsNullOrEmpty(src.CategoryTxt))
				dest.CategoryTxt = src.CategoryTxt;
		}

		private void copyToVM(NotificationsCatActTempView src, NotificationsCatActTempViewVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (src.TemplateId > 0)
				dest.TemplateId = src.TemplateId;
			if (src.NotificationCategoryId > 0)
				dest.NotificationCategoryId = src.NotificationCategoryId;
			if (src.NotificationActionId > 0)
				dest.NotificationActionId = src.NotificationActionId;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			dest.ForSms = src.ForSms;
			dest.ForEmail = src.ForEmail;
			dest.ForNotification = src.ForNotification;
			if (!String.IsNullOrEmpty(src.TemplateTXTAr))
				dest.TemplateTXTAr = src.TemplateTXTAr;
			if (!String.IsNullOrEmpty(src.TemplateTXTEn))
				dest.TemplateTXTEn = src.TemplateTXTEn;
			if (!String.IsNullOrEmpty(src.NotificationActionDescription))
				dest.NotificationActionDescription = src.NotificationActionDescription;
			if (!String.IsNullOrEmpty(src.System))
				dest.System = src.System;
			if (!String.IsNullOrEmpty(src.Controller))
				dest.Controller = src.Controller;
			if (!String.IsNullOrEmpty(src.ControllerAr))
				dest.ControllerAr = src.ControllerAr;
			if (!String.IsNullOrEmpty(src.Action))
				dest.Action = src.Action;
			if (!String.IsNullOrEmpty(src.ActionAr))
				dest.ActionAr = src.ActionAr;
			if (!String.IsNullOrEmpty(src.ActionCode))
				dest.ActionCode = src.ActionCode;
			if (!String.IsNullOrEmpty(src.CategoryTxt))
				dest.CategoryTxt = src.CategoryTxt;
		}

	}
}
