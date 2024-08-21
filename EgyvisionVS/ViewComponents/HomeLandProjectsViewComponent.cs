using EgyVisionCore.Entities.EgyVision.VM;
using EgyVisionService.EgyVision;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyvisionVS.ViewComponents
{
    public class HomeLandProjectsViewComponent : ViewComponent
    {
        public HomeLandProjectsViewComponent()
        {

        }
        public IViewComponentResult Invoke()
        {
            try
            {
                IProjectCoverAttachmentViewService projectservice = new ProjectCoverAttachmentViewService();
                var Projects = projectservice.Search(new ProjectCoverAttachmentViewVM() { });
                return View(Projects);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
