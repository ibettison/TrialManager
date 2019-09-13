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
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
    public class StaffPassportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StaffPassport
        public ActionResult Index()
        {
            List<StaffPassportViewModels> passportList = new List<StaffPassportViewModels>();
            var staffPassportModels = db.StaffPassportModels.Include(s => s.AccessName).Include(s => s.ContactName).Include(s => s.ContractTypeName);
            foreach (var passport in staffPassportModels)
            {
                var today = DateTime.Today;
                var monthsDiff = (passport.ResearchPassportRenewal - today).Days;
                var lessThanNinetyDays = 0;
                var lessThan180Days = 0;
                if (monthsDiff <= 90)
                {
                    lessThan180Days = 1;
                }
                else
                {
                    if (monthsDiff <= 180)
                    {
                        lessThanNinetyDays = 1;
                    }
                }
                var record = new StaffPassportViewModels()
                {
                    Id = passport.Id,
                    ContactName = passport.ContactName.ContactName,
                    ContractType = passport.ContractTypeName.ContractTypeName,
                    ContractEndDate = passport.ContractEndDate,
                    ResearchPassportExpiry = passport.ResearchPassportExpiry,
                    ResearchPassportRenewal = passport.ResearchPassportRenewal,
                    TypeofAccess = passport.AccessName.AccessName,
                    AccessExpiryDate = passport.AccessExpiryDate,
                    Renewal90 = lessThanNinetyDays,
                    Renewal180 = lessThan180Days
                };
                passportList.Add(record);
            }
            return View(passportList);
        }

        // GET: StaffPassport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffPassportModels staffPassportModels = db.StaffPassportModels.Find(id);
            if (staffPassportModels == null)
            {
                return HttpNotFound();
            }
            return View(staffPassportModels);
        }

        // GET: StaffPassport/Create
        public ActionResult Create()
        {
            ViewBag.TypeofAccessId = new SelectList(db.AccessTypesModels.Where(d => d.Deleted == null), "Id", "AccessName");
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName");
            ViewBag.ContractTypeId = new SelectList(db.ContractTypesModels.Where(d => d.Deleted == null), "Id", "ContractTypeName");
            return View();
        }

        // POST: StaffPassport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContactId,ContractTypeId,ContractEndDate,ResearchPassportExpiry,ResearchPassportRenewal,TypeofAccessId,AccessExpiryDate")] StaffPassportModels staffPassportModels)
        {
            if (ModelState.IsValid)
            {
                db.StaffPassportModels.Add(staffPassportModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeofAccessId = new SelectList(db.AccessTypesModels.Where(d => d.Deleted == null), "Id", "AccessName", staffPassportModels.TypeofAccessId);
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", staffPassportModels.ContactId);
            ViewBag.ContractTypeId = new SelectList(db.ContractTypesModels.Where(d => d.Deleted == null), "Id", "ContractTypeName", staffPassportModels.ContractTypeId);
            return View(staffPassportModels);
        }

        // GET: StaffPassport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffPassportModels staffPassportModels = db.StaffPassportModels.Find(id);
            
                                        
            if (staffPassportModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeofAccessId = new SelectList(db.AccessTypesModels.Where(d => d.Deleted == null), "Id", "AccessName", staffPassportModels.TypeofAccessId);
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", staffPassportModels.ContactId);
            ViewBag.ContractTypeId = new SelectList(db.ContractTypesModels.Where(d => d.Deleted == null), "Id", "ContractTypeName", staffPassportModels.ContractTypeId);
            return View(staffPassportModels);
        }

        // POST: StaffPassport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContactId,ContractTypeId,ContractEndDate,ResearchPassportExpiry,ResearchPassportRenewal,TypeofAccessId,AccessExpiryDate")] StaffPassportModels staffPassportModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffPassportModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeofAccessId = new SelectList(db.AccessTypesModels.Where(d => d.Deleted == null), "Id", "AccessName", staffPassportModels.TypeofAccessId);
            ViewBag.ContactId = new SelectList(db.ContactsModels, "Id", "ContactName", staffPassportModels.ContactId);
            ViewBag.ContractTypeId = new SelectList(db.ContractTypesModels.Where(d => d.Deleted == null), "Id", "ContractTypeName", staffPassportModels.ContractTypeId);
            return View(staffPassportModels);
        }

        // GET: StaffPassport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffPassportModels staffPassportModels = db.StaffPassportModels.Find(id);
            if (staffPassportModels == null)
            {
                return HttpNotFound();
            }
            return View(staffPassportModels);
        }

        // POST: StaffPassport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffPassportModels staffPassportModels = db.StaffPassportModels.Find(id);
            db.StaffPassportModels.Remove(staffPassportModels);
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
