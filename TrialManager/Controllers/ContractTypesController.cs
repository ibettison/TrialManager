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

namespace TrialManager.Controllers
{
    public class ContractTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractTypes
        public ActionResult Index()
        {
            return View(db.ContractTypesModels.Where(c => c.Deleted == null).ToList());
        }

        // GET: ContractTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContractTypeName")] ContractTypesModels contractTypesModels)
        {
            if (ModelState.IsValid)
            {
                db.ContractTypesModels.Add(contractTypesModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractTypesModels);
        }

        // GET: ContractTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractTypesModels contractTypesModels = db.ContractTypesModels.Find(id);
            if (contractTypesModels == null)
            {
                return HttpNotFound();
            }
            return View(contractTypesModels);
        }

        // POST: ContractTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContractTypeName")] ContractTypesModels contractTypesModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractTypesModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractTypesModels);
        }

        [HttpPost]
        public ActionResult DeleteContract(ContractTypesModels model)
        {
            var id = model.Id;
            ContractTypesModels contractTypesModels = db.ContractTypesModels.Find(id);
            contractTypesModels.Deleted = DateTime.Now;
            db.ContractTypesModels.AddOrUpdate(contractTypesModels);
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
