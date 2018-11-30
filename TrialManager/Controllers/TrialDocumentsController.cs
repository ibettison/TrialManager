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
    public class TrialDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialDocuments
        public ActionResult Index()
        {
            var trialDocumentsModels = db.TrialDocumentsModels.Include(t => t.ShortName).Include(t => t.TypeName);
            return View(trialDocumentsModels.ToList());
        }

        // GET: TrialDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialDocumentsModels trialDocumentsModels = db.TrialDocumentsModels.Find(id);
            if (trialDocumentsModels == null)
            {
                return HttpNotFound();
            }
            return View(trialDocumentsModels);
        }

        // GET: TrialDocuments/Create
        public ActionResult Create()
        {
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName");
            ViewBag.DocumentType = new SelectList(db.DocumentTypesModels, "Id", "TypeName");
            return View();
        }

        // POST: TrialDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateChanged,UploadedBy,DocumentLink,DocumentVersion,DocumentType,DocumentDescription,TrialId")] TrialDocumentsModels trialDocumentsModels)
        {
            if (ModelState.IsValid)
            {
                db.TrialDocumentsModels.Add(trialDocumentsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialDocumentsModels.TrialId);
            ViewBag.DocumentType = new SelectList(db.DocumentTypesModels, "Id", "TypeName", trialDocumentsModels.DocumentType);
            return View(trialDocumentsModels);
        }

        // GET: TrialDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialDocumentsModels trialDocumentsModels = db.TrialDocumentsModels.Find(id);
            if (trialDocumentsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialDocumentsModels.TrialId);
            ViewBag.DocumentType = new SelectList(db.DocumentTypesModels, "Id", "TypeName", trialDocumentsModels.DocumentType);
            return View(trialDocumentsModels);
        }

        // POST: TrialDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateChanged,UploadedBy,DocumentLink,DocumentVersion,DocumentType,DocumentDescription,TrialId")] TrialDocumentsModels trialDocumentsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialDocumentsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrialId = new SelectList(db.TrialFeasibilityModels, "Id", "ShortName", trialDocumentsModels.TrialId);
            ViewBag.DocumentType = new SelectList(db.DocumentTypesModels, "Id", "TypeName", trialDocumentsModels.DocumentType);
            return View(trialDocumentsModels);
        }

        // GET: TrialDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialDocumentsModels trialDocumentsModels = db.TrialDocumentsModels.Find(id);
            if (trialDocumentsModels == null)
            {
                return HttpNotFound();
            }
            return View(trialDocumentsModels);
        }

        // POST: TrialDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialDocumentsModels trialDocumentsModels = db.TrialDocumentsModels.Find(id);
            db.TrialDocumentsModels.Remove(trialDocumentsModels);
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
