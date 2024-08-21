using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision.VM
{
    public partial class File_last_viewVM
    {
        public string userId { get; set; }
        public string userMail { get; set; }
        public long fileId { get; set; }
        public string fileNameAr { get; set; }
        public Nullable<DateTime> lastView { get; set; }
        public Nullable<DateTime> lastDownload { get; set; }
        public int id { get; set; }
        public int jtStartIndex { get; set; }
        public int jtPageSize { get; set; }
        public string jtSorting { get; set; }
        public int TotalRecordCount { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByReversed { get; set; }
    }
}