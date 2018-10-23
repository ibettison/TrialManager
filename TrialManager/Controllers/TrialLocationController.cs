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
    public class TrialLocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialLocation
        public ActionResult Index()
        {
            return View(db.TrialLocationModels.ToList());
        }

        // GET: TrialLocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialLocationModels trialLocationModels = db.TrialLocationModels.Find(id);
            if (trialLocationModels == null)
            {
                return HttpNotFound();
            }
            return View(trialLocationModels);
        }

        // GET: TrialLocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrialLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Location")] TrialLocationModels trialLocationModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialLocationModels.Add(trialLocationModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trialLocationModels);
        }

        // GET: TrialLocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialLocationModels trialLocationModels = db.TrialLocationModels.Find(id);
            if (trialLocationModels == null)
            {
                return HttpNotFound();
            }
            return View(trialLocationModels);
        }

        // POST: TrialLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Location")] TrialLocationModels trialLocationModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialLocationModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trialLocationModels);
        }

        // GET: TrialLocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialLocationModels trialLocationModels = db.TrialLocationModels.Find(id);
            if (trialLocationModels == null)
            {
                return HttpNotFound();
            }
            return View(trialLocationModels);
        }

        // POST: TrialLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialLocationModels trialLocationModels = db.TrialLocationModels.Find(id);
            db.TrialLocationModels.Remove(trialLocationModels);
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
