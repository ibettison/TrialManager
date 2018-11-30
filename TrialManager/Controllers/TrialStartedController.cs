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
    public class TrialStartedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialStarted
        public ActionResult Index()
        {
            var trialStartedModels = db.TrialStartedModels.Include(t => t.ShortName);
            return View(trialStartedModels.ToList());
        }

        // GET: TrialStarted/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialStartedModels trialStartedModels = db.TrialStartedModels.Find(id);
            if (trialStartedModels == null)
            {
                return HttpNotFound();
            }
            return View(trialStartedModels);
        }

        // GET: TrialStarted/Create
        public ActionResult Create()
        {
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            return View();
        }

        // POST: TrialStarted/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Started,TrialId,Reason, DateChanged")] TrialStartedModels trialStartedModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialStartedModels.Add(trialStartedModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialStartedModels.TrialId);
            return View(trialStartedModels);
        }

        // GET: TrialStarted/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialStartedModels trialStartedModels = db.TrialStartedModels.Find(id);
            if (trialStartedModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialStartedModels.TrialId);
            return View(trialStartedModels);
        }

        // POST: TrialStarted/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Started,TrialId,Reason,DateChanged")] TrialStartedModels trialStartedModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialStartedModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialStartedModels.TrialId);
            return View(trialStartedModels);
        }

        // GET: TrialStarted/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialStartedModels trialStartedModels = db.TrialStartedModels.Find(id);
            if (trialStartedModels == null)
            {
                return HttpNotFound();
            }
            return View(trialStartedModels);
        }

        // POST: TrialStarted/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialStartedModels trialStartedModels = db.TrialStartedModels.Find(id);
            db.TrialStartedModels.Remove(trialStartedModels);
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
