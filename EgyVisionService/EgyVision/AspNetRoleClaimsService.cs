using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IAspNetRoleClaimsService
	{
		List<AspNetRoleClaimsVM> Search(AspNetRoleClaimsVM model);
		bool Insert(AspNetRoleClaimsVM vm);
		bool Update(AspNetRoleClaimsVM vm);
		bool Delete(AspNetRoleClaimsVM vm);
		AspNetRoleClaimsVM GetById(int Id);
	}

	public class AspNetRoleClaimsService : IAspNetRoleClaimsService
	{
		private IEgyVisionRepository<AspNetRoleClaims> _AspNetRoleClaimsRepo = null;
		public AspNetRoleClaimsService()
		{
			_AspNetRoleClaimsRepo = new EgyVisionRepository<AspNetRoleClaims>();
		}

		public bool Insert(AspNetRoleClaimsVM vm)
		{
			AspNetRoleClaims model = new AspNetRoleClaims();
			copyToModel(vm,model);
			bool success = _AspNetRoleClaimsRepo.Insert(model);
			//if (success)
				//vm.AddressId = model.AddressId;
			return success;
		}

		public bool Update(AspNetRoleClaimsVM vm)
		{
			AspNetRoleClaims model = _AspNetRoleClaimsRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AspNetRoleClaimsRepo.Update(model);
		}

		public bool Delete(AspNetRoleClaimsVM vm)
		{
			AspNetRoleClaims model = _AspNetRoleClaimsRepo.GetById(vm.Id);
			return _AspNetRoleClaimsRepo.Delete(model);
		}

		public List<AspNetRoleClaimsVM> Search(AspNetRoleClaimsVM model)
		{
			List<AspNetRoleClaimsVM> returned = new List<AspNetRoleClaimsVM>();
			var predicate = PredicateBuilder.New<AspNetRoleClaims>(true);

			//if (model.Id > 0)
			//{
				//predicate = predicate.And(p => p.Id == model.Id);
			//}
			//if (!String.IsNullOrEmpty(model.RoleId))
			//{
				//predicate = predicate.And(p => p.RoleId == model.RoleId);
			//}
			//if (!String.IsNullOrEmpty(model.ClaimType))
			//{
				//predicate = predicate.And(p => p.ClaimType == model.ClaimType);
			//}
			//if (!String.IsNullOrEmpty(model.ClaimValue))
			//{
				//predicate = predicate.And(p => p.ClaimValue == model.ClaimValue);
			//}

			IQueryable<AspNetRoleClaims> query = _AspNetRoleClaimsRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "RoleId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.RoleId).Where(predicate);
			else if (model.OrderBy == "RoleId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.RoleId).Where(predicate);
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

			foreach (AspNetRoleClaims record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					AspNetRoleClaimsVM vm = new AspNetRoleClaimsVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}

		public AspNetRoleClaimsVM GetById(int Id)
		{
			AspNetRoleClaims model = _AspNetRoleClaimsRepo.GetById(Id);
			AspNetRoleClaimsVM vm = new AspNetRoleClaimsVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AspNetRoleClaimsVM src, AspNetRoleClaims dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
			if (!String.IsNullOrEmpty(src.ClaimType))
				dest.ClaimType = src.ClaimType;
			if (!String.IsNullOrEmpty(src.ClaimValue))
				dest.ClaimValue = src.ClaimValue;
		}

		private void copyToVM(AspNetRoleClaims src, AspNetRoleClaimsVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.RoleId))
				dest.RoleId = src.RoleId;
			if (!String.IsNullOrEmpty(src.ClaimType))
				dest.ClaimType = src.ClaimType;
			if (!String.IsNullOrEmpty(src.ClaimValue))
				dest.ClaimValue = src.ClaimValue;
		}

	}
}
