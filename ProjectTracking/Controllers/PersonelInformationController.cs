using ProjectTracking.Models.Context;
using ProjectTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectTracking.Controllers
{
    public class PersonelInformationController : Controller
    {

        private readonly ProjectTrackingDbContext _context;


        public PersonelInformationController()
        {
            _context = new ProjectTrackingDbContext();
        }

        public PersonelInformationController(ProjectTrackingDbContext context)
        {
            _context = context;
        }

        // GET: PersonelInformation
        public ActionResult Index()
        {
            var result=_context.PersonnelInformations.ToList();

            return View(result);
        }

        public ActionResult PersonelCard() //
        {
            return View(_context.PersonnelInformations.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelInformation personnelInformation)
        {
            if (ModelState.IsValid)
            {
                _context.PersonnelInformations.Add(personnelInformation);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(personnelInformation);
        }

        public ActionResult Details(int id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PersonnelInformation personnelInformation = _context.PersonnelInformations.Find(id);

            if (personnelInformation==null)
            {
                return HttpNotFound();
            }

            return View(personnelInformation);
        }

        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PersonnelInformation personnelInformation = _context.PersonnelInformations.Find(id);

            if (personnelInformation==null)
            {
                return HttpNotFound();
            }

            return View(personnelInformation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonnelInformation personnelInformation) 
        {
            if (ModelState.IsValid)
            {
                _context.Entry(personnelInformation).State=EntityState.Modified;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(personnelInformation);
        }

        public ActionResult Delete(int? id)
        {
            if (id==null || id==0)
            {
                return HttpNotFound();
            }

            var result = _context.PersonnelInformations.Find(id);
            _context.PersonnelInformations.Remove(result);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}