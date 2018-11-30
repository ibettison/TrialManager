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
    public class PhaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Phase
        public ActionResult Index()
        {
            return View(db.PhaseModels.Where(p => p.Deleted == null).ToList());
        }

        // GET: Phase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhaseName")] PhaseModels phaseModels)
        {
            if (ModelState.IsValid)
            {
                db.PhaseModels.Add(phaseModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phaseModels);
        }

        // GET: Phase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhaseModels phaseModels = db.PhaseModels.Find(id);
            if (phaseModels == null)
            {
                return HttpNotFound();
            }
            return View(phaseModels);
        }

        // POST: Phase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhaseName")] PhaseModels phaseModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phaseModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phaseModels);
        }

        // POST: Phase/Delete/5
        [HttpPost]
        public ActionResult DeletePhase(PhaseModels model)
        {
            var id = model.Id;
            PhaseModels phaseModels = db.PhaseModels.Find(id);
            phaseModels.Deleted = DateTime.Now;
            db.PhaseModels.AddOrUpdate(phaseModels);
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
