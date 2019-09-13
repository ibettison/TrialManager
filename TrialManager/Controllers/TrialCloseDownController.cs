using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrialManager.Models;

namespace TrialManager.Controllers
{
    public class TrialCloseDownController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialCloseDown
        public ActionResult Index()
        {
            var trialCloseDownModels = db.TrialCloseDownModels.Include(t => t.ShortName);
            return View(trialCloseDownModels.ToList());
        }

        // GET: TrialCloseDown/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialCloseDownModels trialCloseDownModels = db.TrialCloseDownModels.Find(id);
            if (trialCloseDownModels == null)
            {
                return HttpNotFound();
            }
            return View(trialCloseDownModels);
        }

        // GET: TrialCloseDown/Create
        public ActionResult Create()
        {
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            return View();
        }

        // POST: TrialCloseDown/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrialId,ClinicalStudyReportDue,ClinicalStudyReportSubmitted,EudraCTUploadDue,EudraCTUpload,Archiving,ArchivingPeriod,Destruction")] TrialCloseDownModels trialCloseDownModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialCloseDownModels.Add(trialCloseDownModels);
                db.SaveChanges();
                return RedirectToAction("Edit", "TrialFeasibility", new {Id = trialCloseDownModels.TrialId});
            }

            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialCloseDownModels.TrialId);
            return View(trialCloseDownModels);
        }

        // GET: TrialCloseDown/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialCloseDownModels trialCloseDownModels = db.TrialCloseDownModels.Find(id);
            if (trialCloseDownModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialCloseDownModels.TrialId);
            return View(trialCloseDownModels);
        }

        // POST: TrialCloseDown/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrialId,ClinicalStudyReportDue,ClinicalStudyReportSubmitted,EudraCTUploadDue,EudraCTUpload,Archiving,ArchivingPeriod,Destruction")] TrialCloseDownModels trialCloseDownModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialCloseDownModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialCloseDownModels.TrialId);
            return View(trialCloseDownModels);
        }

        // GET: TrialCloseDown/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialCloseDownModels trialCloseDownModels = db.TrialCloseDownModels.Find(id);
            if (trialCloseDownModels == null)
            {
                return HttpNotFound();
            }
            return View(trialCloseDownModels);
        }

        // POST: TrialCloseDown/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialCloseDownModels trialCloseDownModels = db.TrialCloseDownModels.Find(id);
            db.TrialCloseDownModels.Remove(trialCloseDownModels);
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
