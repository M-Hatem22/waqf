using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface ISettingsSmsService
	{
		List<SettingsSmsVM> Search(SettingsSmsVM model);
		bool Insert(SettingsSmsVM vm);
		bool Update(SettingsSmsVM vm);
		bool Delete(SettingsSmsVM vm);
		SettingsSmsVM GetById(int Id);
	}

	public class SettingsSmsService : ISettingsSmsService
	{
		private IEgyVisionRepository<SettingsSms> _SettingsSmsRepo = null;
		public SettingsSmsService()
		{
			_SettingsSmsRepo = new EgyVisionRepository<SettingsSms>();
		}

		public bool Insert(SettingsSmsVM vm)
		{
			SettingsSms model = new SettingsSms();
			copyToModel(vm,model);
			bool success = _SettingsSmsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(SettingsSmsVM vm)
		{
			SettingsSms model = _SettingsSmsRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _SettingsSmsRepo.Update(model);
		}

		public bool Delete(SettingsSmsVM vm)
		{
			SettingsSms model = _SettingsSmsRepo.GetById(vm.Id);
			return _SettingsSmsRepo.Delete(model);
		}

		public List<SettingsSmsVM> Search(SettingsSmsVM model)
		{
			List<SettingsSmsVM> returned = new List<SettingsSmsVM>();
			var predicate = PredicateBuilder.New<SettingsSms>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (!String.IsNullOrEmpty(model.UserName))
			//{
				//predicate = predicate.And(p => p.UserName == model.UserName);
			//}
			//if (!String.IsNullOrEmpty(model.Password))
			//{
				//predicate = predicate.And(p => p.Password == model.Password);
			//}
				//predicate = predicate.And(p => p.EnableSms == model.EnableSms);
				//predicate = predicate.And(p => p.BalanceLastCheckDate == model.BalanceLastCheckDate);
			//if (model.CheckedBalance > 0)
			//{
				//predicate = predicate.And(p => p.CheckedBalance == model.CheckedBalance);
			//}
			//if (model.MinmumBalanceToAlert > 0)
			//{
				//predicate = predicate.And(p => p.MinmumBalanceToAlert == model.MinmumBalanceToAlert);
			//}
			//if (!String.IsNullOrEmpty(model.ClientMobile))
			//{
				//predicate = predicate.And(p => p.ClientMobile == model.ClientMobile);
			//}
			//if (!String.IsNullOrEmpty(model.Sender))
			//{
				//predicate = predicate.And(p => p.Sender == model.Sender);
			//}

			IQueryable<SettingsSms> query = _SettingsSmsRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "UserName" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserName).Where(predicate);
			else if (model.OrderBy == "UserName" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserName).Where(predicate);
			else if (model.OrderBy == "Password" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Password).Where(predicate);
			else if (model.OrderBy == "Password" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Password).Where(predicate);
			else if (model.OrderBy == "EnableSms" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.EnableSms).Where(predicate);
			else if (model.OrderBy == "EnableSms" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.EnableSms).Where(predicate);
			else if (model.OrderBy == "BalanceLastCheckDate" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.BalanceLastCheckDate).Where(predicate);
			else if (model.OrderBy == "BalanceLastCheckDate" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.BalanceLastCheckDate).Where(predicate);
			else if (model.OrderBy == "CheckedBalance" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.CheckedBalance).Where(predicate);
			else if (model.OrderBy == "CheckedBalance" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.CheckedBalance).Where(predicate);
			else if (model.OrderBy == "MinmumBalanceToAlert" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.MinmumBalanceToAlert).Where(predicate);
			else if (model.OrderBy == "MinmumBalanceToAlert" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.MinmumBalanceToAlert).Where(predicate);
			else if (model.OrderBy == "ClientMobile" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ClientMobile).Where(predicate);
			else if (model.OrderBy == "ClientMobile" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ClientMobile).Where(predicate);
			else if (model.OrderBy == "Sender" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Sender).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Sender).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (SettingsSms record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					SettingsSmsVM vm = new SettingsSmsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public SettingsSmsVM GetById(int Id)
		{
			SettingsSms model = _SettingsSmsRepo.GetById(Id);
			SettingsSmsVM vm = new SettingsSmsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(SettingsSmsVM src, SettingsSms dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.UserName))
				dest.UserName = src.UserName;
			if (!String.IsNullOrEmpty(src.Password))
				dest.Password = src.Password;
			dest.EnableSms = src.EnableSms;
			if (src.BalanceLastCheckDate != null && src.BalanceLastCheckDate != DateTime.MinValue)
				dest.BalanceLastCheckDate = src.BalanceLastCheckDate;
			if (src.CheckedBalance > 0)
				dest.CheckedBalance = src.CheckedBalance;
			if (src.MinmumBalanceToAlert > 0)
				dest.MinmumBalanceToAlert = src.MinmumBalanceToAlert;
			if (!String.IsNullOrEmpty(src.ClientMobile))
				dest.ClientMobile = src.ClientMobile;
			if (!String.IsNullOrEmpty(src.Sender))
				dest.Sender = src.Sender;
		}

		private void copyToVM(SettingsSms src, SettingsSmsVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.UserName))
				dest.UserName = src.UserName;
			if (!String.IsNullOrEmpty(src.Password))
				dest.Password = src.Password;
			dest.EnableSms = src.EnableSms;
			if (src.BalanceLastCheckDate != null && src.BalanceLastCheckDate != DateTime.MinValue)
				dest.BalanceLastCheckDate = src.BalanceLastCheckDate;
			if (src.CheckedBalance > 0)
				dest.CheckedBalance = src.CheckedBalance;
			if (src.MinmumBalanceToAlert > 0)
				dest.MinmumBalanceToAlert = src.MinmumBalanceToAlert;
			if (!String.IsNullOrEmpty(src.ClientMobile))
				dest.ClientMobile = src.ClientMobile;
			if (!String.IsNullOrEmpty(src.Sender))
				dest.Sender = src.Sender;
		}

	}
}
