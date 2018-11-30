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
    public class AccessTypesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: AccessTypes
        public ActionResult Index()
        {
            var accessTypesList = (from a in _db.AccessTypesModels
                where a.Deleted == null
                select a).ToList();
            return View(accessTypesList);
        }

        // GET: AccessTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccessName")] AccessTypesModels accessTypesModels)
        {
            if (ModelState.IsValid)
            {
                _db.AccessTypesModels.Add(accessTypesModels);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accessTypesModels);
        }

        // GET: AccessTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessTypesModels accessTypesModels = _db.AccessTypesModels.Find(id);
            if (accessTypesModels == null)
            {
                return HttpNotFound();
            }
            return View(accessTypesModels);
        }

        // POST: AccessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccessName")] AccessTypesModels accessTypesModels)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(accessTypesModels).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accessTypesModels);
        }

        [HttpPost]
        public ActionResult DeleteAccessTypes(AccessTypesModels Model)
        {
            var id = Model.Id;
            AccessTypesModels accessTypesModels = _db.AccessTypesModels.Find(id);
            accessTypesModels.Deleted = DateTime.Now;
            _db.AccessTypesModels.AddOrUpdate(accessTypesModels);
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
