using EgyVisionCore.Entities.EgyVision;
using EgyVisionCore.Entities.EgyVision.VM;
using System.Collections.Generic;

namespace BackEgyVision.Models
{
    public class GroupsRolesViewModel
    {
        public GroupsRolesViewModel()
        {
            Roles = new List<AppRole>();

            Group = new AspNetGroupsVM();
        }
        public List<AppRole> Roles { get; set; }
        public AspNetGroupsVM Group { get; set; }
    }
}
