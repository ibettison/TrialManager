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
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
    public class TrialRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialRecords
        public ActionResult Index()
        {
            var trialRecordsModels = db.TrialRecordsModels.Include(t => t.ShortName);
            return View(trialRecordsModels.ToList());
        }

        // GET: TrialRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialRecordsModels trialRecordsModels = db.TrialRecordsModels.Find(id);
            if (trialRecordsModels == null)
            {
                return HttpNotFound();
            }
            return View(trialRecordsModels);
        }

        // GET: TrialRecords/Create
        public ActionResult Create()
        {
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            return View();
        }

        // POST: TrialRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateTime,WhoChanged,OriginalValue,NewValue,FieldName,ReasonForChange,TrialId")] TrialRecordsModels trialRecordsModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialRecordsModels.Add(trialRecordsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialRecordsModels.TrialId);
            return View(trialRecordsModels);
        }

        // GET: TrialRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialRecordsModels trialRecordsModels = db.TrialRecordsModels.Find(id);
            if (trialRecordsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialRecordsModels.TrialId);
            return View(trialRecordsModels);
        }

        // POST: TrialRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateTime,WhoChanged,OriginalValue,NewValue,FieldName,ReasonForChange,TrialId")] TrialRecordsModels trialRecordsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialRecordsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialRecordsModels.TrialId);
            return View(trialRecordsModels);
        }

        // GET: TrialRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialRecordsModels trialRecordsModels = db.TrialRecordsModels.Find(id);
            if (trialRecordsModels == null)
            {
                return HttpNotFound();
            }
            return View(trialRecordsModels);
        }

        // POST: TrialRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialRecordsModels trialRecordsModels = db.TrialRecordsModels.Find(id);
            db.TrialRecordsModels.Remove(trialRecordsModels);
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
