using System;
using System.ComponentModel.DataAnnotations;

namespace EgyVisionCore.Entities.EgyVision
{
    public partial class File_last_view : BaseEntity
    {
        public string userId { get; set; }
        public string userMail { get; set; }
        public long fileId { get; set; }
        public string fileNameAr { get; set; }
        public Nullable<DateTime> lastView { get; set; }
        public Nullable<DateTime> lastDownload { get; set; }
        [Key]
        public int id { get; set; }
    }
}
