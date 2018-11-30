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
    public class ContactTrialGroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContactTrialGroup
        public ActionResult Index()
        {
            var contactTrialGroupModels = db.ContactTrialGroupModels.Include(c => c.ContactName).Include(c => c.GroupName);
            return View(contactTrialGroupModels.ToList());
        }


        // GET: ContactTrialGroup/Create
        public ActionResult Create()
        {
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName");
            ViewBag.TrialGroupId = new SelectList(db.TrialGroupModels, "Id", "GroupName");
            return View();
        }

        // POST: ContactTrialGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrialGroupId,ContactId,ReadOnly")] ContactTrialGroupModels contactTrialGroupModels)
        {
            if (ModelState.IsValid)
            {
                db.ContactTrialGroupModels.Add(contactTrialGroupModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", contactTrialGroupModels.ContactId);
            ViewBag.TrialGroupId = new SelectList(db.TrialGroupModels, "Id", "GroupName", contactTrialGroupModels.TrialGroupId);
            return View(contactTrialGroupModels);
        }

        // GET: ContactTrialGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactTrialGroupModels contactTrialGroupModels = db.ContactTrialGroupModels.Find(id);
            if (contactTrialGroupModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", contactTrialGroupModels.ContactId);
            ViewBag.TrialGroupId = new SelectList(db.TrialGroupModels, "Id", "GroupName", contactTrialGroupModels.TrialGroupId);
            return View(contactTrialGroupModels);
        }

        // POST: ContactTrialGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrialGroupId,ContactId,ReadOnly")] ContactTrialGroupModels contactTrialGroupModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactTrialGroupModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", contactTrialGroupModels.ContactId);
            ViewBag.TrialGroupId = new SelectList(db.TrialGroupModels, "Id", "GroupName", contactTrialGroupModels.TrialGroupId);
            return View(contactTrialGroupModels);
        }

        [HttpPost]
        public ActionResult DeleteAccess(ContactTrialGroupModels Model)
        {
            var id = Model.Id;
            ContactTrialGroupModels contactTrialGroupModels = db.ContactTrialGroupModels.Find(id);
            db.ContactTrialGroupModels.Remove(contactTrialGroupModels);
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
