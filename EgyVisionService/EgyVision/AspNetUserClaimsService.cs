using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IAspNetUserClaimsService
	{
		List<AspNetUserClaimsVM> Search(AspNetUserClaimsVM model);
		bool Insert(AspNetUserClaimsVM vm);
		bool Update(AspNetUserClaimsVM vm);
		bool Delete(AspNetUserClaimsVM vm);
		AspNetUserClaimsVM GetById(int Id);
	}

	public class AspNetUserClaimsService : IAspNetUserClaimsService
	{
		private IEgyVisionRepository<AspNetUserClaims> _AspNetUserClaimsRepo = null;
		public AspNetUserClaimsService()
		{
			_AspNetUserClaimsRepo = new EgyVisionRepository<AspNetUserClaims>();
		}

		public bool Insert(AspNetUserClaimsVM vm)
		{
			AspNetUserClaims model = new AspNetUserClaims();
			copyToModel(vm,model);
			bool success = _AspNetUserClaimsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AspNetUserClaimsVM vm)
		{
			AspNetUserClaims model = _AspNetUserClaimsRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AspNetUserClaimsRepo.Update(model);
		}

		public bool Delete(AspNetUserClaimsVM vm)
		{
			AspNetUserClaims model = _AspNetUserClaimsRepo.GetById(vm.Id);
			return _AspNetUserClaimsRepo.Delete(model);
		}

		public List<AspNetUserClaimsVM> Search(AspNetUserClaimsVM model)
		{
			List<AspNetUserClaimsVM> returned = new List<AspNetUserClaimsVM>();
			var predicate = PredicateBuilder.New<AspNetUserClaims>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (!String.IsNullOrEmpty(model.UserId))
			//{
				//predicate = predicate.And(p => p.UserId == model.UserId);
			//}
			//if (!String.IsNullOrEmpty(model.ClaimType))
			//{
				//predicate = predicate.And(p => p.ClaimType == model.ClaimType);
			//}
			//if (!String.IsNullOrEmpty(model.ClaimValue))
			//{
				//predicate = predicate.And(p => p.ClaimValue == model.ClaimValue);
			//}

			IQueryable<AspNetUserClaims> query = _AspNetUserClaimsRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "UserId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "UserId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.UserId).Where(predicate);
			else if (model.OrderBy == "ClaimType" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ClaimType).Where(predicate);
			else if (model.OrderBy == "ClaimType" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ClaimType).Where(predicate);
			else if (model.OrderBy == "ClaimValue" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.ClaimValue).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.ClaimValue).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (AspNetUserClaims record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AspNetUserClaimsVM vm = new AspNetUserClaimsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AspNetUserClaimsVM GetById(int Id)
		{
			AspNetUserClaims model = _AspNetUserClaimsRepo.GetById(Id);
			AspNetUserClaimsVM vm = new AspNetUserClaimsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AspNetUserClaimsVM src, AspNetUserClaims dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			if (!String.IsNullOrEmpty(src.ClaimType))
				dest.ClaimType = src.ClaimType;
			if (!String.IsNullOrEmpty(src.ClaimValue))
				dest.ClaimValue = src.ClaimValue;
		}

		private void copyToVM(AspNetUserClaims src, AspNetUserClaimsVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.UserId))
				dest.UserId = src.UserId;
			if (!String.IsNullOrEmpty(src.ClaimType))
				dest.ClaimType = src.ClaimType;
			if (!String.IsNullOrEmpty(src.ClaimValue))
				dest.ClaimValue = src.ClaimValue;
		}

	}
}
