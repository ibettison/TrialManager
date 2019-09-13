using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using TrialManager.Models;

namespace TrialManager.Controllers
{
    public class TrialActiveController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialActive
        public ActionResult Index()
        {
            var trialActiveModels = db.TrialActiveModels.Include(t => t.ShortName).Include(t => t.StatusName);
            return View(trialActiveModels.ToList());
        }

        // GET: TrialActive/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialActiveModels trialActiveModels = db.TrialActiveModels.Find(id);
            if (trialActiveModels == null)
            {
                return HttpNotFound();
            }
            return View(trialActiveModels);
        }

        // GET: TrialActive/Create
        public ActionResult Create()
        {
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            ViewBag.StatusId = new SelectList(db.ActiveStatusModels, "Id", "StatusName");
            return View();
        }

        // POST: TrialActive/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrialId,StatusId,TargetFPFV,ActualFPFV,CurrentRecruitment,EstimatedLPLV,ActualLPLV,EOSNotificationDue,EOSNotificationSubmitted,SiteCloseDown")] TrialActiveModels trialActiveModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialActiveModels.Add(trialActiveModels);
                db.SaveChanges();
                return RedirectToAction("Edit", "TrialFeasibility", new {Id = trialActiveModels.TrialId});
            }

            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialActiveModels.TrialId);
            ViewBag.StatusId = new SelectList(db.ActiveStatusModels, "Id", "StatusName", trialActiveModels.StatusId);
            return View(trialActiveModels);
        }

        // GET: TrialActive/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialActiveModels trialActiveModels = db.TrialActiveModels.Find(id);
            if (trialActiveModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialActiveModels.TrialId);
            ViewBag.StatusId = new SelectList(db.ActiveStatusModels, "Id", "StatusName", trialActiveModels.StatusId);
            return View(trialActiveModels);
        }

        // POST: TrialActive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrialId,StatusId,TargetFPFV,ActualFPFV,CurrentRecruitment,EstimatedLPLV,ActualLPLV,EOSNotificationDue,EOSNotificationSubmitted,SiteCloseDown")] TrialActiveModels trialActiveModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialActiveModels).State = EntityState.Modified;
                db.SaveChanges();
               return RedirectToAction("Index");
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialActiveModels.TrialId);
            ViewBag.StatusId = new SelectList(db.ActiveStatusModels, "Id", "StatusName", trialActiveModels.StatusId);
            return View(trialActiveModels);
        }

        // GET: TrialActive/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialActiveModels trialActiveModels = db.TrialActiveModels.Find(id);
            if (trialActiveModels == null)
            {
                return HttpNotFound();
            }
            return View(trialActiveModels);
        }

        // POST: TrialActive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialActiveModels trialActiveModels = db.TrialActiveModels.Find(id);
            db.TrialActiveModels.Remove(trialActiveModels);
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
