using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{

    public interface ISlidersService
    {
        List<SlidersVM> Search(SlidersVM model);
        bool Insert(SlidersVM vm);
        Sliders InsertAndReturnModel(SlidersVM vm);
        bool Update(SlidersVM vm);
        bool Delete(SlidersVM vm);
        SlidersVM GetById(int ID);
    }

    public class SlidersService : ISlidersService
    {
        private IEgyVisionRepository<Sliders> _SlidersRepo = null;
        public SlidersService()
        {
            _SlidersRepo = new EgyVisionRepository<Sliders>();
        }

        public bool Insert(SlidersVM vm)
        {
            Sliders model = new Sliders();
            copyToModel(vm, model);
            bool success = _SlidersRepo.Insert(model);
            //if (success)
            //vm.AddressId = model.AddressId;
            return success;
        }
        public Sliders InsertAndReturnModel(SlidersVM vm)
        {
            Sliders model = new Sliders();
            copyToModel(vm, model);
            bool success = _SlidersRepo.Insert(model);
            //if (success)
            //vm.AddressId = model.AddressId;
            return model;
        }

        public bool Update(SlidersVM vm)
        {
            Sliders model = _SlidersRepo.GetById(vm.SliderId);
            copyToModel(vm, model);
            return _SlidersRepo.Update(model);
        }

        public bool Delete(SlidersVM vm)
        {
            Sliders model = _SlidersRepo.GetById(vm.SliderId);
            return _SlidersRepo.Delete(model);
        }

        public List<SlidersVM> Search(SlidersVM model)
        {
            List<SlidersVM> returned = new List<SlidersVM>();
            var predicate = PredicateBuilder.New<Sliders>(true);

            if (model.SliderId > 0)
            {
                predicate = predicate.And(p => p.SliderId == model.SliderId);
            }
            if (!String.IsNullOrEmpty(model.SliderTitleAr))
            {
                predicate = predicate.And(p => p.SliderTitleAr == model.SliderTitleAr);
            }
            if (!String.IsNullOrEmpty(model.SliderTitleEn))
            {
                predicate = predicate.And(p => p.SliderTitleEn == model.SliderTitleEn);
            }
            if (!String.IsNullOrEmpty(model.ContentAr))
            {
                predicate = predicate.And(p => p.ContentAr == model.ContentAr);
            }
            if (!String.IsNullOrEmpty(model.ContentEn))
            {
                predicate = predicate.And(p => p.ContentEn == model.ContentEn);
            }
            //predicate = predicate.And(p => p.MainSlider == model.MainSlider);

            IQueryable<Sliders> query = _SlidersRepo.Table.AsExpandable().Where(predicate);
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
                model.OrderBy = "SliderId";
                model.OrderByReversed = false;
            }
            if (model.OrderBy == "SliderId" && model.OrderByReversed == true)
                query = query.AsExpandable().OrderByDescending(x => x.SliderId).Where(predicate);
            else if (model.OrderBy == "SliderId" && model.OrderByReversed == false)
                query = query.AsExpandable().OrderBy(x => x.SliderId).Where(predicate);
            else if (model.OrderBy == "SliderTitleAr" && model.OrderByReversed == true)
                query = query.AsExpandable().OrderByDescending(x => x.SliderTitleAr).Where(predicate);
            else if (model.OrderBy == "SliderTitleAr" && model.OrderByReversed == false)
                query = query.AsExpandable().OrderBy(x => x.SliderTitleAr).Where(predicate);
            else if (model.OrderBy == "SliderTitleEn" && model.OrderByReversed == true)
                query = query.AsExpandable().OrderByDescending(x => x.SliderTitleEn).Where(predicate);
            else if (model.OrderBy == "SliderTitleEn" && model.OrderByReversed == false)
                query = query.AsExpandable().OrderBy(x => x.SliderTitleEn).Where(predicate);
            else if (model.OrderBy == "ContentAr" && model.OrderByReversed == true)
                query = query.AsExpandable().OrderByDescending(x => x.ContentAr).Where(predicate);
            else if (model.OrderBy == "ContentAr" && model.OrderByReversed == false)
                query = query.AsExpandable().OrderBy(x => x.ContentAr).Where(predicate);
            else if (model.OrderBy == "ContentEn" && model.OrderByReversed == true)
                query = query.AsExpandable().OrderByDescending(x => x.ContentEn).Where(predicate);
            else
                query = query.AsExpandable().OrderBy(x => x.ContentEn).Where(predicate);
            model.TotalRecordCount = query.Count();
            int index = 0;
            int startRow = model.jtStartIndex;
            if (model.jtPageSize <= 0)
                model.jtPageSize = 1000;
            foreach (Sliders record in query)
            {
                if (index >= startRow && index < (model.jtPageSize + startRow))
                {
                    SlidersVM vm = new SlidersVM();
                    copyToVM(record, vm);
                    returned.Add(vm);
                }
                index++;
                if (index > (startRow + model.jtPageSize))
                    break;
            }
            return returned;
        }

        public SlidersVM GetById(int ID)
        {
            Sliders model = _SlidersRepo.GetById(ID);
            SlidersVM vm = new SlidersVM();
            copyToVM(model, vm);
            return vm;
        }

        private void copyToModel(SlidersVM src, Sliders dest)
        {
            if (src.SliderId > 0)
                dest.SliderId = src.SliderId;
            if (!String.IsNullOrEmpty(src.SliderTitleAr))
                dest.SliderTitleAr = src.SliderTitleAr;
            if (!String.IsNullOrEmpty(src.SliderTitleEn))
                dest.SliderTitleEn = src.SliderTitleEn;
            if (!String.IsNullOrEmpty(src.ContentAr))
                dest.ContentAr = src.ContentAr;
            if (!String.IsNullOrEmpty(src.ContentEn))
                dest.ContentEn = src.ContentEn;
            dest.MainSlider = src.MainSlider;
        }

        private void copyToVM(Sliders src, SlidersVM dest)
        {
            if (src.SliderId > 0)
                dest.SliderId = src.SliderId;
            if (!String.IsNullOrEmpty(src.SliderTitleAr))
                dest.SliderTitleAr = src.SliderTitleAr;
            if (!String.IsNullOrEmpty(src.SliderTitleEn))
                dest.SliderTitleEn = src.SliderTitleEn;
            if (!String.IsNullOrEmpty(src.ContentAr))
                dest.ContentAr = src.ContentAr;
            if (!String.IsNullOrEmpty(src.ContentEn))
                dest.ContentEn = src.ContentEn;
            dest.MainSlider = src.MainSlider;
        }

    }
}
