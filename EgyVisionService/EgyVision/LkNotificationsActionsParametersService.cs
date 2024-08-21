using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ILkNotificationsActionsParametersService
	{
		List<LkNotificationsActionsParametersVM> Search(LkNotificationsActionsParametersVM model);
		bool Insert(LkNotificationsActionsParametersVM vm);
		bool Update(LkNotificationsActionsParametersVM vm);
		bool Delete(LkNotificationsActionsParametersVM vm);
		LkNotificationsActionsParametersVM GetById(int ParameterId);
	}

	public class LkNotificationsActionsParametersService : ILkNotificationsActionsParametersService
	{
		private IEgyVisionRepository<LkNotificationsActionsParameters> _LkNotificationsActionsParametersRepo = null;
		public LkNotificationsActionsParametersService()
		{
			_LkNotificationsActionsParametersRepo = new EgyVisionRepository<LkNotificationsActionsParameters>();
		}

		public bool Insert(LkNotificationsActionsParametersVM vm)
		{
			LkNotificationsActionsParameters model = new LkNotificationsActionsParameters();
			copyToModel(vm,model);
			bool success = _LkNotificationsActionsParametersRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(LkNotificationsActionsParametersVM vm)
		{
			LkNotificationsActionsParameters model = _LkNotificationsActionsParametersRepo.GetById(vm.ParameterId);
			copyToModel(vm,model);
			return _LkNotificationsActionsParametersRepo.Update(model);
		}

		public bool Delete(LkNotificationsActionsParametersVM vm)
		{
			LkNotificationsActionsParameters model = _LkNotificationsActionsParametersRepo.GetById(vm.ParameterId);
			return _LkNotificationsActionsParametersRepo.Delete(model);
		}

		public List<LkNotificationsActionsParametersVM> Search(LkNotificationsActionsParametersVM model)
		{
			List<LkNotificationsActionsParametersVM> returned = new List<LkNotificationsActionsParametersVM>();
			var predicate = PredicateBuilder.New<LkNotificationsActionsParameters>(true);

			//if (model.ParameterId > 0)
			//{
				//predicate = predicate.And(p => p.ParameterId == model.ParameterId);
			//}
			//if (model.NotificationActionId > 0)
			//{
				//predicate = predicate.And(p => p.NotificationActionId == model.NotificationActionId);
			//}
			//if (!String.IsNullOrEmpty(model.ParameterName))
			//{
				//predicate = predicate.And(p => p.ParameterName == model.ParameterName);
			//}
			//if (!String.IsNullOrEmpty(model.ParameterNameAr))
			//{
				//predicate = predicate.And(p => p.ParameterNameAr == model.ParameterNameAr);
			//}

			IQueryable<LkNotificationsActionsParameters> query = _LkNotificationsActionsParametersRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "ParameterId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "ParameterId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ParameterId).Where(predicate);
			else if (model.OrderBy == "ParameterId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ParameterId).Where(predicate);
			else if (model.OrderBy == "NotificationActionId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationActionId).Where(predicate);
			else if (model.OrderBy == "NotificationActionId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationActionId).Where(predicate);
			else if (model.OrderBy == "ParameterName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ParameterName).Where(predicate);
			else if (model.OrderBy == "ParameterName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ParameterName).Where(predicate);
			else if (model.OrderBy == "ParameterNameAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ParameterNameAr).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.ParameterNameAr).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (LkNotificationsActionsParameters record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					LkNotificationsActionsParametersVM vm = new LkNotificationsActionsParametersVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public LkNotificationsActionsParametersVM GetById(int ParameterId)
		{
			LkNotificationsActionsParameters model = _LkNotificationsActionsParametersRepo.GetById(ParameterId);
			LkNotificationsActionsParametersVM vm = new LkNotificationsActionsParametersVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(LkNotificationsActionsParametersVM src, LkNotificationsActionsParameters dest)
		{
			if (src.ParameterId > 0)
				dest.ParameterId = src.ParameterId;
			if (src.NotificationActionId > 0)
				dest.NotificationActionId = src.NotificationActionId;
			if (!String.IsNullOrEmpty(src.ParameterName))
				dest.ParameterName = src.ParameterName;
			if (!String.IsNullOrEmpty(src.ParameterNameAr))
				dest.ParameterNameAr = src.ParameterNameAr;
		}

		private void copyToVM(LkNotificationsActionsParameters src, LkNotificationsActionsParametersVM dest)
		{
			if (src.ParameterId > 0)
				dest.ParameterId = src.ParameterId;
			if (src.NotificationActionId > 0)
				dest.NotificationActionId = src.NotificationActionId;
			if (!String.IsNullOrEmpty(src.ParameterName))
				dest.ParameterName = src.ParameterName;
			if (!String.IsNullOrEmpty(src.ParameterNameAr))
				dest.ParameterNameAr = src.ParameterNameAr;
		}

	}
}
