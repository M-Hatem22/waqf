using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKNotificationsActionsViewService
	{
		List<LKNotificationsActionsViewVM> Search(LKNotificationsActionsViewVM model);
	}

	public class LKNotificationsActionsViewService : ILKNotificationsActionsViewService
	{
		private IEgyVisionRepository<LKNotificationsActionsView> _LKNotificationsActionsViewRepo = null;
		public LKNotificationsActionsViewService()
		{
			_LKNotificationsActionsViewRepo = new EgyVisionRepository<LKNotificationsActionsView>();
		}

		public List<LKNotificationsActionsViewVM> Search(LKNotificationsActionsViewVM model)
		{
			List<LKNotificationsActionsViewVM> returned = new List<LKNotificationsActionsViewVM>();
			var predicate = PredicateBuilder.New<LKNotificationsActionsView>(true);

			//if (model.NotificationActionId > 0)
			//{
				//predicate = predicate.And(p => p.NotificationActionId == model.NotificationActionId);
			//}
			//if (!String.IsNullOrEmpty(model.NotificationActionDescription))
			//{
				//predicate = predicate.And(p => p.NotificationActionDescription == model.NotificationActionDescription);
			//}
			//if (model.NotificationCategoryId > 0)
			//{
				//predicate = predicate.And(p => p.NotificationCategoryId == model.NotificationCategoryId);
			//}
			//if (!String.IsNullOrEmpty(model.System))
			//{
				//predicate = predicate.And(p => p.System == model.System);
			//}
			//if (!String.IsNullOrEmpty(model.SystemFrom))
			//{
				//predicate = predicate.And(p => p.SystemFrom == model.SystemFrom);
			//}
			//if (!String.IsNullOrEmpty(model.Controller))
			//{
				//predicate = predicate.And(p => p.Controller == model.Controller);
			//}
			//if (!String.IsNullOrEmpty(model.ControllerAr))
			//{
				//predicate = predicate.And(p => p.ControllerAr == model.ControllerAr);
			//}
			//if (!String.IsNullOrEmpty(model.TargetUsers))
			//{
				//predicate = predicate.And(p => p.TargetUsers == model.TargetUsers);
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
				//predicate = predicate.And(p => p.Deleted == model.Deleted);
			//if (!String.IsNullOrEmpty(model.CategoryTxt))
			//{
				//predicate = predicate.And(p => p.CategoryTxt == model.CategoryTxt);
			//}
			IQueryable<LKNotificationsActionsView> query = _LKNotificationsActionsViewRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "NotificationActionId";
					model.OrderByReversed = false;
			}

			if (model.OrderBy == "NotificationActionId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationActionId).Where(predicate);
			else if (model.OrderBy == "NotificationActionId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationActionId).Where(predicate);
			else if (model.OrderBy == "NotificationActionDescription" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationActionDescription).Where(predicate);
			else if (model.OrderBy == "NotificationActionDescription" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationActionDescription).Where(predicate);
			else if (model.OrderBy == "NotificationCategoryId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationCategoryId).Where(predicate);
			else if (model.OrderBy == "NotificationCategoryId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationCategoryId).Where(predicate);
			else if (model.OrderBy == "System" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.System).Where(predicate);
			else if (model.OrderBy == "System" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.System).Where(predicate);
			else if (model.OrderBy == "SystemFrom" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.SystemFrom).Where(predicate);
			else if (model.OrderBy == "SystemFrom" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.SystemFrom).Where(predicate);
			else if (model.OrderBy == "Controller" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Controller).Where(predicate);
			else if (model.OrderBy == "Controller" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Controller).Where(predicate);
			else if (model.OrderBy == "ControllerAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ControllerAr).Where(predicate);
			else if (model.OrderBy == "ControllerAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ControllerAr).Where(predicate);
			else if (model.OrderBy == "TargetUsers" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TargetUsers).Where(predicate);
			else if (model.OrderBy == "TargetUsers" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TargetUsers).Where(predicate);
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
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "Deleted" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Deleted).Where(predicate);
			else if (model.OrderBy == "CategoryTxt" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.CategoryTxt).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.CategoryTxt).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKNotificationsActionsView record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKNotificationsActionsViewVM vm = new LKNotificationsActionsViewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		private void copyToModel(LKNotificationsActionsViewVM src, LKNotificationsActionsView dest)
		{
			if (src.NotificationActionId > 0)
				dest.NotificationActionId = src.NotificationActionId;
			if (!String.IsNullOrEmpty(src.NotificationActionDescription))
				dest.NotificationActionDescription = src.NotificationActionDescription;
			if (src.NotificationCategoryId > 0)
				dest.NotificationCategoryId = src.NotificationCategoryId;
			if (!String.IsNullOrEmpty(src.System))
				dest.System = src.System;
			if (!String.IsNullOrEmpty(src.SystemFrom))
				dest.SystemFrom = src.SystemFrom;
			if (!String.IsNullOrEmpty(src.Controller))
				dest.Controller = src.Controller;
			if (!String.IsNullOrEmpty(src.ControllerAr))
				dest.ControllerAr = src.ControllerAr;
			if (!String.IsNullOrEmpty(src.TargetUsers))
				dest.TargetUsers = src.TargetUsers;
			if (!String.IsNullOrEmpty(src.Action))
				dest.Action = src.Action;
			if (!String.IsNullOrEmpty(src.ActionAr))
				dest.ActionAr = src.ActionAr;
			if (!String.IsNullOrEmpty(src.ActionCode))
				dest.ActionCode = src.ActionCode;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (!String.IsNullOrEmpty(src.CategoryTxt))
				dest.CategoryTxt = src.CategoryTxt;
		}

		private void copyToVM(LKNotificationsActionsView src, LKNotificationsActionsViewVM dest)
		{
			if (src.NotificationActionId > 0)
				dest.NotificationActionId = src.NotificationActionId;
			if (!String.IsNullOrEmpty(src.NotificationActionDescription))
				dest.NotificationActionDescription = src.NotificationActionDescription;
			if (src.NotificationCategoryId > 0)
				dest.NotificationCategoryId = src.NotificationCategoryId;
			if (!String.IsNullOrEmpty(src.System))
				dest.System = src.System;
			if (!String.IsNullOrEmpty(src.SystemFrom))
				dest.SystemFrom = src.SystemFrom;
			if (!String.IsNullOrEmpty(src.Controller))
				dest.Controller = src.Controller;
			if (!String.IsNullOrEmpty(src.ControllerAr))
				dest.ControllerAr = src.ControllerAr;
			if (!String.IsNullOrEmpty(src.TargetUsers))
				dest.TargetUsers = src.TargetUsers;
			if (!String.IsNullOrEmpty(src.Action))
				dest.Action = src.Action;
			if (!String.IsNullOrEmpty(src.ActionAr))
				dest.ActionAr = src.ActionAr;
			if (!String.IsNullOrEmpty(src.ActionCode))
				dest.ActionCode = src.ActionCode;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (!String.IsNullOrEmpty(src.CategoryTxt))
				dest.CategoryTxt = src.CategoryTxt;
		}

	}
}
