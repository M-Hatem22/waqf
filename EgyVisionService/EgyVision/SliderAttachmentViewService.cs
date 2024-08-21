using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{

	public interface ISliderAttachmentViewService
	{
		List<SliderAttachmentViewVM> Search(SliderAttachmentViewVM model);
	}

	public class SliderAttachmentViewService : ISliderAttachmentViewService
	{
		private IEgyVisionRepository<SliderAttachmentView> _SliderAttachmentViewRepo = null;
		public SliderAttachmentViewService()
		{
			_SliderAttachmentViewRepo = new EgyVisionRepository<SliderAttachmentView>();
		}

		public List<SliderAttachmentViewVM> Search(SliderAttachmentViewVM model)
		{
			List<SliderAttachmentViewVM> returned = new List<SliderAttachmentViewVM>();
			var predicate = PredicateBuilder.New<SliderAttachmentView>(true);

            //if (!String.IsNullOrEmpty(model.SliderTitleAr))
            //{
            //predicate = predicate.And(p => p.SliderTitleAr == model.SliderTitleAr);
            //}
            //if (!String.IsNullOrEmpty(model.SliderTitleEn))
            //{
            //predicate = predicate.And(p => p.SliderTitleEn == model.SliderTitleEn);
            //}
            //if (!String.IsNullOrEmpty(model.ContentAr))
            //{
            //predicate = predicate.And(p => p.ContentAr == model.ContentAr);
            //}
            //if (!String.IsNullOrEmpty(model.ContentEn))
            //{
            //predicate = predicate.And(p => p.ContentEn == model.ContentEn);
            //}
            //predicate = predicate.And(p => p.AttachmentFile == model.AttachmentFile);
            if (model.KeyId > 0)
            {
                predicate = predicate.And(p => p.KeyId == model.KeyId);
            }
            if (model.LKKeyTypeId > 0)
            {
                predicate = predicate.And(p => p.LKKeyTypeId == model.LKKeyTypeId);
            }
            if (model.SliderId > 0)
			{
				predicate = predicate.And(p => p.SliderId == model.SliderId);
			}
			if (model.LKAttachmentTypeId > 0)
            {
                predicate = predicate.And(p => p.LKAttachmentTypeId == model.LKAttachmentTypeId);
            }
           // predicate = predicate.And(p => p.MainSlider == model.MainSlider);
            if (model.AttachmentId > 0)
            {
                predicate = predicate.And(p => p.AttachmentId == model.AttachmentId);
            }
            IQueryable<SliderAttachmentView> query = _SliderAttachmentViewRepo.Table.AsExpandable().Where(predicate);

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
			else if (model.OrderBy == "ContentEn" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.ContentEn).Where(predicate);
			else if (model.OrderBy == "AttachmentFile" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentFile).Where(predicate);
			else if (model.OrderBy == "AttachmentFile" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.AttachmentFile).Where(predicate);
			else if (model.OrderBy == "KeyId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.KeyId).Where(predicate);
			else if (model.OrderBy == "KeyId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.KeyId).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKKeyTypeId).Where(predicate);
			else if (model.OrderBy == "LKKeyTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKKeyTypeId).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.LKAttachmentTypeId).Where(predicate);
			else if (model.OrderBy == "LKAttachmentTypeId" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.LKAttachmentTypeId).Where(predicate);
			else if (model.OrderBy == "MainSlider" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.MainSlider).Where(predicate);
			else if (model.OrderBy == "MainSlider" && model.OrderByReversed == false)
				query = query.AsExpandable().OrderBy(x => x.MainSlider).Where(predicate);
			else if (model.OrderBy == "AttachmentId" && model.OrderByReversed == true)
				query = query.AsExpandable().OrderByDescending(x => x.AttachmentId).Where(predicate);
			else
				query = query.AsExpandable().OrderBy(x => x.AttachmentId).Where(predicate);
			model.TotalRecordCount = query.Count();

			int index = 0;
			int startRow = model.jtStartIndex;

			if (model.jtPageSize <= 0)
				model.jtPageSize = 1000;

			foreach (SliderAttachmentView record in query)
			{
				if (index >= startRow && index < (model.jtPageSize + startRow))
				{
					SliderAttachmentViewVM vm = new SliderAttachmentViewVM();
					copyToVM(record, vm);
					returned.Add(vm);
				}

				index++;
				if (index > (startRow + model.jtPageSize))
					break;

			}

			return returned;
		}
		private void copyToModel(SliderAttachmentViewVM src, SliderAttachmentView dest)
		{
			if (!String.IsNullOrEmpty(src.SliderTitleAr))
				dest.SliderTitleAr = src.SliderTitleAr;
			if (!String.IsNullOrEmpty(src.SliderTitleEn))
				dest.SliderTitleEn = src.SliderTitleEn;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			dest.KeyId = src.KeyId;
			if (src.LKKeyTypeId > 0)
				dest.LKKeyTypeId = src.LKKeyTypeId;
			if (src.LKAttachmentTypeId > 0)
				dest.LKAttachmentTypeId = src.LKAttachmentTypeId;
			dest.MainSlider = src.MainSlider;
			dest.AttachmentId = src.AttachmentId;
			if (src.SliderId > 0)
				dest.SliderId = src.SliderId;
		}

		private void copyToVM(SliderAttachmentView src, SliderAttachmentViewVM dest)
		{
			if (!String.IsNullOrEmpty(src.SliderTitleAr))
				dest.SliderTitleAr = src.SliderTitleAr;
			if (!String.IsNullOrEmpty(src.SliderTitleEn))
				dest.SliderTitleEn = src.SliderTitleEn;
			if (!String.IsNullOrEmpty(src.ContentAr))
				dest.ContentAr = src.ContentAr;
			if (!String.IsNullOrEmpty(src.ContentEn))
				dest.ContentEn = src.ContentEn;
			if (src.AttachmentFile != null && src.AttachmentFile.Length > 0)
				dest.AttachmentFile = src.AttachmentFile;
			dest.KeyId = src.KeyId;
			if (src.LKKeyTypeId > 0)
				dest.LKKeyTypeId = src.LKKeyTypeId;
			if (src.LKAttachmentTypeId > 0)
				dest.LKAttachmentTypeId = src.LKAttachmentTypeId;
			dest.MainSlider = src.MainSlider;
			dest.AttachmentId = src.AttachmentId;
			if (src.SliderId > 0)
				dest.SliderId = src.SliderId;
		}

	}
}
