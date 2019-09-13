using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TrialManager.Models;

namespace TrialManager.Controllers
{
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
    public class TrialLateDevelopmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialLateDevelopment
        public ActionResult Index()
        {
            var trialLateDevelopmentModels = db.TrialLateDevelopmentModels.Include(t => t.ShortName);
            return View(trialLateDevelopmentModels.ToList());
        }

        // GET: TrialLateDevelopment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialLateDevelopmentModels trialLateDevelopmentModels = db.TrialLateDevelopmentModels.Find(id);
            if (trialLateDevelopmentModels == null)
            {
                return HttpNotFound();
            }
            return View(trialLateDevelopmentModels);
        }

        // GET: TrialLateDevelopment/Create
        public ActionResult Create()
        {
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            return View();
        }

        // POST: TrialLateDevelopment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrialId,ClinicalTrialsGovRef,LocalCC,SponsorGreenLight,SiteInitiationVisit,LocalSiteActivationDate")] TrialLateDevelopmentModels trialLateDevelopmentModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialLateDevelopmentModels.Add(trialLateDevelopmentModels);
                db.SaveChanges();
                return RedirectToAction("Edit", "TrialFeasibility", new { Id = trialLateDevelopmentModels.TrialId });
            }

            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialLateDevelopmentModels.TrialId);
            return View(trialLateDevelopmentModels);
        }

        // GET: TrialLateDevelopment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialLateDevelopmentModels trialLateDevelopmentModels = db.TrialLateDevelopmentModels.Find(id);
            if (trialLateDevelopmentModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialLateDevelopmentModels.TrialId);
            return View(trialLateDevelopmentModels);
        }

        // POST: TrialLateDevelopment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrialId,ClinicalTrialsGovRef,LocalCC,SponsorGreenLight,LocalSiteInitiation,TargetFpfvDate,MultiSiteInitiation")] TrialLateDevelopmentModels trialLateDevelopmentModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialLateDevelopmentModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialLateDevelopmentModels.TrialId);
            return View(trialLateDevelopmentModels);
        }

        // GET: TrialLateDevelopment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialLateDevelopmentModels trialLateDevelopmentModels = db.TrialLateDevelopmentModels.Find(id);
            if (trialLateDevelopmentModels == null)
            {
                return HttpNotFound();
            }
            return View(trialLateDevelopmentModels);
        }

        // POST: TrialLateDevelopment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialLateDevelopmentModels trialLateDevelopmentModels = db.TrialLateDevelopmentModels.Find(id);
            db.TrialLateDevelopmentModels.Remove(trialLateDevelopmentModels);
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
