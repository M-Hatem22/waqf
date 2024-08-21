using EgyVisionCore;
using EgyVisionCore.Entities.STS;
using EgyVisionCore.Entities.STS.VM;
using EgyVisionRepository;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyVision.STS
{
    public interface IAudienceKeysService
	{
		List<AudienceKeysVM> Search(QueryFilterVM filter);
		bool Insert(AudienceKeysVM vm);
		bool Update(AudienceKeysVM vm);
		bool Delete(AudienceKeysVM vm);
		AudienceKeysVM GetById(int Id);
        AudienceKeys getByAudience(string audience);
    }

	public class AudienceKeysService : IAudienceKeysService
	{
		private ISTSRepository<AudienceKeys> _AudienceKeysRepo = null;
		public AudienceKeysService()
		{
			_AudienceKeysRepo = new STSRepository<AudienceKeys>();
		}

		public bool Insert(AudienceKeysVM vm)
		{
			AudienceKeys model = new AudienceKeys();
			copyToModel(vm,model);
			return _AudienceKeysRepo.Insert(model);
		}

		public bool Update(AudienceKeysVM vm)
		{
			AudienceKeys model = _AudienceKeysRepo.GetById(vm.Id);
			copyToModel(vm,model);
			return _AudienceKeysRepo.Update(model);
		}

		public bool Delete(AudienceKeysVM vm)
		{
			AudienceKeys model = _AudienceKeysRepo.GetById(vm.Id);
			return _AudienceKeysRepo.Delete(model);
		}

		public List<AudienceKeysVM> Search(QueryFilterVM filter)
		{
			List<AudienceKeysVM> returned = new List<AudienceKeysVM>();
			var predicate = PredicateBuilder.New<AudienceKeys>(true);

			foreach (FilterFieldsVM field in filter.FilterFields)
			{
				if (field.FilterColumn == "Id" && !String.IsNullOrEmpty(field.FilterValue))
				{
					int obj = int.Parse(field.FilterValue);
					predicate = predicate.And(p => p.Id == obj);
				}
				else if (field.FilterColumn == "GeneratedKey" && !String.IsNullOrEmpty(field.FilterValue))
				{
					predicate = predicate.And(p => p.GeneratedKey.Contains(field.FilterValue));
				}
				else if (field.FilterColumn == "Audience" && !String.IsNullOrEmpty(field.FilterValue))
				{
					predicate = predicate.And(p => p.Audience.Contains(field.FilterValue));
				}
			}

			IQueryable<AudienceKeys> query = _AudienceKeysRepo.Table.AsExpandable().Where(predicate);

			if (filter.OrderBy == "Id" && filter.OrderByReversed != "true")
				query = query.AsExpandable().OrderByDescending(x => x.Id).Where(predicate);
			else if (filter.OrderBy == "Id" && filter.OrderByReversed != "false")
				query = query.AsExpandable().OrderBy(x => x.Id).Where(predicate);
			else if (filter.OrderBy == "GeneratedKey" && filter.OrderByReversed != "true")
				query = query.AsExpandable().OrderByDescending(x => x.GeneratedKey).Where(predicate);
			else if (filter.OrderBy == "GeneratedKey" && filter.OrderByReversed != "false")
				query = query.AsExpandable().OrderBy(x => x.GeneratedKey).Where(predicate);
			else if (filter.OrderBy == "Audience" && filter.OrderByReversed != "true")
				query = query.AsExpandable().OrderByDescending(x => x.Audience).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.Audience).Where(predicate);
			filter.TotalCount = query.Count();

			int index = 0;
			filter.PageIndex++;
			int startRow = filter.StartIndex;

			if (filter.PageSize <= 0)
				filter.PageSize = 1000;

			foreach (AudienceKeys record in query)
			{
				if (index >= startRow && index < (filter.PageSize + startRow))
				{
					AudienceKeysVM vm = new AudienceKeysVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + filter.PageSize))
					break;

			}

			return returned;
		}

		public AudienceKeysVM GetById(int Id)
		{
			AudienceKeys model = _AudienceKeysRepo.GetById(Id);
			AudienceKeysVM vm = new AudienceKeysVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(AudienceKeysVM src, AudienceKeys dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.GeneratedKey))
				dest.GeneratedKey = src.GeneratedKey;
			if (!String.IsNullOrEmpty(src.Audience))
				dest.Audience = src.Audience;
		}

		private void copyToVM(AudienceKeys src, AudienceKeysVM dest)
		{
			if (src.Id > 0)
				dest.Id = src.Id;
			if (!String.IsNullOrEmpty(src.GeneratedKey))
				dest.GeneratedKey = src.GeneratedKey;
			if (!String.IsNullOrEmpty(src.Audience))
				dest.Audience = src.Audience;
		}

        public AudienceKeys getByAudience(string audience)
        {
            return _AudienceKeysRepo.Table.Where(k => k.Audience == audience).FirstOrDefault();
        }

    }
}
