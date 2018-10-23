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
    public class TrialSetupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialSetup
        public ActionResult Index()
        {
            var trialSetupModels = db.TrialSetupModels.Include(t => t.Location).Include(t => t.ShortName);
            return View(trialSetupModels.ToList());
        }

        // GET: TrialSetup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialSetupModels trialSetupModels = db.TrialSetupModels.Find(id);
            if (trialSetupModels == null)
            {
                return HttpNotFound();
            }
            return View(trialSetupModels);
        }

        // GET: TrialSetup/Create
        public ActionResult Create()
        {
            ViewBag.TrialLocationId = new SelectList(db.TrialLocationModels, "Id", "Location");
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            return View();
        }

        // POST: TrialSetup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectIdentifier,ResearchDevelopmentId,GrantFunderRef,SponsorRef,RecRef,EudractRef,IrasId,RecruitmentTarget,TrialLocationId,RecDate,HraDate,CtaDate,TrialId")] TrialSetupModels trialSetupModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialSetupModels.Add(trialSetupModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrialLocationId = new SelectList(db.TrialLocationModels, "Id", "Location", trialSetupModels.TrialLocationId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialSetupModels.TrialId);
            return View(trialSetupModels);
        }

        // GET: TrialSetup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialSetupModels trialSetupModels = db.TrialSetupModels.Find(id);
            if (trialSetupModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialLocationId = new SelectList(db.TrialLocationModels, "Id", "Location", trialSetupModels.TrialLocationId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialSetupModels.TrialId);
            return View(trialSetupModels);
        }

        // POST: TrialSetup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectIdentifier,ResearchDevelopmentId,GrantFunderRef,SponsorRef,RecRef,EudractRef,IrasId,RecruitmentTarget,TrialLocationId,RecDate,HraDate,CtaDate,TrialId")] TrialSetupModels trialSetupModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialSetupModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrialLocationId = new SelectList(db.TrialLocationModels, "Id", "Location", trialSetupModels.TrialLocationId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialSetupModels.TrialId);
            return View(trialSetupModels);
        }

        // GET: TrialSetup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialSetupModels trialSetupModels = db.TrialSetupModels.Find(id);
            if (trialSetupModels == null)
            {
                return HttpNotFound();
            }
            return View(trialSetupModels);
        }

        // POST: TrialSetup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialSetupModels trialSetupModels = db.TrialSetupModels.Find(id);
            db.TrialSetupModels.Remove(trialSetupModels);
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
