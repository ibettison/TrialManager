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
    public class TrialContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialContacts
        public ActionResult Index()
        {
            var trialContactsModels = db.TrialContactsModels.Include(t => t.ContactName).Include(t => t.RoleName).Include(t => t.ShortName);
            return View(trialContactsModels.ToList());
        }

        // GET: TrialContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialContactsModels trialContactsModels = db.TrialContactsModels.Find(id);
            if (trialContactsModels == null)
            {
                return HttpNotFound();
            }
            return View(trialContactsModels);
        }

        // GET: TrialContacts/Create
        public ActionResult Create()
        {
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName");
            ViewBag.RoleId = new SelectList(db.RolesModels, "Id", "RoleName");
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            return View();
        }

        // POST: TrialContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContactId,TrialId,RoleId")] TrialContactsModels trialContactsModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialContactsModels.Add(trialContactsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", trialContactsModels.ContactId);
            ViewBag.RoleId = new SelectList(db.RolesModels, "Id", "RoleName", trialContactsModels.RoleId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialContactsModels.TrialId);
            return View(trialContactsModels);
        }

        // GET: TrialContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialContactsModels trialContactsModels = db.TrialContactsModels.Find(id);
            if (trialContactsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", trialContactsModels.ContactId);
            ViewBag.RoleId = new SelectList(db.RolesModels, "Id", "RoleName", trialContactsModels.RoleId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialContactsModels.TrialId);
            return View(trialContactsModels);
        }

        // POST: TrialContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContactId,TrialId,RoleId")] TrialContactsModels trialContactsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialContactsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", trialContactsModels.ContactId);
            ViewBag.RoleId = new SelectList(db.RolesModels, "Id", "RoleName", trialContactsModels.RoleId);
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialContactsModels.TrialId);
            return View(trialContactsModels);
        }

        // GET: TrialContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialContactsModels trialContactsModels = db.TrialContactsModels.Find(id);
            if (trialContactsModels == null)
            {
                return HttpNotFound();
            }
            return View(trialContactsModels);
        }

        // POST: TrialContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialContactsModels trialContactsModels = db.TrialContactsModels.Find(id);
            db.TrialContactsModels.Remove(trialContactsModels);
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
