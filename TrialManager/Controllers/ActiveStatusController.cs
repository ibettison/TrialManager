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
    public class ActiveStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActiveStatus
        public ActionResult Index()
        {
            return View(db.ActiveStatusModels.ToList());
        }

        // GET: ActiveStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActiveStatusModels activeStatusModels = db.ActiveStatusModels.Find(id);
            if (activeStatusModels == null)
            {
                return HttpNotFound();
            }
            return View(activeStatusModels);
        }

        // GET: ActiveStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActiveStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StatusName")] ActiveStatusModels activeStatusModels)
        {
            if (ModelState.IsValid)
            {
                db.ActiveStatusModels.Add(activeStatusModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activeStatusModels);
        }

        // GET: ActiveStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActiveStatusModels activeStatusModels = db.ActiveStatusModels.Find(id);
            if (activeStatusModels == null)
            {
                return HttpNotFound();
            }
            return View(activeStatusModels);
        }

        // POST: ActiveStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StatusName")] ActiveStatusModels activeStatusModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activeStatusModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activeStatusModels);
        }

        // GET: ActiveStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActiveStatusModels activeStatusModels = db.ActiveStatusModels.Find(id);
            if (activeStatusModels == null)
            {
                return HttpNotFound();
            }
            return View(activeStatusModels);
        }

        // POST: ActiveStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActiveStatusModels activeStatusModels = db.ActiveStatusModels.Find(id);
            db.ActiveStatusModels.Remove(activeStatusModels);
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
