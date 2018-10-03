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
    public class TrialTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialType
        public ActionResult Index()
        {
            return View(db.TrialTypeModels.ToList());
        }

        // GET: TrialType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialTypeModels trialTypeModels = db.TrialTypeModels.Find(id);
            if (trialTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(trialTypeModels);
        }

        // GET: TrialType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrialType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrialTypeName")] TrialTypeModels trialTypeModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialTypeModels.Add(trialTypeModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trialTypeModels);
        }

        // GET: TrialType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialTypeModels trialTypeModels = db.TrialTypeModels.Find(id);
            if (trialTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(trialTypeModels);
        }

        // POST: TrialType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrialTypeName")] TrialTypeModels trialTypeModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialTypeModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trialTypeModels);
        }

        // GET: TrialType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialTypeModels trialTypeModels = db.TrialTypeModels.Find(id);
            if (trialTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(trialTypeModels);
        }

        // POST: TrialType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialTypeModels trialTypeModels = db.TrialTypeModels.Find(id);
            db.TrialTypeModels.Remove(trialTypeModels);
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
