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
    public class GrantTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GrantType
        public ActionResult Index()
        {
            return View(db.GrantTypeModels.ToList());
        }

        // GET: GrantType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrantTypeModels grantTypeModels = db.GrantTypeModels.Find(id);
            if (grantTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(grantTypeModels);
        }

        // GET: GrantType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GrantType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GrantTypeName")] GrantTypeModels grantTypeModels)
        {
            if (ModelState.IsValid)
            {
                db.GrantTypeModels.Add(grantTypeModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grantTypeModels);
        }

        // GET: GrantType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrantTypeModels grantTypeModels = db.GrantTypeModels.Find(id);
            if (grantTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(grantTypeModels);
        }

        // POST: GrantType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GrantTypeName")] GrantTypeModels grantTypeModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grantTypeModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grantTypeModels);
        }

        // GET: GrantType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrantTypeModels grantTypeModels = db.GrantTypeModels.Find(id);
            if (grantTypeModels == null)
            {
                return HttpNotFound();
            }
            return View(grantTypeModels);
        }

        // POST: GrantType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GrantTypeModels grantTypeModels = db.GrantTypeModels.Find(id);
            db.GrantTypeModels.Remove(grantTypeModels);
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
