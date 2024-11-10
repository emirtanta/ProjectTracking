using ProjectTracking.Models.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTracking.Controllers
{
    public class DashboardController : Controller
    {
        private  ProjectTrackingDbContext context = new ProjectTrackingDbContext();

       

        // GET: Dashboard
        public ActionResult Index()
        {
            //proje sayısı
            var projectCount = context.PersonelProjects.Count();
            ViewBag.ProjectCount = projectCount;

            //tamamlanmış proje sayısı
            var completedProjectCount = context.PersonelProjects.Where(x => x.CompletionStatus == true).Count();
            ViewBag.CompletedProjectCount = completedProjectCount;

            //yüksek öncelikli proje sayısı
            var highPriorityProjectCount = context.PersonelProjects.Where(x => x.PriorityStatus == "Yüksek Öncelikli").Count();
            ViewBag.HighPriorityProjectCount = highPriorityProjectCount;

            //orta öncelikli proje sayısı
            var midPriorityProjectCount = context.PersonelProjects.Where(x => x.PriorityStatus == "Orta Öncelikli").Count();
            ViewBag.MidPriorityProjectCount = midPriorityProjectCount;

            //düşük öncelikli proje sayısı
            var lowPriorityProject = context.PersonelProjects.Where(x => x.PriorityStatus == "Düşük Öncelikli").Count();
            ViewBag.LowPriorityProjectCount = lowPriorityProject;

            //başarılı ve yüksek öncelikli proje sayısı
            var successfulAndHighProjectCount = context.PersonelProjects.Where(x => x.CompletionStatus == true && x.PriorityStatus == "Yüksek Öncelikli").Count();
            ViewBag.SuccessfulAndHighProjectCount = successfulAndHighProjectCount;

            //başarılı ve orta öncelikli proje sayısı
            var successfulAndMidProjectCount = context.PersonelProjects.Where(x => x.CompletionStatus == true && x.PriorityStatus == "Orta Öncelikli").Count();
            ViewBag.SuccessfulAndMidProjectCount = successfulAndMidProjectCount;

            //düşük ve yüksek öncelikli proje sayısı
            var successfulAndLowProjectCount = context.PersonelProjects.Where(x => x.CompletionStatus == true && x.PriorityStatus == "Yüksek Öncelikli").Count();
            ViewBag.SuccessfulAndLowProjectCount = successfulAndLowProjectCount;

            //tamamlanmamış proje sayısı
            var unfinishedProjectCount = context.PersonelProjects.Where(x => x.CompletionStatus == false).Count();
            ViewBag.UnfinishedProjectCount = unfinishedProjectCount;

            //personel proje listesi
            var personelProjectList = context.PersonelProjects.ToList();

            var personelFinishedProjectCount = new Dictionary<int, int>();//personelId-tamamlanmış proje sayısı çiftlerini tutmak için

            foreach (var personel in context.PersonnelInformations.ToList())
            {
                int finishedProjectCount = 0;

                foreach (var project in personel.PersonelProjects)
                {
                    if (project.CompletionStatus == true)
                    {
                        completedProjectCount++;
                    }
                }

                personelFinishedProjectCount[personel.PersonelInformationId] = finishedProjectCount;
            }

            //sıralı personel listesi
            var rankedStaffList = personelFinishedProjectCount.OrderByDescending(x => x.Value);

            //en çok proje tamamlayan personel
            var mostProjectCompletedPersonelId = rankedStaffList.First().Key;

            //en çok taöaölanan 
            var mostProjectCompletedPersonel = context.PersonnelInformations.FirstOrDefault(x => x.PersonelInformationId == mostProjectCompletedPersonelId);
            ViewBag.MostProjectCompletedPersonel = mostProjectCompletedPersonel.Name +" "+ mostProjectCompletedPersonel.Surname;

            int theMostProjectToPersonelCount = personelFinishedProjectCount[mostProjectCompletedPersonelId];
            ViewBag.TheMostProjectToPersonelCount = theMostProjectToPersonelCount;

            return View();
        }

        public ActionResult GeneralStatistics()
        {
            var personels = context.PersonnelInformations.ToList();

            var personelProjects = context.PersonelProjects.ToList();

            var finishedProjectCount = new Dictionary<int, int>();

            var unfinishedProjectCount=new Dictionary<int,int>();

            var totalProjectCount=new Dictionary<int, int>();

            foreach (var personel in personels)
            {
                int finishedProject = 0;
                int unfinishedProject = 0;
                int totalProject = 0;

                foreach (var project in personelProjects)
                {
                    if (project.PersonnelInformations.Contains(personel))
                    {
                        totalProject++;

                        if (project.CompletionStatus)
                        {
                            finishedProject++;
                        }

                        else
                        {
                            unfinishedProject++;
                        }
                    }
                }

                finishedProjectCount[personel.PersonelInformationId]=finishedProject;
                unfinishedProjectCount[personel.PersonelInformationId]=unfinishedProject;
                totalProjectCount[personel.PersonelInformationId] = totalProject;
            }

            ViewBag.FinishedProjectCount = finishedProjectCount;
            ViewBag.UnfinishedProjectCount = unfinishedProjectCount;
            ViewBag.TotalProjectCount = totalProjectCount;

            //proje sayısı
            int projectCount = context.PersonelProjects.Count();
            ViewBag.ProjectCount = projectCount;

            //personel sayısı
            int personelCount = context.PersonnelInformations.Count();
            ViewBag.PersonelCount = personelCount;

            //tamamlanmış proje
            int completedProject=context.PersonelProjects.Where(x=>x.CompletionStatus==true).Count();
            ViewBag.CompletedProject = completedProject;

            //tamamlanmamış proje
            int uncompletedProject = context.PersonelProjects.Where(x => x.CompletionStatus == false).Count();
            ViewBag.UncompletedProjectCount = uncompletedProject;

            //başarısız ve yüksek öncelikli proje saıyısı
            var unsuccessfullAndHigh = context.PersonelProjects.Where(x => x.CompletionStatus == false && x.PriorityStatus == "Yüksek Öncelikli").Count();
            ViewBag.UnsuccessfullAndHigh= unsuccessfullAndHigh;

            //başarısız ve orta öncelikli proje saıyısı
            var unsuccessfullAndMid = context.PersonelProjects.Where(x => x.CompletionStatus == false && x.PriorityStatus == "Orta Öncelikli").Count();
            ViewBag.UnsuccessfullAndMid = unsuccessfullAndMid;

            return View(personels);
        }
    }
}