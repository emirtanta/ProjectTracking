using Newtonsoft.Json;
using ProjectTracking.Models.Context;
using ProjectTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTracking.Controllers
{
    public class PersonelProjectController : Controller
    {
        private readonly ProjectTrackingDbContext _context;


        public PersonelProjectController()
        {
            _context=new ProjectTrackingDbContext();
        }

        public PersonelProjectController(ProjectTrackingDbContext context)
        {
            _context = context;
        }



        // GET: PersonelProject
        public ActionResult Index()
        {
            var projectList = _context.PersonelProjects.ToList();

            return View(projectList);
        }

        public ActionResult Create()
        {
            ViewBag.PersonelInformationId = new SelectList(
            _context.PersonnelInformations.Select(p => new
            {
                PersonelInformationId = p.PersonelInformationId,
                AdSoyad = p.Name + " " + p.Surname
            }),
            "PersonelInformationId",
            "AdSoyad"
            );

            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonelProject personelProject, int[] personelInformationId) 
        {
            foreach (var item in personelInformationId)
            {
                personelProject.PersonnelInformations.Add(_context.PersonnelInformations.Find(item));
            }

            personelProject.CreatedDate = DateTime.Now;

            _context.PersonelProjects.Add(personelProject);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            // Veritabanından projeyi getir
            var result = _context.PersonelProjects
                .Include(p => p.PersonnelInformations) // Projeye bağlı personelleri dahil et
                .FirstOrDefault(p => p.PersonelProjectId == id);

            if (result == null)
            {
                return HttpNotFound();
            }

            // Tüm personel listesini hazırlıyoruz
            ViewBag.PersonelInformationId = new SelectList(
                _context.PersonnelInformations.Select(p => new
                {
                    PersonelInformationId = p.PersonelInformationId,
                    AdSoyad = p.Name + " " + p.Surname
                }),
                "PersonelInformationId",
                "AdSoyad"
            );

            // AssignedPersonnel bilgisini JSON formatına çeviriyoruz
            ViewBag.AssignedPersonnelJson = JsonConvert.SerializeObject(result.PersonnelInformations.Select(pi => new
            {
                pi.PersonelInformationId,
                AdSoyad = pi.Name + " " + pi.Surname
            }).ToList());

            return View(result);

        }

        [HttpPost]
        public ActionResult Edit(PersonelProject personelProject, int[] personelInformationId)
        {
            //var result = _context.PersonelProjects.Find(personelProject.PersonelProjectId);
            //result.ProjectDescription = personelProject.ProjectDescription;
            //result.ProjectTitle = personelProject.ProjectTitle;
            //result.CompletionRate = personelProject.CompletionRate;
            //result.PriorityStatus = personelProject.PriorityStatus;
            //result.CompletionDate = DateTime.Now;

            //foreach (var item in personelInformationId)
            //{
            //    personelProject.PersonnelInformations.Add(_context.PersonnelInformations.Find(item));
            //}

            //_context.SaveChanges();

            //return RedirectToAction(nameof(Index));

            // Veritabanındaki mevcut projeyi getir
            var result = _context.PersonelProjects
                .Include(p => p.PersonnelInformations) // Projeye bağlı personelleri dahil et
                .FirstOrDefault(p => p.PersonelProjectId == personelProject.PersonelProjectId);

            if (result == null)
            {
                return HttpNotFound();
            }

            // Proje bilgilerini güncelle
            result.ProjectTitle = personelProject.ProjectTitle;
            result.ProjectDescription = personelProject.ProjectDescription;
            result.CompletionRate = personelProject.CompletionRate;
            result.PriorityStatus = personelProject.PriorityStatus;
            result.CompletionDate = DateTime.Now;

            // Eski personelleri temizle
            result.PersonnelInformations.Clear();

            // Yeni seçilen personelleri ekle
            if (personelInformationId != null && personelInformationId.Any())
            {
                foreach (var personelId in personelInformationId)
                {
                    var personel = _context.PersonnelInformations.Find(personelId);
                    if (personel != null)
                    {
                        result.PersonnelInformations.Add(personel);
                    }
                }
            }

            // Değişiklikleri kaydet
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        public ActionResult Complete(int id)
        {
            var result = _context.PersonelProjects.Find(id);
            result.CompletionStatus = true;
            result.CompletionRate = 100;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}