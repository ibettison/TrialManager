using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrialManager.Models;

namespace TrialManager.Controllers
{
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors, NTRF_AUTO_MC_TrialManager_Membership")]
    public class SettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Settings
        public ActionResult Index()
        {
            var settingsModels = db.SettingsModels.Include(s => s.ContactName);
            return View(settingsModels.ToList());
        }

        // GET: Settings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsModels settingsModels = db.SettingsModels.Find(id);
            if (settingsModels == null)
            {
                return HttpNotFound();
            }
            return View(settingsModels);
        }

        // GET: Settings/Create
        public ActionResult Create()
        {
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName");
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContactId,HideClosed,IconType,QuickLinks,MiniMenu,ShowDashStages")] SettingsModels settingsModels)
        {
            if (ModelState.IsValid)
            {
                db.SettingsModels.Add(settingsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", settingsModels.ContactId);
            return View(settingsModels);
        }

        // GET: Settings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsModels settingsModels = db.SettingsModels.Find(id);
            if (settingsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", settingsModels.ContactId);
            return View(settingsModels);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContactId,HideClosed,IconType,QuickLinks,MiniMenu,ShowDashStages")] SettingsModels settingsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(settingsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", settingsModels.ContactId);
            return View(settingsModels);
        }

        // GET: Settings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsModels settingsModels = db.SettingsModels.Find(id);
            if (settingsModels == null)
            {
                return HttpNotFound();
            }
            return View(settingsModels);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SettingsModels settingsModels = db.SettingsModels.Find(id);
            db.SettingsModels.Remove(settingsModels);
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
