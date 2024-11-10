using ProjectTracking.Models.Context;
using ProjectTracking.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTracking.Controllers
{
    public class ProjectReportController : Controller
    {
        private ProjectTrackingDbContext context = new ProjectTrackingDbContext();

        // GET: ProjectReport
        //tamamlanmış öncelik grupları
        public ActionResult CompletedPriorityGroups()
        {
            return View();
        }

        public ActionResult VisualizeCompletedStatusGroups()
        {
            return Json(PriorityGroupType(), JsonRequestBehavior.AllowGet);
        }

        public List<PrioritySituationAnalysis> PriorityGroupType()
        {
            List<PrioritySituationAnalysis> prioritySituationAnalyses = new List<PrioritySituationAnalysis>();

            using (var db=new ProjectTrackingDbContext())
            {
                prioritySituationAnalyses = db.PersonelProjects.Where(x => x.CompletionStatus == true).GroupBy(x => x.PriorityStatus).Select(z => new PrioritySituationAnalysis
                {
                    PriorityType = z.Key,
                    PriorityQuantity = z.Count()
                }).ToList();

                return prioritySituationAnalyses;
            }
        }

        //tamamlanmamış öncelik grupları
        public ActionResult UnCompletedPriorityGroups()
        {
            return View();
        }

        public ActionResult VisualizeUnCompletedStatusGroups()
        {
            return Json(PriorityGroupType(), JsonRequestBehavior.AllowGet);
        }

        public List<PrioritySituationAnalysis> PriorityCompletedGroupType()
        {
            List<PrioritySituationAnalysis> prioritySituationAnalyses = new List<PrioritySituationAnalysis>();
            using (var c = new ProjectTrackingDbContext())

                prioritySituationAnalyses = c.PersonelProjects.Where(x => x.CompletionStatus == false).GroupBy(p => p.PriorityStatus).Select(x => new PrioritySituationAnalysis
                {

                    PriorityType = x.Key,
                    PriorityQuantity = x.Count(),
                }).ToList();

            return prioritySituationAnalyses;
        }

        public ActionResult GeneralProjectReports()
        {
            return View();
        }

        public ActionResult LiveSupport()
        {
            var support = context.PersonnelInformations.Where(x => x.Department == "Yönetim").ToList();

            return View(support);
        }
    }
}