using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrialManager.Models;

namespace TrialManager.Controllers
{
    [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contacts
        public ActionResult Index()
        {
            var contactsModels = db.ContactsModels.Include(c => c.ContactStatusName);
            return View(contactsModels.ToList());
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.ContactStatusId = new SelectList(db.ContactStatusModels, "Id", "ContactStatusName");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,ContactName,UserId,Organisation,Telephone,Email,ContactStatusId,ContactNotes")] ContactsModels contactsModels)
        {
            if (contactsModels.UserId == null)
            {
                //lets try to find the userId from the UserName
                string userName = contactsModels.ContactName;
                string email = contactsModels.Email.ToLower();
                email = email.Replace("ncl", "newcastle");
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "campus.ncl.ac.uk"))
                {
                    PrincipalSearcher search = new PrincipalSearcher();
                    UserPrincipal user = new UserPrincipal(ctx);
                    user.Enabled = true;
                    user.DisplayName = userName;
                    user.EmailAddress = email;
                    search.QueryFilter = user;
                    PrincipalSearchResult<Principal> results = search.FindAll();

                    if(results.Count() == 1)
                    {
                        //have definitely found the user and can now save the users ID
                        contactsModels.UserId = results.FirstOrDefault().SamAccountName;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                db.ContactsModels.Add(contactsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactStatusId = new SelectList(db.ContactStatusModels.Where(s => s.Deleted == null), "Id", "ContactStatusName", contactsModels.ContactStatusId);
            return View(contactsModels);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactsModels contactsModels = db.ContactsModels.Find(id);
            if (contactsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactStatusId = new SelectList(db.ContactStatusModels.Where(s => s.Deleted == null), "Id", "ContactStatusName", contactsModels.ContactStatusId);
            return View(contactsModels);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,ContactName,UserId,Organisation,Telephone,Email,ContactStatusId,ContactNotes")] ContactsModels contactsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactStatusId = new SelectList(db.ContactStatusModels.Where(s => s.Deleted == null), "Id", "ContactStatusName", contactsModels.ContactStatusId);
            return View(contactsModels);
        }

        [HttpPost]
        public ActionResult DeleteContact(ContactsModels model)
        {
            // if deleting a contact then we need to remove it also from
            // TrialContactsModels
            // and ContactTrialGroupModels
            // Thus removing the contact from all trials but also from
            // the access table for all trials.
            var trialContactsModels = (from tc in db.TrialContactsModels
                where tc.ContactId == model.Id
                select tc).ToList();

            foreach (var tcm in trialContactsModels)
            {
                db.TrialContactsModels.Remove(tcm);
            }

            var contactTrialGroupModels = (from c in db.ContactTrialGroupModels
                where c.ContactId == model.Id
                select c).ToList();

            foreach (var ctg in contactTrialGroupModels)
            {
                db.ContactTrialGroupModels.Remove(ctg);
            }

            ContactsModels contactsModels = db.ContactsModels.Find(model.Id);
            db.ContactsModels.Remove(contactsModels);
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
