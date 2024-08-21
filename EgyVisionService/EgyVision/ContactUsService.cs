using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IContactUsService
	{
		List<ContactUsVM> Search(ContactUsVM model);
		bool Insert(ContactUsVM vm);
		bool Update(ContactUsVM vm);
		bool Delete(ContactUsVM vm);
		ContactUsVM GetById(int ID);
	}

	public class ContactUsService : IContactUsService
	{
		private IEgyVisionRepository<ContactUs> _ContactUsRepo = null;
		public ContactUsService()
		{
			_ContactUsRepo = new EgyVisionRepository<ContactUs>();
		}

		public bool Insert(ContactUsVM vm)
		{
			ContactUs model = new ContactUs();
			copyToModel(vm,model);
			bool success = _ContactUsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(ContactUsVM vm)
		{
			ContactUs model = _ContactUsRepo.GetById(vm.ID);
			copyToModel(vm,model);
			return _ContactUsRepo.Update(model);
		}

		public bool Delete(ContactUsVM vm)
		{
			ContactUs model = _ContactUsRepo.GetById(vm.ID);
			return _ContactUsRepo.Delete(model);
		}

		public List<ContactUsVM> Search(ContactUsVM model)
		{
			List<ContactUsVM> returned = new List<ContactUsVM>();
			var predicate = PredicateBuilder.New<ContactUs>(true);

			//if (model.ID > 0)
			//{
				//predicate = predicate.And(p => p.ID == model.ID);
			//}
			//if (!String.IsNullOrEmpty(model.PhoneNumber))
			//{
				//predicate = predicate.And(p => p.PhoneNumber == model.PhoneNumber);
			//}
			//if (!String.IsNullOrEmpty(model.MobileNumber))
			//{
				//predicate = predicate.And(p => p.MobileNumber == model.MobileNumber);
			//}
			//if (!String.IsNullOrEmpty(model.Email))
			//{
				//predicate = predicate.And(p => p.Email == model.Email);
			//}
			//if (!String.IsNullOrEmpty(model.TitleAr))
			//{
				//predicate = predicate.And(p => p.TitleAr == model.TitleAr);
			//}
			//if (!String.IsNullOrEmpty(model.TitleEn))
			//{
				//predicate = predicate.And(p => p.TitleEn == model.TitleEn);
			//}
			//if (!String.IsNullOrEmpty(model.GoogleURL))
			//{
				//predicate = predicate.And(p => p.GoogleURL == model.GoogleURL);
			//}
			//if (!String.IsNullOrEmpty(model.FacebookUrl))
			//{
				//predicate = predicate.And(p => p.FacebookUrl == model.FacebookUrl);
			//}
			//if (!String.IsNullOrEmpty(model.GooglePlus))
			//{
				//predicate = predicate.And(p => p.GooglePlus == model.GooglePlus);
			//}
			//if (!String.IsNullOrEmpty(model.InstagramUrl))
			//{
				//predicate = predicate.And(p => p.InstagramUrl == model.InstagramUrl);
			//}
			//if (!String.IsNullOrEmpty(model.YoutubeUrl))
			//{
				//predicate = predicate.And(p => p.YoutubeUrl == model.YoutubeUrl);
			//}
			//if (!String.IsNullOrEmpty(model.LinkedinUrl))
			//{
				//predicate = predicate.And(p => p.LinkedinUrl == model.LinkedinUrl);
			//}
			//if (!String.IsNullOrEmpty(model.TwitterUrl))
			//{
				//predicate = predicate.And(p => p.TwitterUrl == model.TwitterUrl);
			//}
			//if (model.Latitude > 0)
			//{
				//predicate = predicate.And(p => p.Latitude == model.Latitude);
			//}
			//if (model.Longitude > 0)
			//{
				//predicate = predicate.And(p => p.Longitude == model.Longitude);
			//}

			IQueryable<ContactUs> query = _ContactUsRepo.Table.AsExpandable().Where(predicate);
			IQueryable<ContactUs> queryCount = _ContactUsRepo.Table.AsExpandable().Where(predicate);

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
					model.OrderBy = "ID";
					model.OrderByReversed = false;
			}
			if (model.OrderBy == "ID" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ID).Where(predicate);
			else if (model.OrderBy == "ID" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ID).Where(predicate);
			else if (model.OrderBy == "PhoneNumber" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.PhoneNumber).Where(predicate);
			else if (model.OrderBy == "PhoneNumber" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.PhoneNumber).Where(predicate);
			else if (model.OrderBy == "MobileNumber" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.MobileNumber).Where(predicate);
			else if (model.OrderBy == "MobileNumber" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.MobileNumber).Where(predicate);
			else if (model.OrderBy == "Email" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Email).Where(predicate);
			else if (model.OrderBy == "Email" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Email).Where(predicate);
			else if (model.OrderBy == "TitleAr" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TitleAr).Where(predicate);
			else if (model.OrderBy == "TitleAr" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TitleAr).Where(predicate);
			else if (model.OrderBy == "TitleEn" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TitleEn).Where(predicate);
			else if (model.OrderBy == "TitleEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TitleEn).Where(predicate);
			else if (model.OrderBy == "GoogleURL" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GoogleURL).Where(predicate);
			else if (model.OrderBy == "GoogleURL" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GoogleURL).Where(predicate);
			else if (model.OrderBy == "FacebookUrl" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.FacebookUrl).Where(predicate);
			else if (model.OrderBy == "FacebookUrl" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.FacebookUrl).Where(predicate);
			else if (model.OrderBy == "GooglePlus" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.GooglePlus).Where(predicate);
			else if (model.OrderBy == "GooglePlus" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.GooglePlus).Where(predicate);
			else if (model.OrderBy == "InstagramUrl" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.InstagramUrl).Where(predicate);
			else if (model.OrderBy == "InstagramUrl" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.InstagramUrl).Where(predicate);
			else if (model.OrderBy == "YoutubeUrl" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.YoutubeUrl).Where(predicate);
			else if (model.OrderBy == "YoutubeUrl" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.YoutubeUrl).Where(predicate);
			else if (model.OrderBy == "LinkedinUrl" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LinkedinUrl).Where(predicate);
			else if (model.OrderBy == "LinkedinUrl" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LinkedinUrl).Where(predicate);
			else if (model.OrderBy == "TwitterUrl" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.TwitterUrl).Where(predicate);
			else if (model.OrderBy == "TwitterUrl" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.TwitterUrl).Where(predicate);
			else if (model.OrderBy == "Latitude" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Latitude).Where(predicate);
			else if (model.OrderBy == "Latitude" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.Latitude).Where(predicate);
			else if (model.OrderBy == "Longitude" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.Longitude).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Longitude).Where(predicate);
			model.TotalRecordCount = queryCount.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (ContactUs record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					ContactUsVM vm = new ContactUsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public ContactUsVM GetById(int ID)
		{
			ContactUs model = _ContactUsRepo.GetById(ID);
			ContactUsVM vm = new ContactUsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(ContactUsVM src, ContactUs dest)
		{
			if (src.ID > 0)
				dest.ID = src.ID;
			if (!String.IsNullOrEmpty(src.PhoneNumber))
				dest.PhoneNumber = src.PhoneNumber;
			if (!String.IsNullOrEmpty(src.MobileNumber))
				dest.MobileNumber = src.MobileNumber;
			if (!String.IsNullOrEmpty(src.Email))
				dest.Email = src.Email;
			if (!String.IsNullOrEmpty(src.TitleAr))
				dest.TitleAr = src.TitleAr;
			if (!String.IsNullOrEmpty(src.TitleEn))
				dest.TitleEn = src.TitleEn;
			if (!String.IsNullOrEmpty(src.GoogleURL))
				dest.GoogleURL = src.GoogleURL;
			if (!String.IsNullOrEmpty(src.FacebookUrl))
				dest.FacebookUrl = src.FacebookUrl;
			if (!String.IsNullOrEmpty(src.GooglePlus))
				dest.GooglePlus = src.GooglePlus;
			if (!String.IsNullOrEmpty(src.InstagramUrl))
				dest.InstagramUrl = src.InstagramUrl;
			if (!String.IsNullOrEmpty(src.YoutubeUrl))
				dest.YoutubeUrl = src.YoutubeUrl;
			if (!String.IsNullOrEmpty(src.LinkedinUrl))
				dest.LinkedinUrl = src.LinkedinUrl;
			if (!String.IsNullOrEmpty(src.TwitterUrl))
				dest.TwitterUrl = src.TwitterUrl;
			if (src.Latitude > 0)
				dest.Latitude = src.Latitude;
			if (src.Longitude > 0)
				dest.Longitude = src.Longitude;
		}

		private void copyToVM(ContactUs src, ContactUsVM dest)
		{
			if (src.ID > 0)
				dest.ID = src.ID;
			if (!String.IsNullOrEmpty(src.PhoneNumber))
				dest.PhoneNumber = src.PhoneNumber;
			if (!String.IsNullOrEmpty(src.MobileNumber))
				dest.MobileNumber = src.MobileNumber;
			if (!String.IsNullOrEmpty(src.Email))
				dest.Email = src.Email;
			if (!String.IsNullOrEmpty(src.TitleAr))
				dest.TitleAr = src.TitleAr;
			if (!String.IsNullOrEmpty(src.TitleEn))
				dest.TitleEn = src.TitleEn;
			if (!String.IsNullOrEmpty(src.GoogleURL))
				dest.GoogleURL = src.GoogleURL;
			if (!String.IsNullOrEmpty(src.FacebookUrl))
				dest.FacebookUrl = src.FacebookUrl;
			if (!String.IsNullOrEmpty(src.GooglePlus))
				dest.GooglePlus = src.GooglePlus;
			if (!String.IsNullOrEmpty(src.InstagramUrl))
				dest.InstagramUrl = src.InstagramUrl;
			if (!String.IsNullOrEmpty(src.YoutubeUrl))
				dest.YoutubeUrl = src.YoutubeUrl;
			if (!String.IsNullOrEmpty(src.LinkedinUrl))
				dest.LinkedinUrl = src.LinkedinUrl;
			if (!String.IsNullOrEmpty(src.TwitterUrl))
				dest.TwitterUrl = src.TwitterUrl;
			if (src.Latitude > 0)
				dest.Latitude = src.Latitude;
			if (src.Longitude > 0)
				dest.Longitude = src.Longitude;
		}

	}
}
