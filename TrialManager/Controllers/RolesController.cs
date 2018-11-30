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
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Roles
        public ActionResult Index()
        {
            return View(db.RolesModels.Where(r => r.Deleted == null).ToList());
        }

        
        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleName")] RolesModels rolesModels)
        {
            if (ModelState.IsValid)
            {
                db.RolesModels.Add(rolesModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rolesModels);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesModels rolesModels = db.RolesModels.Find(id);
            if (rolesModels == null)
            {
                return HttpNotFound();
            }
            return View(rolesModels);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleName")] RolesModels rolesModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolesModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rolesModels);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult DeleteRoles(RolesModels model)
        {
            var id = model.Id;
            RolesModels rolesModels = db.RolesModels.Find(id);
            rolesModels.Deleted = DateTime.Now;
            db.RolesModels.AddOrUpdate(rolesModels);
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
