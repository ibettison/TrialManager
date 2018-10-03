using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trialmanager.Models;

namespace Trialmanager.Controllers
{
    public class DiseaseTherapyAreaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DiseaseTherapyArea
        public ActionResult Index()
        {
            return View(db.DiseaseTherapyAreaModels.ToList());
        }

        // GET: DiseaseTherapyArea/Details/5
        public ActionResult Details(int? id)
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

        // GET: DiseaseTherapyArea/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: DiseaseTherapyArea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiseaseTherapyAreaModels diseaseTherapyAreaModels = db.DiseaseTherapyAreaModels.Find(id);
            db.DiseaseTherapyAreaModels.Remove(diseaseTherapyAreaModels);
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
