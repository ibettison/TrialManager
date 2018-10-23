using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trialmanager.Models;

namespace Trialmanager.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contacts
        public ActionResult Index()
        {
            var contactsModels = db.ContactsModels.Include(c => c.ContactStatusName);
            return View(contactsModels.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
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
            return View(contactsModels);
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
            if (ModelState.IsValid)
            {
                db.ContactsModels.Add(contactsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactStatusId = new SelectList(db.ContactStatusModels, "Id", "ContactStatusName", contactsModels.ContactStatusId);
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
            ViewBag.ContactStatusId = new SelectList(db.ContactStatusModels, "Id", "ContactStatusName", contactsModels.ContactStatusId);
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
            ViewBag.ContactStatusId = new SelectList(db.ContactStatusModels, "Id", "ContactStatusName", contactsModels.ContactStatusId);
            return View(contactsModels);
        }


        public List<string> GetUserFromAD(string name) //Get Network Users (AD)
        {
            //ArrayList searchUser = new ArrayList();
            var domainContext = new PrincipalContext(ContextType.Domain);
            var groupPrincipal = GroupPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, "Domain Users");
            UserPrincipal user = new UserPrincipal(domainContext);
            user.Enabled = true;
            user.Name = name + "*";

            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = user;

            PrincipalSearchResult<Principal> results = pS.FindAll();
            var list = new List<string>();

            
            //searchUser.Add(results);
            foreach (var item in results)
            {
                var users = new List<string>();
                //users.UserLoginName = item.SamAccountName;
                //users.UserDisplayName = item.DisplayName;
                //list.Add(users);
                //searchUser.Add(users);
            }

            return list;

        }


    // GET: Contacts/Delete/5
    public ActionResult Delete(int? id)
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
            return View(contactsModels);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactsModels contactsModels = db.ContactsModels.Find(id);
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
