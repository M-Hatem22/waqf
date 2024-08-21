using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface INotificationsCatActTempService
	{
		List<NotificationsCatActTempVM> Search(NotificationsCatActTempVM model);
		bool Insert(NotificationsCatActTempVM vm);
		bool Update(NotificationsCatActTempVM vm);
		bool Delete(NotificationsCatActTempVM vm);
		NotificationsCatActTempVM GetById(int Id);
	}

	public class NotificationsCatActTempService : INotificationsCatActTempService
	{
		private IEgyVisionRepository<NotificationsCatActTemp> _NotificationsCatActTempRepo = null;
		public NotificationsCatActTempService()
		{
			_NotificationsCatActTempRepo = new EgyVisionRepository<NotificationsCatActTemp>();
		}

		public bool Insert(NotificationsCatActTempVM vm)
		{
			NotificationsCatActTemp model = new NotificationsCatActTemp();
			copyToModel(vm,model);
			bool success = _NotificationsCatActTempRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(NotificationsCatActTempVM vm)
		{
			NotificationsCatActTemp model = _NotificationsCatActTempRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _NotificationsCatActTempRepo.Update(model);
		}

		public bool Delete(NotificationsCatActTempVM vm)
		{
			NotificationsCatActTemp model = _NotificationsCatActTempRepo.GetById(vm.Id);
			return _NotificationsCatActTempRepo.Delete(model);
		}

		public List<NotificationsCatActTempVM> Search(NotificationsCatActTempVM model)
		{
			List<NotificationsCatActTempVM> returned = new List<NotificationsCatActTempVM>();
			var predicate = PredicateBuilder.New<NotificationsCatActTemp>(true);

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

			IQueryable<NotificationsCatActTemp> query = _NotificationsCatActTempRepo.Table.AsExpandable().Where(predicate);

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
			else
				query = query.AsExpandable().OrderBy(x => x.ForNotification).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (NotificationsCatActTemp record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					NotificationsCatActTempVM vm = new NotificationsCatActTempVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public NotificationsCatActTempVM GetById(int Id)
		{
			NotificationsCatActTemp model = _NotificationsCatActTempRepo.GetById(Id);
			NotificationsCatActTempVM vm = new NotificationsCatActTempVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(NotificationsCatActTempVM src, NotificationsCatActTemp dest)
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
		}

		private void copyToVM(NotificationsCatActTemp src, NotificationsCatActTempVM dest)
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
		}

	}
}
