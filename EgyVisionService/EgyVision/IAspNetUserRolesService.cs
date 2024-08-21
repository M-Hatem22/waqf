using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgyVisionService.EgyVision
{
    public interface IAspNetUserRolesService
    {
        List<AspNetUserRolesVM> Search(AspNetUserRolesVM model);
        bool Insert(AspNetUserRolesVM vm);
        bool Update(AspNetUserRolesVM vm);
        bool Delete(AspNetUserRolesVM vm);
        AspNetUserRolesVM GetById(string UserId, string RoleId);
    }

    public class AspNetUserRolesService : IAspNetUserRolesService
    {
        private IEgyVisionRepository<AspNetUserRoles> _AspNetUserRolesRepo = null;
        public AspNetUserRolesService()
        {
            _AspNetUserRolesRepo = new EgyVisionRepository<AspNetUserRoles>();
        }

        public bool Insert(AspNetUserRolesVM vm)
        {
            AspNetUserRoles model = new AspNetUserRoles();
            copyToModel(vm, model);
            bool success = _AspNetUserRolesRepo.Insert(model);
            //if (success)
            //vm.AddressId = model.AddressId;
            return success;
        }

        public bool Update(AspNetUserRolesVM vm)
        {
            AspNetUserRoles model = _AspNetUserRolesRepo.Table.Where(x => x.UserId == vm.UserId && x.RoleId == vm.RoleId).FirstOrDefault();
            copyToModel(vm, model);
            return _AspNetUserRolesRepo.Update(model);
        }

        public bool Delete(AspNetUserRolesVM vm)
        {
            AspNetUserRoles model = _AspNetUserRolesRepo.Table.Where(x => x.UserId == vm.UserId && x.RoleId == vm.RoleId).FirstOrDefault();
            //AspNetUserRoles model = _AspNetUserRolesRepo.Table.Where(x => x.UserId == vm.UserId || x.RoleId == vm.RoleId).FirstOrDefault();
            return _AspNetUserRolesRepo.Delete(model);
        }

        public List<AspNetUserRolesVM> Search(AspNetUserRolesVM model)
        {
            List<AspNetUserRolesVM> returned = new List<AspNetUserRolesVM>();
            var predicate = PredicateBuilder.New<AspNetUserRoles>(true);

            if (!String.IsNullOrEmpty(model.UserId))
            {
                predicate = predicate.And(p => p.UserId == model.UserId);
            }
            if (!String.IsNullOrEmpty(model.RoleId))
            {
                predicate = predicate.And(p => p.RoleId == model.RoleId);
            }

            IQueryable<AspNetUserRoles> query = _AspNetUserRolesRepo.Table.AsExpandable().Where(predicate);

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
                model.OrderBy = "UserId";
                model.OrderByReversed = false;
            }
            if (model.OrderBy == "UserId" && model.OrderByReversed == true)
                query = query.AsExpandable().OrderByDescending(x => x.UserId).Where(predicate);
            else if (model.OrderBy == "UserId" && model.OrderByReversed == false)
                query = query.AsExpandable().OrderBy(x => x.UserId).Where(predicate);
            else if (model.OrderBy == "RoleId" && model.OrderByReversed == true)
                query = query.AsExpandable().OrderByDescending(x => x.RoleId).Where(predicate);
            else
                query = query.AsExpandable().OrderBy(x => x.RoleId).Where(predicate);
            model.TotalRecordCount = query.Count();

            int index = 0;
            int startRow = model.jtStartIndex;

            if (model.jtPageSize <= 0)
                model.jtPageSize = 1000;

            foreach (AspNetUserRoles record in query)
            {
                if (index >= startRow && index < (model.jtPageSize + startRow))
                {
                    AspNetUserRolesVM vm = new AspNetUserRolesVM();
                    copyToVM(record, vm);
                    returned.Add(vm);
                }

                index++;
                if (index > (startRow + model.jtPageSize))
                    break;

            }

            return returned;
        }

        public AspNetUserRolesVM GetById(string UserId, string RoleId)
        {
            AspNetUserRoles model = _AspNetUserRolesRepo.Table.Where(x => x.UserId == UserId && x.RoleId == RoleId).FirstOrDefault();
            AspNetUserRolesVM vm = new AspNetUserRolesVM();
            copyToVM(model, vm);
            return vm;
        }

        private void copyToModel(AspNetUserRolesVM src, AspNetUserRoles dest)
        {
            if (!String.IsNullOrEmpty(src.UserId))
                dest.UserId = src.UserId;
            if (!String.IsNullOrEmpty(src.RoleId))
                dest.RoleId = src.RoleId;
        }

        private void copyToVM(AspNetUserRoles src, AspNetUserRolesVM dest)
        {
            if (!String.IsNullOrEmpty(src.UserId))
                dest.UserId = src.UserId;
            if (!String.IsNullOrEmpty(src.RoleId))
                dest.RoleId = src.RoleId;
        }

    }
}
