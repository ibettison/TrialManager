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
    public class TrialRemindersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialReminders
        public ActionResult Index()
        {
            var trialRemindersModels = db.TrialRemindersModels.Include(t => t.ShortName);
            return View(trialRemindersModels.ToList());
        }

        // GET: TrialReminders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialRemindersModels trialRemindersModels = db.TrialRemindersModels.Find(id);
            if (trialRemindersModels == null)
            {
                return HttpNotFound();
            }
            return View(trialRemindersModels);
        }

        // GET: TrialReminders/Create
        public ActionResult Create()
        {
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            return View();
        }

        // POST: TrialReminders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Reminder,TrialId,DueDate")] TrialRemindersModels trialRemindersModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialRemindersModels.Add(trialRemindersModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialRemindersModels.TrialId);
            return View(trialRemindersModels);
        }

        // GET: TrialReminders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialRemindersModels trialRemindersModels = db.TrialRemindersModels.Find(id);
            if (trialRemindersModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialRemindersModels.TrialId);
            return View(trialRemindersModels);
        }

        // POST: TrialReminders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Reminder,TrialId,DueDate")] TrialRemindersModels trialRemindersModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialRemindersModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialRemindersModels.TrialId);
            return View(trialRemindersModels);
        }

        // GET: TrialReminders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialRemindersModels trialRemindersModels = db.TrialRemindersModels.Find(id);
            if (trialRemindersModels == null)
            {
                return HttpNotFound();
            }
            return View(trialRemindersModels);
        }

        // POST: TrialReminders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialRemindersModels trialRemindersModels = db.TrialRemindersModels.Find(id);
            db.TrialRemindersModels.Remove(trialRemindersModels);
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
