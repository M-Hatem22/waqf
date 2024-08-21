using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILKNotificationsActionsService
	{
		List<LKNotificationsActionsVM> Search(LKNotificationsActionsVM model);
		bool Insert(LKNotificationsActionsVM vm);
		bool Update(LKNotificationsActionsVM vm);
		bool Delete(LKNotificationsActionsVM vm);
		LKNotificationsActionsVM GetById(int NotificationActionId);
	}

	public class LKNotificationsActionsService : ILKNotificationsActionsService
	{
		private IEgyVisionRepository<LKNotificationsActions> _LKNotificationsActionsRepo = null;
		public LKNotificationsActionsService()
		{
			_LKNotificationsActionsRepo = new EgyVisionRepository<LKNotificationsActions>();
		}

		public bool Insert(LKNotificationsActionsVM vm)
		{
			LKNotificationsActions model = new LKNotificationsActions();
			copyToModel(vm,model);
			bool success = _LKNotificationsActionsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LKNotificationsActionsVM vm)
		{
			LKNotificationsActions model = _LKNotificationsActionsRepo.GetById(vm.NotificationActionId);
			copyToModel(vm,model);
			return _LKNotificationsActionsRepo.Update(model);
		}

		public bool Delete(LKNotificationsActionsVM vm)
		{
			LKNotificationsActions model = _LKNotificationsActionsRepo.GetById(vm.NotificationActionId);
			return _LKNotificationsActionsRepo.Delete(model);
		}

		public List<LKNotificationsActionsVM> Search(LKNotificationsActionsVM model)
		{
			List<LKNotificationsActionsVM> returned = new List<LKNotificationsActionsVM>();
			var predicate = PredicateBuilder.New<LKNotificationsActions>(true);

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
			//if (!String.IsNullOrEmpty(model.TargetUsers))
			//{
				//predicate = predicate.And(p => p.TargetUsers == model.TargetUsers);
			//}

			IQueryable<LKNotificationsActions> query = _LKNotificationsActionsRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "TargetUsers" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TargetUsers).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.TargetUsers).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LKNotificationsActions record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LKNotificationsActionsVM vm = new LKNotificationsActionsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LKNotificationsActionsVM GetById(int NotificationActionId)
		{
			LKNotificationsActions model = _LKNotificationsActionsRepo.GetById(NotificationActionId);
			LKNotificationsActionsVM vm = new LKNotificationsActionsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LKNotificationsActionsVM src, LKNotificationsActions dest)
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
			if (!String.IsNullOrEmpty(src.Action))
				dest.Action = src.Action;
			if (!String.IsNullOrEmpty(src.ActionAr))
				dest.ActionAr = src.ActionAr;
			if (!String.IsNullOrEmpty(src.ActionCode))
				dest.ActionCode = src.ActionCode;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (!String.IsNullOrEmpty(src.TargetUsers))
				dest.TargetUsers = src.TargetUsers;
		}

		private void copyToVM(LKNotificationsActions src, LKNotificationsActionsVM dest)
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
			if (!String.IsNullOrEmpty(src.Action))
				dest.Action = src.Action;
			if (!String.IsNullOrEmpty(src.ActionAr))
				dest.ActionAr = src.ActionAr;
			if (!String.IsNullOrEmpty(src.ActionCode))
				dest.ActionCode = src.ActionCode;
			if (src.Deleted != null && src.Deleted != DateTime.MinValue)
				dest.Deleted = src.Deleted;
			if (!String.IsNullOrEmpty(src.TargetUsers))
				dest.TargetUsers = src.TargetUsers;
		}

	}
}
