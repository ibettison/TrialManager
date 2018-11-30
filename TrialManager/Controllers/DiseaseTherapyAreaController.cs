using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trialmanager.Models;

namespace Trialmanager.Controllers
{
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
    public class DiseaseTherapyAreaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DiseaseTherapyArea
        public ActionResult Index()
        {
            return View(db.DiseaseTherapyAreaModels.Where(d => d.Deleted == null).ToList());
        }

        // GET: DiseaseTherapyArea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiseaseTherapyArea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DiseaseTherapyAreaName")] DiseaseTherapyAreaModels diseaseTherapyAreaModels)
        {
            if (ModelState.IsValid)
            {
                db.DiseaseTherapyAreaModels.Add(diseaseTherapyAreaModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diseaseTherapyAreaModels);
        }

        // GET: DiseaseTherapyArea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseaseTherapyAreaModels diseaseTherapyAreaModels = db.DiseaseTherapyAreaModels.Find(id);
            if (diseaseTherapyAreaModels == null)
            {
                return HttpNotFound();
            }
            return View(diseaseTherapyAreaModels);
        }

        // POST: DiseaseTherapyArea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DiseaseTherapyAreaName")] DiseaseTherapyAreaModels diseaseTherapyAreaModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diseaseTherapyAreaModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diseaseTherapyAreaModels);
        }

        // POST: DiseaseTherapyArea/Delete/5
        [HttpPost]
        public ActionResult DeleteTherapy(DiseaseTherapyAreaModels model)
        {
            var id = model.Id;
            DiseaseTherapyAreaModels diseaseTherapyAreaModels = db.DiseaseTherapyAreaModels.Find(id);
            diseaseTherapyAreaModels.Deleted = DateTime.Now;
            db.DiseaseTherapyAreaModels.AddOrUpdate(diseaseTherapyAreaModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
