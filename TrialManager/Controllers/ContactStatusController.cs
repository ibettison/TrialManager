using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trialmanager.Models;

namespace Trialmanager.Controllers
{
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
    public class ContactStatusController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: ContactStatus
        public ActionResult Index()
        {
            return View(_db.ContactStatusModels.Where(s => s.Deleted == null).ToList());
        }

        // GET: ContactStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContactStatusName")] ContactStatusModels contactStatusModels)
        {
            if (ModelState.IsValid)
            {
                _db.ContactStatusModels.Add(contactStatusModels);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactStatusModels);
        }

        // GET: ContactStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactStatusModels contactStatusModels = _db.ContactStatusModels.Find(id);
            if (contactStatusModels == null)
            {
                return HttpNotFound();
            }
            return View(contactStatusModels);
        }

        // POST: ContactStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContactStatusName")] ContactStatusModels contactStatusModels)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(contactStatusModels).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactStatusModels);
        }


        // POST: ContactStatus/Delete/5
        [HttpPost]
        public ActionResult DeleteContact(ContactStatusModels model)
        {
            var id = model.Id;
            ContactStatusModels contactStatusModels = _db.ContactStatusModels.Find(id);
            contactStatusModels.Deleted = DateTime.Now;
            _db.ContactStatusModels.AddOrUpdate(contactStatusModels);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
