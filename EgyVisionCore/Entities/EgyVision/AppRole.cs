using Microsoft.AspNetCore.Identity;
using System;
namespace EgyVisionCore.Entities.EgyVision
{
    public class AppRole : IdentityRole
    {
        public AppRole(string name) : base(name)
        {
        }
        public AppRole() : base()
        {
        }
        public string Description { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
