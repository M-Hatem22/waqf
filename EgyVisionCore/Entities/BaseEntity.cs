using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EgyVisionCore.Entities
{
    public abstract partial class BaseEntity
    {
        [NotMapped]
        public int jtStartIndex { get; set; }
        [NotMapped]
        public int jtPageSize { get; set; }
        [NotMapped]
        public string jtSorting { get; set; }
        [NotMapped]
        public int TotalRecordCount { get; set; }
    }
}
