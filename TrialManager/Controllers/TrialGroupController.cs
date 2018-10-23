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
    public class TrialGroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialGroup
        public ActionResult Index()
        {
            return View(db.TrialGroupModels.ToList());
        }

        // GET: TrialGroup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialGroupModels trialGroupModels = db.TrialGroupModels.Find(id);
            if (trialGroupModels == null)
            {
                return HttpNotFound();
            }
            return View(trialGroupModels);
        }

        // GET: TrialGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrialGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupName")] TrialGroupModels trialGroupModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialGroupModels.Add(trialGroupModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trialGroupModels);
        }

        // GET: TrialGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialGroupModels trialGroupModels = db.TrialGroupModels.Find(id);
            if (trialGroupModels == null)
            {
                return HttpNotFound();
            }
            return View(trialGroupModels);
        }

        // POST: TrialGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupName")] TrialGroupModels trialGroupModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialGroupModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trialGroupModels);
        }

        // GET: TrialGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialGroupModels trialGroupModels = db.TrialGroupModels.Find(id);
            if (trialGroupModels == null)
            {
                return HttpNotFound();
            }
            return View(trialGroupModels);
        }

        // POST: TrialGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialGroupModels trialGroupModels = db.TrialGroupModels.Find(id);
            db.TrialGroupModels.Remove(trialGroupModels);
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
