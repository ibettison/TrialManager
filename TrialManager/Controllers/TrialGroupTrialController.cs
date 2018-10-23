using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrialManager.Models;
using Trialmanager.Models;

namespace TrialManager.Controllers
{
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators")]
    public class TrialGroupTrialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialGroupTrial
        public ActionResult Index()
        {
            var trialGroupTrialModels = db.TrialGroupTrialModels.Include(t => t.GroupName).Include(t => t.ShortName);
            return View(trialGroupTrialModels.ToList());
        }

        // GET: TrialGroupTrial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialGroupTrialModels trialGroupTrialModels = db.TrialGroupTrialModels.Find(id);
            if (trialGroupTrialModels == null)
            {
                return HttpNotFound();
            }
            return View(trialGroupTrialModels);
        }

        // GET: TrialGroupTrial/Create
        public ActionResult Create()
        {
            ViewBag.TrialGroupId = new SelectList(db.TrialGroupModels, "Id", "GroupName");
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            return View();
        }

        // POST: TrialGroupTrial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrialId,TrialGroupId")] TrialGroupTrialModels trialGroupTrialModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialGroupTrialModels.Add(trialGroupTrialModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrialGroupId = new SelectList(db.TrialGroupModels, "Id", "GroupName", trialGroupTrialModels.TrialGroupId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialGroupTrialModels.TrialId);
            return View(trialGroupTrialModels);
        }

        // GET: TrialGroupTrial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialGroupTrialModels trialGroupTrialModels = db.TrialGroupTrialModels.Find(id);
            if (trialGroupTrialModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialGroupId = new SelectList(db.TrialGroupModels, "Id", "GroupName", trialGroupTrialModels.TrialGroupId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialGroupTrialModels.TrialId);
            return View(trialGroupTrialModels);
        }

        // POST: TrialGroupTrial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrialId,TrialGroupId")] TrialGroupTrialModels trialGroupTrialModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialGroupTrialModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrialGroupId = new SelectList(db.TrialGroupModels, "Id", "GroupName", trialGroupTrialModels.TrialGroupId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialGroupTrialModels.TrialId);
            return View(trialGroupTrialModels);
        }

        // GET: TrialGroupTrial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialGroupTrialModels trialGroupTrialModels = db.TrialGroupTrialModels.Find(id);
            if (trialGroupTrialModels == null)
            {
                return HttpNotFound();
            }
            return View(trialGroupTrialModels);
        }

        // POST: TrialGroupTrial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialGroupTrialModels trialGroupTrialModels = db.TrialGroupTrialModels.Find(id);
            db.TrialGroupTrialModels.Remove(trialGroupTrialModels);
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
