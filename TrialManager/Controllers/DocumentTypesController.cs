using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrialManager.Models;

namespace TrialManager.Controllers
{
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
    public class DocumentTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DocumentTypes
        public ActionResult Index()
        {
            return View(db.DocumentTypesModels.Where(d => d.Deleted == null).ToList());
        }

        // GET: DocumentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName")] DocumentTypesModels documentTypesModels)
        {
            if (ModelState.IsValid)
            {
                db.DocumentTypesModels.Add(documentTypesModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentTypesModels);
        }

        // GET: DocumentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentTypesModels documentTypesModels = db.DocumentTypesModels.Find(id);
            if (documentTypesModels == null)
            {
                return HttpNotFound();
            }
            return View(documentTypesModels);
        }

        // POST: DocumentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName")] DocumentTypesModels documentTypesModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentTypesModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentTypesModels);
        }
        
        // POST: DocumentTypes/Delete/5
        [HttpPost]
        public ActionResult DeleteDoctypes(DocumentTypesModels model)
        {
            var id = model.Id;
            DocumentTypesModels documentTypesModels = db.DocumentTypesModels.Find(id);
            documentTypesModels.Deleted = DateTime.Now;
            db.DocumentTypesModels.AddOrUpdate(documentTypesModels);
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
