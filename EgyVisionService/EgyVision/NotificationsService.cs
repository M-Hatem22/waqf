using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface INotificationsService
	{
		List<NotificationsVM> Search(NotificationsVM model);
		bool Insert(NotificationsVM vm);
		bool Update(NotificationsVM vm);
		bool Delete(NotificationsVM vm);
		NotificationsVM GetById(long NotificationId);
	}

	public class NotificationsService : INotificationsService
	{
		private IEgyVisionRepository<Notifications> _NotificationsRepo = null;
		public NotificationsService()
		{
			_NotificationsRepo = new EgyVisionRepository<Notifications>();
		}

		public bool Insert(NotificationsVM vm)
		{
			Notifications model = new Notifications();
			copyToModel(vm,model);
			bool success = _NotificationsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(NotificationsVM vm)
		{
			Notifications model = _NotificationsRepo.GetById(vm.NotificationId);
			copyToModel(vm,model);
			return _NotificationsRepo.Update(model);
		}

		public bool Delete(NotificationsVM vm)
		{
			Notifications model = _NotificationsRepo.GetById(vm.NotificationId);
			return _NotificationsRepo.Delete(model);
		}

		public List<NotificationsVM> Search(NotificationsVM model)
		{
			List<NotificationsVM> returned = new List<NotificationsVM>();
			var predicate = PredicateBuilder.New<Notifications>(true);

			//if (model.NotificationId > 0)
			//{
				//predicate = predicate.And(p => p.NotificationId == model.NotificationId);
			//}
			//if (!String.IsNullOrEmpty(model.NotificationText))
			//{
				//predicate = predicate.And(p => p.NotificationText == model.NotificationText);
			//}
				//predicate = predicate.And(p => p.NotificationDate == model.NotificationDate);
			//if (!String.IsNullOrEmpty(model.NotificationDateHJ))
			//{
				//predicate = predicate.And(p => p.NotificationDateHJ == model.NotificationDateHJ);
			//}
			//if (!String.IsNullOrEmpty(model.AddedUserId))
			//{
				//predicate = predicate.And(p => p.AddedUserId == model.AddedUserId);
			//}
			//if (!String.IsNullOrEmpty(model.TargetUserId))
			//{
				//predicate = predicate.And(p => p.TargetUserId == model.TargetUserId);
			//}
			//if (model.TargetRequestId > 0)
			//{
				//predicate = predicate.And(p => p.TargetRequestId == model.TargetRequestId);
			//}
			//if (!String.IsNullOrEmpty(model.TargetRoles))
			//{
				//predicate = predicate.And(p => p.TargetRoles == model.TargetRoles);
			//}
			//if (!String.IsNullOrEmpty(model.TargetMobileNumber))
			//{
				//predicate = predicate.And(p => p.TargetMobileNumber == model.TargetMobileNumber);
			//}
			//if (!String.IsNullOrEmpty(model.TargetEmail))
			//{
				//predicate = predicate.And(p => p.TargetEmail == model.TargetEmail);
			//}
			//if (model.NotificationActionId > 0)
			//{
				//predicate = predicate.And(p => p.NotificationActionId == model.NotificationActionId);
			//}
			//if (model.NotificationTemplateId > 0)
			//{
				//predicate = predicate.And(p => p.NotificationTemplateId == model.NotificationTemplateId);
			//}
			//if (!String.IsNullOrEmpty(model.NotificationCode))
			//{
				//predicate = predicate.And(p => p.NotificationCode == model.NotificationCode);
			//}
				//predicate = predicate.And(p => p.IsRead == model.IsRead);
				//predicate = predicate.And(p => p.IsMailSent == model.IsMailSent);
				//predicate = predicate.And(p => p.IsMobileSent == model.IsMobileSent);
				//predicate = predicate.And(p => p.ForSms == model.ForSms);
				//predicate = predicate.And(p => p.ForEmail == model.ForEmail);
				//predicate = predicate.And(p => p.ForNotification == model.ForNotification);
				//predicate = predicate.And(p => p.SmsProcessingDate == model.SmsProcessingDate);

			IQueryable<Notifications> query = _NotificationsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "NotificationId";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "NotificationId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationId).Where(predicate);
			else if (model.OrderBy == "NotificationId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationId).Where(predicate);
			else if (model.OrderBy == "NotificationText" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationText).Where(predicate);
			else if (model.OrderBy == "NotificationText" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationText).Where(predicate);
			else if (model.OrderBy == "NotificationDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationDate).Where(predicate);
			else if (model.OrderBy == "NotificationDate" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationDate).Where(predicate);
			else if (model.OrderBy == "NotificationDateHJ" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationDateHJ).Where(predicate);
			else if (model.OrderBy == "NotificationDateHJ" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationDateHJ).Where(predicate);
			else if (model.OrderBy == "AddedUserId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AddedUserId).Where(predicate);
			else if (model.OrderBy == "AddedUserId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AddedUserId).Where(predicate);
			else if (model.OrderBy == "TargetUserId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TargetUserId).Where(predicate);
			else if (model.OrderBy == "TargetUserId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TargetUserId).Where(predicate);
			else if (model.OrderBy == "TargetRequestId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TargetRequestId).Where(predicate);
			else if (model.OrderBy == "TargetRequestId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TargetRequestId).Where(predicate);
			else if (model.OrderBy == "TargetRoles" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TargetRoles).Where(predicate);
			else if (model.OrderBy == "TargetRoles" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TargetRoles).Where(predicate);
			else if (model.OrderBy == "TargetMobileNumber" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TargetMobileNumber).Where(predicate);
			else if (model.OrderBy == "TargetMobileNumber" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TargetMobileNumber).Where(predicate);
			else if (model.OrderBy == "TargetEmail" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TargetEmail).Where(predicate);
			else if (model.OrderBy == "TargetEmail" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TargetEmail).Where(predicate);
			else if (model.OrderBy == "NotificationActionId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationActionId).Where(predicate);
			else if (model.OrderBy == "NotificationActionId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationActionId).Where(predicate);
			else if (model.OrderBy == "NotificationTemplateId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationTemplateId).Where(predicate);
			else if (model.OrderBy == "NotificationTemplateId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationTemplateId).Where(predicate);
			else if (model.OrderBy == "NotificationCode" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.NotificationCode).Where(predicate);
			else if (model.OrderBy == "NotificationCode" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.NotificationCode).Where(predicate);
			else if (model.OrderBy == "IsRead" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.IsRead).Where(predicate);
			else if (model.OrderBy == "IsRead" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.IsRead).Where(predicate);
			else if (model.OrderBy == "IsMailSent" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.IsMailSent).Where(predicate);
			else if (model.OrderBy == "IsMailSent" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.IsMailSent).Where(predicate);
			else if (model.OrderBy == "IsMobileSent" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.IsMobileSent).Where(predicate);
			else if (model.OrderBy == "IsMobileSent" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.IsMobileSent).Where(predicate);
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
			else if (model.OrderBy == "SmsProcessingDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.SmsProcessingDate).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.SmsProcessingDate).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (Notifications record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					NotificationsVM vm = new NotificationsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public NotificationsVM GetById(long NotificationId)
		{
			Notifications model = _NotificationsRepo.GetById(NotificationId);
			NotificationsVM vm = new NotificationsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(NotificationsVM src, Notifications dest)
		{
			dest.NotificationId = src.NotificationId;
			if (!String.IsNullOrEmpty(src.NotificationText))
				dest.NotificationText = src.NotificationText;
			if (src.NotificationDate != null && src.NotificationDate != DateTime.MinValue)
				dest.NotificationDate = src.NotificationDate;
			if (!String.IsNullOrEmpty(src.NotificationDateHJ))
				dest.NotificationDateHJ = src.NotificationDateHJ;
			if (!String.IsNullOrEmpty(src.AddedUserId))
				dest.AddedUserId = src.AddedUserId;
			if (!String.IsNullOrEmpty(src.TargetUserId))
				dest.TargetUserId = src.TargetUserId;
			dest.TargetRequestId = src.TargetRequestId;
			if (!String.IsNullOrEmpty(src.TargetRoles))
				dest.TargetRoles = src.TargetRoles;
			if (!String.IsNullOrEmpty(src.TargetMobileNumber))
				dest.TargetMobileNumber = src.TargetMobileNumber;
			if (!String.IsNullOrEmpty(src.TargetEmail))
				dest.TargetEmail = src.TargetEmail;
			if (src.NotificationActionId > 0)
				dest.NotificationActionId = src.NotificationActionId;
			if (src.NotificationTemplateId > 0)
				dest.NotificationTemplateId = src.NotificationTemplateId;
			if (!String.IsNullOrEmpty(src.NotificationCode))
				dest.NotificationCode = src.NotificationCode;
			dest.IsRead = src.IsRead;
			dest.IsMailSent = src.IsMailSent;
			dest.IsMobileSent = src.IsMobileSent;
			dest.ForSms = src.ForSms;
			dest.ForEmail = src.ForEmail;
			dest.ForNotification = src.ForNotification;
			if (src.SmsProcessingDate != null && src.SmsProcessingDate != DateTime.MinValue)
				dest.SmsProcessingDate = src.SmsProcessingDate;
		}

		private void copyToVM(Notifications src, NotificationsVM dest)
		{
			dest.NotificationId = src.NotificationId;
			if (!String.IsNullOrEmpty(src.NotificationText))
				dest.NotificationText = src.NotificationText;
			if (src.NotificationDate != null && src.NotificationDate != DateTime.MinValue)
				dest.NotificationDate = src.NotificationDate;
			if (!String.IsNullOrEmpty(src.NotificationDateHJ))
				dest.NotificationDateHJ = src.NotificationDateHJ;
			if (!String.IsNullOrEmpty(src.AddedUserId))
				dest.AddedUserId = src.AddedUserId;
			if (!String.IsNullOrEmpty(src.TargetUserId))
				dest.TargetUserId = src.TargetUserId;
			dest.TargetRequestId = src.TargetRequestId;
			if (!String.IsNullOrEmpty(src.TargetRoles))
				dest.TargetRoles = src.TargetRoles;
			if (!String.IsNullOrEmpty(src.TargetMobileNumber))
				dest.TargetMobileNumber = src.TargetMobileNumber;
			if (!String.IsNullOrEmpty(src.TargetEmail))
				dest.TargetEmail = src.TargetEmail;
			if (src.NotificationActionId > 0)
				dest.NotificationActionId = src.NotificationActionId;
			if (src.NotificationTemplateId > 0)
				dest.NotificationTemplateId = src.NotificationTemplateId;
			if (!String.IsNullOrEmpty(src.NotificationCode))
				dest.NotificationCode = src.NotificationCode;
			dest.IsRead = src.IsRead;
			dest.IsMailSent = src.IsMailSent;
			dest.IsMobileSent = src.IsMobileSent;
			dest.ForSms = src.ForSms;
			dest.ForEmail = src.ForEmail;
			dest.ForNotification = src.ForNotification;
			if (src.SmsProcessingDate != null && src.SmsProcessingDate != DateTime.MinValue)
				dest.SmsProcessingDate = src.SmsProcessingDate;
		}

	}
}
