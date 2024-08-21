using EgyVisionCore;
using EgyVisionCore.Entities.STS;
using EgyVisionCore.Entities.STS.VM;
using EgyVisionRepository;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyVisionService.STS
{
    public interface IClientsAudiencesService
	{
		List<ClientsAudiencesVM> Search(QueryFilterVM filter);
		bool Insert(ClientsAudiencesVM vm);
		bool Update(ClientsAudiencesVM vm);
		bool Delete(ClientsAudiencesVM vm);
		ClientsAudiencesVM GetById(int ClientId);
        ClientsAudiences getByKeys(string AccessKey, string SecretKey, string audiance);
    }

	public class ClientsAudiencesService : IClientsAudiencesService
	{
		private ISTSRepository<ClientsAudiences> _ClientsAudiencesRepo = null;
		public ClientsAudiencesService()
		{
			_ClientsAudiencesRepo = new STSRepository<ClientsAudiences>();
		}

		public bool Insert(ClientsAudiencesVM vm)
		{
			ClientsAudiences model = new ClientsAudiences();
			copyToModel(vm,model);
			return _ClientsAudiencesRepo.Insert(model);
		}

		public bool Update(ClientsAudiencesVM vm)
		{
			ClientsAudiences model = _ClientsAudiencesRepo.GetById(vm.ClientId);
			copyToModel(vm,model);
			return _ClientsAudiencesRepo.Update(model);
		}

		public bool Delete(ClientsAudiencesVM vm)
		{
			ClientsAudiences model = _ClientsAudiencesRepo.GetById(vm.ClientId);
			return _ClientsAudiencesRepo.Delete(model);
		}

		public List<ClientsAudiencesVM> Search(QueryFilterVM filter)
		{
			List<ClientsAudiencesVM> returned = new List<ClientsAudiencesVM>();
			var predicate = PredicateBuilder.New<ClientsAudiences>(true);

			foreach (FilterFieldsVM field in filter.FilterFields)
			{
				if (field.FilterColumn == "ClientId" && !String.IsNullOrEmpty(field.FilterValue))
				{
					int obj = int.Parse(field.FilterValue);
					predicate = predicate.And(p => p.ClientId == obj);
				}
				else if (field.FilterColumn == "Audience" && !String.IsNullOrEmpty(field.FilterValue))
				{
					predicate = predicate.And(p => p.Audience.Contains(field.FilterValue));
				}
				else if (field.FilterColumn == "CientName" && !String.IsNullOrEmpty(field.FilterValue))
				{
					predicate = predicate.And(p => p.CientName.Contains(field.FilterValue));
				}
				else if (field.FilterColumn == "AccessKey" && !String.IsNullOrEmpty(field.FilterValue))
				{
					predicate = predicate.And(p => p.AccessKey.Contains(field.FilterValue));
				}
				else if (field.FilterColumn == "SecretKey" && !String.IsNullOrEmpty(field.FilterValue))
				{
					predicate = predicate.And(p => p.SecretKey.Contains(field.FilterValue));
				}
			}

			IQueryable<ClientsAudiences> query = _ClientsAudiencesRepo.Table.AsExpandable().Where(predicate);

			if (filter.OrderBy == "ClientId" && filter.OrderByReversed != "true")
				query = query.AsExpandable().OrderByDescending(x => x.ClientId).Where(predicate);
			else if (filter.OrderBy == "ClientId" && filter.OrderByReversed != "false")
				query = query.AsExpandable().OrderBy(x => x.ClientId).Where(predicate);
			else if (filter.OrderBy == "Audience" && filter.OrderByReversed != "true")
				query = query.AsExpandable().OrderByDescending(x => x.Audience).Where(predicate);
			else if (filter.OrderBy == "Audience" && filter.OrderByReversed != "false")
				query = query.AsExpandable().OrderBy(x => x.Audience).Where(predicate);
			else if (filter.OrderBy == "CientName" && filter.OrderByReversed != "true")
				query = query.AsExpandable().OrderByDescending(x => x.CientName).Where(predicate);
			else if (filter.OrderBy == "CientName" && filter.OrderByReversed != "false")
				query = query.AsExpandable().OrderBy(x => x.CientName).Where(predicate);
			else if (filter.OrderBy == "AccessKey" && filter.OrderByReversed != "true")
				query = query.AsExpandable().OrderByDescending(x => x.AccessKey).Where(predicate);
			else if (filter.OrderBy == "AccessKey" && filter.OrderByReversed != "false")
				query = query.AsExpandable().OrderBy(x => x.AccessKey).Where(predicate);
			else if (filter.OrderBy == "SecretKey" && filter.OrderByReversed != "true")
				query = query.AsExpandable().OrderByDescending(x => x.SecretKey).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.SecretKey).Where(predicate);
			filter.TotalCount = query.Count();

			int index = 0;
			filter.PageIndex++;
			int startRow = filter.StartIndex;

			if (filter.PageSize <= 0)
				filter.PageSize = 1000;

			foreach (ClientsAudiences record in query)
			{
				if (index >= startRow && index < (filter.PageSize + startRow))
				{
					ClientsAudiencesVM vm = new ClientsAudiencesVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + filter.PageSize))
					break;

			}

			return returned;
		}

		public ClientsAudiencesVM GetById(int ClientId)
		{
			ClientsAudiences model = _ClientsAudiencesRepo.GetById(ClientId);
			ClientsAudiencesVM vm = new ClientsAudiencesVM();
			copyToVM(model,vm);
			return vm;
		}

		private void copyToModel(ClientsAudiencesVM src, ClientsAudiences dest)
		{
			if (src.ClientId > 0)
				dest.ClientId = src.ClientId;
			if (!String.IsNullOrEmpty(src.Audience))
				dest.Audience = src.Audience;
			if (!String.IsNullOrEmpty(src.CientName))
				dest.CientName = src.CientName;
			if (!String.IsNullOrEmpty(src.AccessKey))
				dest.AccessKey = src.AccessKey;
			if (!String.IsNullOrEmpty(src.SecretKey))
				dest.SecretKey = src.SecretKey;
		}

		private void copyToVM(ClientsAudiences src, ClientsAudiencesVM dest)
		{
			if (src.ClientId > 0)
				dest.ClientId = src.ClientId;
			if (!String.IsNullOrEmpty(src.Audience))
				dest.Audience = src.Audience;
			if (!String.IsNullOrEmpty(src.CientName))
				dest.CientName = src.CientName;
			if (!String.IsNullOrEmpty(src.AccessKey))
				dest.AccessKey = src.AccessKey;
			if (!String.IsNullOrEmpty(src.SecretKey))
				dest.SecretKey = src.SecretKey;
		}

        public ClientsAudiences getByKeys(string AccessKey, string SecretKey, string audiance)
        {
            return _ClientsAudiencesRepo.Table.Where(x => x.AccessKey == AccessKey && x.SecretKey == SecretKey && x.Audience == audiance).FirstOrDefault();
        }

    }
}
