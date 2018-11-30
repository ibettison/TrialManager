using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using Trialmanager.Models;
using TrialManager.Classes;
using TrialManager.Models;

namespace Trialmanager.Controllers
{
    public class TrialFeasibilityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrialFeasibility
        [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
        public ActionResult Index()
        {

            var trialFeasibilityModels = (from fease in db.TrialFeasibilityModels
                join setup in db.TrialSetupModels on fease.Id equals setup.TrialId
                select fease).ToList();
            ViewBag.Trial = trialFeasibilityModels.Count > 0 ? trialFeasibilityModels : null;
            //db.TrialFeasibilityModels.Include(t => t.DiseaseTherapyAreaName).Include(t => t.GrantTypeName).Include(t => t.PhaseName).Include(t => t.TrialTypeName);
            return View();
        }

        // GET: TrialFeasibility/Details/5
        [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(id);
            if (trialFeasibilityModels == null)
            {
                return HttpNotFound();
            }
            return View(trialFeasibilityModels);
        }

        // GET: TrialFeasibility/Create
        [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
        public ActionResult Create()
        {
            ViewBag.Title = "New Trial";
            ViewBag.Small = "Create a New trial";
            ViewBag.Link = "Dashboard";
            ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id", "DiseaseTherapyAreaName");
            ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(g => g.Deleted == null), "Id", "GrantTypeName");
            ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(p => p.Deleted == null), "Id", "PhaseName");
            ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName");
            return View();
        }

        // POST: TrialFeasibility/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,ShortName,TrialTitle,ApplicationReference,BhNumber,TrialTypeId,Commercial,PhaseId,SampleSize,GrantTypeId,FundingStream,GrantDeadlineDate,UniConsultancyAgreement,NhsConsultancyAgreement,DiseaseTherapyAreaId")] TrialFeasibilityModels trialFeasibilityModels)
        {
            ViewBag.Title = "New Trial";
            ViewBag.Small = "Create a New trial";
            ViewBag.Link = "Dashboard";

            if (ModelState.IsValid)
            {
                
                db.TrialFeasibilityModels.Add(trialFeasibilityModels);
                db.SaveChanges();
               return RedirectToAction("CreateGroupAccess", new {trialFeasibilityModels.ShortName});
            }
            ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id", "DiseaseTherapyAreaName", trialFeasibilityModels.DiseaseTherapyAreaId);
            ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(g => g.Deleted == null), "Id", "GrantTypeName", trialFeasibilityModels.GrantTypeId);
            ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(p => p.Deleted == null), "Id", "PhaseName", trialFeasibilityModels.PhaseId);
            ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName", trialFeasibilityModels.TrialTypeId);
            return View(trialFeasibilityModels);
        }

        [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
        public ActionResult CreateGroupAccess( string shortName)
        {
            var groupName = (from s in db.TrialFeasibilityModels
                where s.ShortName == shortName
                select s).FirstOrDefault();

            if (groupName != null)
            {
                var groupRecord = new TrialGroupModels
                {
                    GroupName = groupName.ShortName
                };
                if (ModelState.IsValid)
                {
                    db.TrialGroupModels.Add(groupRecord);
                    db.SaveChanges();
                    int groupId = groupRecord.Id;
                    var trialRecord = new TrialGroupTrialModels
                    {
                        TrialId = groupName.Id,
                        TrialGroupId = groupId
                    };
                    if (ModelState.IsValid)
                    {
                        db.TrialGroupTrialModels.Add(trialRecord);
                        db.SaveChanges();
                    }
                    var contactId = (from c in db.ContactsModels
                                     where c.UserId == User.Identity.Name
                                     select c).FirstOrDefault();
                    var contactGroup = new ContactTrialGroupModels
                    {
                        TrialGroupId = groupId,
                        ContactId = contactId.Id,
                        ReadOnly = false
                    };
                    if (ModelState.IsValid)
                    {
                        db.ContactTrialGroupModels.Add(contactGroup);
                        db.SaveChanges();
                    }
                }
                
            }
            
            return RedirectToAction("Index","Home");
        }


        // GET: TrialFeasibility/Edit/5
        [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //check to see if the trial is availble to the user for editing
            var trialAccess = (from tgt in db.TrialGroupTrialModels
                join tg in db.TrialGroupModels on tgt.TrialGroupId equals tg.Id
                where tgt.TrialId == id
                select tgt).ToList();

            var accessGranted = false;
            foreach (var groupId in trialAccess)
            {
                var contactFound = (from ct in db.ContactTrialGroupModels
                    where groupId.TrialGroupId == ct.TrialGroupId && ct.ContactName.UserId == User.Identity.Name
                    select ct).FirstOrDefault();

                if (contactFound != null)
                {
                    accessGranted = true;
                }

            }

            if (!accessGranted)
            {
                return RedirectToAction("NotAuthorised");
            }

            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(id);
           // TrialRecordsModels trialRecordsModels = db.TrialRecordsModels.Find(id);
            if (trialFeasibilityModels == null)
            {
                return HttpNotFound();
            }
            var contactsRole = (from c in db.TrialContactsModels
                where c.TrialId == id
                select c).ToList();
            ViewBag.contactsRole = contactsRole.Count > 0 ? contactsRole : null;

            var notes = (from c in db.NotesModels
                         where c.TrialId == id
                         select c).ToList();
            ViewBag.notifications = notes.Count > 0 ? notes : null;

            var reminders = (from r in db.TrialRemindersModels
                             where r.TrialId == id
                             select r).Distinct().OrderByDescending(d => d.DueDate).ToList();
            ViewBag.reminders = reminders.Count > 0 ? reminders : null;

            var changedRecords = (from c in db.TrialRecordsModels
                where c.TrialId == id
                select c).ToList();
            ViewBag.changed = changedRecords.Count > 0 ? changedRecords : null;

            var progress = (from p in db.TrialStartedModels
                where p.TrialId == id
                select p).ToList();
            ViewBag.progress = progress.Count > 0 ? progress : null;

            var setupComplete = (from sc in db.TrialSetupCompleteModels
                where sc.TrialId == id
                select sc).ToList();
            ViewBag.setupComplete = setupComplete.Count > 0 ? setupComplete : null;

            ViewBag.User = User.Identity.Name;
            ViewBag.Id = id;
            ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id", "DiseaseTherapyAreaName", trialFeasibilityModels.DiseaseTherapyAreaId);
            ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(t => t.Deleted == null), "Id", "GrantTypeName", trialFeasibilityModels.GrantTypeId);
            ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(t => t.Deleted == null), "Id", "PhaseName", trialFeasibilityModels.PhaseId);
            ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName", trialFeasibilityModels.TrialTypeId);

            ViewBag.Contacts = db.ContactsModels;
            ViewBag.Roles = db.RolesModels.Where(t => t.Deleted == null);
            ViewBag.DocumentTypes = db.DocumentTypesModels.Where(t => t.Deleted == null);

            //before displaying the Trial lets save it to the recent Trials table
            var recentTrials = (from r in db.RecentTrialsModels
                                where r.TrialId == id && User.Identity.Name == r.UserId
                                select r).FirstOrDefault();
            if (recentTrials != null)
            {
                //need to Update the date of access
                recentTrials.LastAccessed = DateTime.Now;
                recentTrials.Access = "Edit";
                db.Entry(recentTrials).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                //need to create a new record as this is the first access of this trial.
                var recentTrialsModels = new RecentTrialsModels
                {
                    TrialId = id,
                    LastAccessed = DateTime.Now,
                    UserId = User.Identity.Name,
                    Access = "Edit"
                };
                db.RecentTrialsModels.Add(recentTrialsModels);
                db.SaveChanges();
            }

            return View(trialFeasibilityModels);
        }

        // POST: TrialFeasibility/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ShortName,TrialTitle,ApplicationReference,BhNumber,TrialTypeId,Commercial,PhaseId,SampleSize,GrantTypeId,FundingStream,GrantDeadlineDate,UniConsultancyAgreement,NhsConsultancyAgreement,DiseaseTherapyAreaId")] TrialFeasibilityModels trialFeasibilityModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trialFeasibilityModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id", "DiseaseTherapyAreaName", trialFeasibilityModels.DiseaseTherapyAreaId);
            ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(t => t.Deleted == null), "Id", "GrantTypeName", trialFeasibilityModels.GrantTypeId);
            ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(t => t.Deleted == null), "Id", "PhaseName", trialFeasibilityModels.PhaseId);
            ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName", trialFeasibilityModels.TrialTypeId);
            return View(trialFeasibilityModels);
        }

        public ActionResult NotAuthorised()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxEdit(AjaxViewModels model)
        {
            var fn = model.FieldName;
            var nv = model.NewValue;
            switch (model.Controller)
            {
                case "TrialFeasibility":
                    TrialFeasibilityModels fModel = db.TrialFeasibilityModels.Find(model.Id);
                    
                    if (fModel != null)
                    {
                        var getVar = fModel.GetType().GetProperty(fn);
                        if (getVar != null)
                        {
                            if (getVar.PropertyType.Name == "Int32")
                            {
                                getVar.SetValue(fModel, Convert.ToInt32(nv));
                            }
                            else
                            {
                                var property = getVar.PropertyType.GenericTypeArguments.FirstOrDefault();
                                if (property != null && property.Name == "DateTime")
                                {
                                    getVar.SetValue(fModel, Convert.ToDateTime(nv));
                                }
                                else
                                {
                                    getVar.SetValue(fModel, Convert.ToString(nv));
                                }
                            }
                        }
                    }
                    db.Entry(fModel).State = EntityState.Modified;
                    break;
                case "TrialSetup":
                    var sModel = (from s in db.TrialSetupModels
                                  where s.TrialId == model.TrialId
                                  select s).FirstOrDefault();
                    if (sModel != null)
                    {
                        var getVar = sModel.GetType().GetProperty(fn);
                        if (getVar != null)
                        {
                            if (getVar.PropertyType.Name == "Int32")
                            {
                                getVar.SetValue(sModel, Convert.ToInt32(nv));
                            }
                            else
                            {
                                var property = getVar.PropertyType.GenericTypeArguments.FirstOrDefault();
                                if (property != null && property.Name == "DateTime")
                                {
                                    getVar.SetValue(sModel, Convert.ToDateTime(nv));
                                }
                                else
                                {
                                    getVar.SetValue(sModel, Convert.ToString(nv));
                                }
                            }
                        }
                    }
                    db.Entry(sModel).State = EntityState.Modified;
                    break;
                case "TrialLateDevelopment":
                    var lModel = (from s in db.TrialLateDevelopmentModels
                                  where s.TrialId == model.TrialId
                                  select s).FirstOrDefault();
                    if (lModel != null)
                    {
                        var getVar = lModel.GetType().GetProperty(fn);
                        if (getVar != null)
                        {
                            if (getVar.PropertyType.Name == "Int32")
                            {
                                getVar.SetValue(lModel, Convert.ToInt32(nv));
                            }
                            else
                            {
                                var property = getVar.PropertyType.GenericTypeArguments.FirstOrDefault();

                                if (property != null && property.Name == "DateTime")
                                {
                                    getVar.SetValue(lModel, Convert.ToDateTime(nv));
                                }
                                else
                                {
                                    getVar.SetValue(lModel, Convert.ToString(nv));
                                }
                                
                            }

                        }
                    }
                    db.Entry(lModel).State = EntityState.Modified;
                    break;
                case "TrialActive":
                    var aModel = (from a in db.TrialActiveModels
                                  where a.TrialId == model.TrialId
                                  select a).FirstOrDefault();
                    if (aModel != null)
                    {
                        var getVar = aModel.GetType().GetProperty(fn);
                        if (getVar != null)
                        {
                            if (getVar.PropertyType.Name == "Int32")
                            {
                                getVar.SetValue(aModel, Convert.ToInt32(nv));
                            }
                            else
                            {
                                var property = getVar.PropertyType.GenericTypeArguments.FirstOrDefault();

                                if (property != null && property.Name == "DateTime")
                                {
                                    getVar.SetValue(aModel, Convert.ToDateTime(nv));
                                }
                                else
                                {
                                    getVar.SetValue(aModel, Convert.ToString(nv));
                                }
                            }
                        }
                    }
                    db.Entry(aModel).State = EntityState.Modified;
                    break;
                case "TrialClosed":
                    TrialCloseDownModels cModel = db.TrialCloseDownModels.Find(model.Id);

                    if (cModel != null)
                    {
                        var getVar = cModel.GetType().GetProperty(fn);
                        if (getVar != null)
                        {
                            if (getVar.PropertyType.Name == "Int32")
                            {
                                getVar.SetValue(cModel, Convert.ToInt32(nv));
                            }
                            else
                            {
                                var property = getVar.PropertyType.GenericTypeArguments.FirstOrDefault();
                                if (property != null && property.Name == "DateTime")
                                {
                                    getVar.SetValue(cModel, Convert.ToDateTime(nv));
                                }
                                else
                                {
                                    getVar.SetValue(cModel, Convert.ToString(nv));
                                }
                            }
                        }
                    }
                    db.Entry(cModel).State = EntityState.Modified;
                    break;
            }
            Claim claimFirstName;
            Claim claimSurName;
            var name = GetFullName(out claimFirstName, out claimSurName);
            if (ModelState.IsValid)
            {
                var record = new TrialRecordsModels
                {
                    DateChanged = DateTime.Now,
                    FieldName = model.FieldName,
                    OriginalValue = model.Original,
                    NewValue = model.NewValue,
                    ReasonForChange = model.Reason,
                    TrialId = model.Id,
                    WhoChanged = name
                };
                db.TrialRecordsModels.Add(record);
                db.SaveChanges();
            }
            return RedirectToAction("ListAddedRecords", new {model.Id} );
        }

        private string GetFullName(out Claim claimFirstName, out Claim claimSurName)
        {
            claimFirstName = ((ClaimsIdentity) User.Identity).FindFirst(ClaimTypes.GivenName);
            var firstName = claimFirstName.Value;
            claimSurName = ((ClaimsIdentity) User.Identity).FindFirst(ClaimTypes.Surname);
            var surName = claimSurName.Value;
            var name = $"{firstName} {surName}";
            return name;
        }

        public ActionResult ListAddedRecords(int? id)
        {
            var changedRecords = (from c in db.TrialRecordsModels
                                  where c.TrialId == id
                                  select c).ToList();
            ViewBag.changed = changedRecords.Count > 0 ? changedRecords : null;

            return PartialView("ListRecords");
        }

        [HttpPost]
        public ActionResult ListContacts(TrialContactsModels model)
        {

            if (ModelState.IsValid)
            {
                var newContact = new TrialContactsModels
                {
                    ContactId = model.ContactId,
                    RoleId = model.RoleId,
                    TrialId = model.TrialId
                };
                db.TrialContactsModels.Add(newContact);
                db.SaveChanges();
                var trial = (from t in db.TrialFeasibilityModels
                    where t.Id == model.TrialId
                    select t).FirstOrDefault();
                if (trial == null) throw new ArgumentNullException(nameof(trial));
                //create a link to the trial group for the new contact
                var trialGroup = (from g in db.TrialGroupModels
                    where g.GroupName == trial.ShortName
                    select g).FirstOrDefault();
                if (trialGroup == null) throw new ArgumentNullException(nameof(trialGroup));
                //check if access already exists as it's lowest access would be Read Only
                var checkGroup = (from cg in db.ContactTrialGroupModels
                    where cg.ContactId == model.ContactId && cg.TrialGroupId == trialGroup.Id
                    select cg).FirstOrDefault();
                if (checkGroup == null)
                {
                    var addToGroup = new ContactTrialGroupModels
                    {
                        ContactId = model.ContactId,
                        TrialGroupId = trialGroup.Id,
                        ReadOnly = true
                    };
                    db.ContactTrialGroupModels.Add(addToGroup);
                    db.SaveChanges();
                }
                
            }
            return RedirectToAction("ShowUpdatedContacts", new {Id=model.TrialId});
        }

        public ActionResult ShowUpdatedContacts(int? Id)
        {
            var contactsRole = (from c in db.TrialContactsModels
                                where c.TrialId == Id
                                select c).ToList();

            ViewBag.contactsRole = contactsRole.Count > 0 ? contactsRole : null;
            ViewBag.Contacts = db.ContactsModels;
            ViewBag.Roles = db.RolesModels.Where(r => r.Deleted == null);
            return PartialView("_ListContacts");
        }

        [HttpPost]
        public ActionResult ListNotes(NotesModels model)
        {
            Claim claimFirstName;
            Claim claimSurName;
            var name = GetFullName(out claimFirstName, out claimSurName);
            if (ModelState.IsValid)
            {
                var newNote = new NotesModels
                {
                    Who = name,
                    Message = model.Message,
                    DateTime = DateTime.Now,
                    TrialId = model.TrialId
                };
                db.NotesModels.Add(newNote);
                db.SaveChanges();
            }
            return RedirectToAction("ShowUpdatedNotes", new { Id = model.TrialId });
        }

        public ActionResult ShowUpdatedNotes(int? Id)
        {
            var notes = (from c in db.NotesModels
                                where c.TrialId == Id
                                select c).ToList();
            ViewBag.notifications = notes.Count > 0 ? notes : null;
            return PartialView("_ListNotes");
        }

        [HttpPost]
        public ActionResult ListReminders(TrialRemindersModels model)
        {

            if (ModelState.IsValid)
            {
                var newReminder = new TrialRemindersModels()
                {
                    Reminder = model.Reminder,
                    DueDate = model.DueDate,
                    TrialId = model.TrialId
                };
                db.TrialRemindersModels.Add(newReminder);
                db.SaveChanges();
            }
            return RedirectToAction("ShowUpdatedReminders", new { Id = model.TrialId });
        }

        public ActionResult ShowUpdatedReminders(int? Id)
        {
            var reminders = (from c in db.TrialRemindersModels
                         where c.TrialId == Id
                         select c).ToList();
            ViewBag.reminders = reminders.Count > 0 ? reminders : null;
            return PartialView("_ListReminders");
        }
        public ActionResult ShowSetup(int? id)
        {

            var trialSetupModels = (from ts in db.TrialSetupModels
                where ts.TrialId == id
                select ts).FirstOrDefault();
            var setupId = (from s in db.TrialSetupModels
                where s.TrialId == id
                select s).FirstOrDefault();
            if (setupId != null)
            {
                ViewBag.setupId = setupId.Id;
            }
            var setupComplete = (from sc in db.TrialSetupCompleteModels
                                 where sc.TrialId == id
                                 select sc).ToList();
            ViewBag.setupComplete = setupComplete.Count > 0 ? setupComplete : null;
            //check to see what access the user has for this Trial.
            var checkAccess = new CheckTrialAccess();
            var accessValue = checkAccess.GetAccess(User.Identity.Name, id);

            if (trialSetupModels == null)
            {
                ViewBag.Id = id;
                ViewBag.TrialLocationId = new SelectList(db.TrialLocationModels.Where(t => t.Deleted == null), "Id", "Location");
                ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id",
                    "DiseaseTherapyAreaName");
                ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(t => t.Deleted == null), "Id", "GrantTypeName");
                ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(t => t.Deleted == null), "Id", "PhaseName");
                ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName");
                if (accessValue == "View")
                {
                    return PartialView("Setup/_SetupNewView", trialSetupModels);
                }
                if(accessValue == "Edit")
                {
                    return PartialView("Setup/_SetupNewRecord", trialSetupModels);
                }
                
            }
            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(id);
            ViewBag.TrialLocationId = new SelectList(db.TrialLocationModels.Where(t => t.Deleted == null), "Id", "Location", trialSetupModels.TrialLocationId);
            ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id", "DiseaseTherapyAreaName", trialFeasibilityModels.DiseaseTherapyAreaId);
            ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(t => t.Deleted == null), "Id", "GrantTypeName", trialFeasibilityModels.GrantTypeId);
            ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(t => t.Deleted == null), "Id", "PhaseName", trialFeasibilityModels.PhaseId);
            ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName", trialFeasibilityModels.TrialTypeId);
            if (accessValue == "View")
            {
                return PartialView("Setup/_SetupEditView", trialSetupModels);
            }
            if (accessValue == "Edit")
            {
               return PartialView("Setup/_SetupEditRecord", trialSetupModels); 
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult ShowLateDevelopment(int? id)
        {
            var trialLateDevelopmentModels = (from ts in db.TrialLateDevelopmentModels
                                    where ts.TrialId == id
                                    select ts).FirstOrDefault();
            var trialLateDevId = (from s in db.TrialLateDevelopmentModels
                           where s.TrialId == id
                           select s).FirstOrDefault();
            if (trialLateDevId != null)
            {
                ViewBag.lateDevId = trialLateDevId.Id;
            }

            //check to see what access the user has for this Trial.
            var checkAccess = new CheckTrialAccess();
            var accessValue = checkAccess.GetAccess(User.Identity.Name, id);

            if (trialLateDevelopmentModels == null)
            {
                ViewBag.Id = id;
                ViewBag.TrialLocationId = new SelectList(db.TrialLocationModels.Where(t => t.Deleted == null), "Id", "Location");
                ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id",
                    "DiseaseTherapyAreaName");
                ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(t => t.Deleted == null), "Id", "GrantTypeName");
                ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(t => t.Deleted == null), "Id", "PhaseName");
                ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName");
                if (accessValue == "View")
                {
                    return PartialView("LateDev/_LateDevNewView", trialLateDevelopmentModels);
                }
                if (accessValue == "Edit")
                {
                    return PartialView("LateDev/_LateDevNewRecord", trialLateDevelopmentModels);
                }
                
            }
            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(id);
            ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id", "DiseaseTherapyAreaName", trialFeasibilityModels.DiseaseTherapyAreaId);
            ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(t => t.Deleted == null), "Id", "GrantTypeName", trialFeasibilityModels.GrantTypeId);
            ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(t => t.Deleted == null), "Id", "PhaseName", trialFeasibilityModels.PhaseId);
            ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName", trialFeasibilityModels.TrialTypeId);
            if (accessValue == "View")
            {
                return PartialView("LateDev/_LateDevEditView", trialLateDevelopmentModels);
            }
            if (accessValue == "Edit")
            {
                return PartialView("LateDev/_LateDevEditRecord", trialLateDevelopmentModels);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult ShowActive(int? id)
        {
            var trialActiveModels = (from a in db.TrialActiveModels
                where a.TrialId == id
                select a).FirstOrDefault();

            //check to see what access the user has for this Trial.
            var checkAccess = new CheckTrialAccess();
            var accessValue = checkAccess.GetAccess(User.Identity.Name, id);
            if (trialActiveModels == null)
            {
                ViewBag.Id = id;
                ViewBag.StatusId = new SelectList(db.ActiveStatusModels, "Id", "StatusName");
                if (accessValue == "View")
                {
                    return PartialView("Active/_ActiveNewView");
                }
                if (accessValue == "Edit")
                {
                    return PartialView("Active/_ActiveNewRecord");
                }

            }
            ViewBag.Id = id;
            ViewBag.StatusId = new SelectList(db.ActiveStatusModels, "Id", "StatusName", trialActiveModels.StatusId);
            if (accessValue == "View")
            {
                return PartialView("Active/_ActiveEditView", trialActiveModels);
            }
            if (accessValue == "Edit")
            {
                return PartialView("Active/_ActiveEditRecord", trialActiveModels);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult ShowCloseDown(int? id)
        {
            var trialCloseDownModels = (from a in db.TrialCloseDownModels
                                     where a.TrialId == id
                                     select a).FirstOrDefault();

            //check to see what access the user has for this Trial.
            var checkAccess = new CheckTrialAccess();
            var accessValue = checkAccess.GetAccess(User.Identity.Name, id);
            if (trialCloseDownModels == null)
            {
                ViewBag.Id = id;
                if (accessValue == "View")
                {
                    return PartialView("Closed/_ClosedNewView");
                }
                if (accessValue == "Edit")
                {
                    return PartialView("Closed/_ClosedNewRecord");
                }

            }
            ViewBag.Id = id;
            if (accessValue == "View")
            {
                return PartialView("Closed/_ClosedEditView", trialCloseDownModels);
            }
            if (accessValue == "Edit")
            {
                return PartialView("Closed/_ClosedEditRecord", trialCloseDownModels);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult ShowDocuments(int? id)
        {

            var trialDocumentsModels = (from td in db.TrialDocumentsModels
                where td.TrialId == id
                select td).ToList();
            ViewBag.Documents = trialDocumentsModels.Count > 0 ? trialDocumentsModels:null;
            ViewBag.DocumentTypes = db.DocumentTypesModels.Where(t => t.Deleted == null);
            return PartialView("_ListDocuments");
        }

        [HttpPost]
        public ActionResult AddDocument(TrialDocumentsModels model)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = model.UploadFile;
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    if (fileName != null)
                    {
                        var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                        file.SaveAs(path);
                        model.DocumentLink = path;
                        model.DocumentFileName = fileName;
                    }
                    var newDoc = new TrialDocumentsModels()
                        {
                            DateTime = DateTime.Now,
                            UploadedBy = User.Identity.Name,
                            TrialId = model.TrialId,
                            DocumentFileName = model.DocumentFileName,
                            DocumentLink = model.DocumentLink,
                            DocumentVersion = model.DocumentVersion,
                            DocumentDescription = model.DocumentDescription,
                            DocumentType = model.DocumentType
                        };
                    db.TrialDocumentsModels.Add(newDoc);
                    db.SaveChanges();
                }

               
            }
            return RedirectToAction("Edit", new { Id = model.TrialId });
        }

        public ActionResult ChangeTrialTitle(TrialFeasibilityModels model)
        {
            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(model.Id);
            if (trialFeasibilityModels != null)
            {
                trialFeasibilityModels.TrialTitle = model.TrialTitle;
                db.TrialFeasibilityModels.AddOrUpdate(trialFeasibilityModels);
                db.SaveChanges();
            }
            
            return PartialView("ShowProgressView");
        }

        public ActionResult ChangeCommercial(TrialFeasibilityModels model)
        {
            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(model.Id);
            if (trialFeasibilityModels != null)
            {
                trialFeasibilityModels.Commercial = model.Commercial;
                db.TrialFeasibilityModels.AddOrUpdate(trialFeasibilityModels);
                db.SaveChanges();
            }

            return PartialView("ShowProgressView");
        }

        public ActionResult ChangeAgreement(TrialFeasibilityModels model)
        {
            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(model.Id);
            if (trialFeasibilityModels != null)
            {
                trialFeasibilityModels.UniConsultancyAgreement = model.UniConsultancyAgreement;
                trialFeasibilityModels.NhsConsultancyAgreement = model.NhsConsultancyAgreement;
                db.TrialFeasibilityModels.AddOrUpdate(trialFeasibilityModels);
                db.SaveChanges();
            }

            return PartialView("ShowProgressView");
        }

        public ActionResult DownloadFile(string fileName)
        {
            byte[] fileData = System.IO.File.ReadAllBytes(fileName);
            string contentType = MimeMapping.GetMimeMapping(fileName);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = Path.GetFileName(fileName),
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(fileData, contentType);
        }

        public ActionResult AddProgress(TrialStartedModels model)
        {

            if (ModelState.IsValid)
            {
                var newTrialStart = new TrialStartedModels()
                {
                    Reason = model.Reason,
                    DateTime = DateTime.Now,
                    Started = model.Started,
                    TrialId = model.TrialId
                };
            }

            return RedirectToAction("DisplayProgress", new { Id = model.TrialId });
        }

        public ActionResult DisplayProgress()
        {
            return PartialView("ShowProgressView");
        }
        public ActionResult AddProgressSetup(TrialSetupCompleteModels model)
        {

            if (ModelState.IsValid)
            {
                var newSetupComplete = new TrialSetupCompleteModels()
                {
                    Reason = model.Reason,
                    DateTime = DateTime.Now,
                    Completed = model.Completed,
                    TrialId = model.TrialId
                };
                db.TrialSetupCompleteModels.Add(newSetupComplete);
                db.SaveChanges();
            }

            return RedirectToAction("DisplayProgress", new { Id = model.TrialId });
        }
        // GET: TrialFeasibility/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(id);
            if (trialFeasibilityModels == null)
            {
                return HttpNotFound();
            }
            return View(trialFeasibilityModels);
        }

        [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors, NTRF_AUTO_MC_TrialManager_Membership")]
        public ActionResult ViewTrial(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //check to see if the trial is availble to the user for editing
            var trialAccess = (from tgt in db.TrialGroupTrialModels
                               join tg in db.TrialGroupModels on tgt.TrialGroupId equals tg.Id
                               where tgt.TrialId == id
                               select tgt).ToList();

            var accessGranted = false;
            foreach (var groupId in trialAccess)
            {
                var contactFound = (from ct in db.ContactTrialGroupModels
                                    where groupId.TrialGroupId == ct.TrialGroupId && ct.ContactName.UserId == User.Identity.Name
                                    select ct).FirstOrDefault();

                if (contactFound != null)
                {
                    accessGranted = true;
                }

            }

            if (!accessGranted)
            {
                return RedirectToAction("NotAuthorised");
            }

            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(id);
            // TrialRecordsModels trialRecordsModels = db.TrialRecordsModels.Find(id);
            if (trialFeasibilityModels == null)
            {
                return HttpNotFound();
            }
            var contactsRole = (from c in db.TrialContactsModels
                                where c.TrialId == id
                                select c).ToList();
            ViewBag.contactsRole = contactsRole.Count > 0 ? contactsRole : null;

            var notes = (from c in db.NotesModels
                         where c.TrialId == id
                         select c).ToList();
            ViewBag.notifications = notes.Count > 0 ? notes : null;

            var reminders = (from r in db.TrialRemindersModels
                             where r.TrialId == id
                             select r).Distinct().OrderByDescending(d => d.DueDate).ToList();
            ViewBag.reminders = reminders.Count > 0 ? reminders : null;

            var changedRecords = (from c in db.TrialRecordsModels
                                  where c.TrialId == id
                                  select c).ToList();
            ViewBag.changed = changedRecords.Count > 0 ? changedRecords : null;

            var progress = (from p in db.TrialStartedModels
                            where p.TrialId == id
                            select p).ToList();
            ViewBag.progress = progress.Count > 0 ? progress : null;

            var setupComplete = (from sc in db.TrialSetupCompleteModels
                                 where sc.TrialId == id
                                 select sc).ToList();
            ViewBag.setupComplete = setupComplete.Count > 0 ? setupComplete : null;

            ViewBag.User = User.Identity.Name;
            ViewBag.Id = id;
            ViewBag.DiseaseTherapyAreaId = new SelectList(db.DiseaseTherapyAreaModels.Where(t => t.Deleted == null), "Id", "DiseaseTherapyAreaName", trialFeasibilityModels.DiseaseTherapyAreaId);
            ViewBag.GrantTypeId = new SelectList(db.GrantTypeModels.Where(t => t.Deleted == null), "Id", "GrantTypeName", trialFeasibilityModels.GrantTypeId);
            ViewBag.PhaseId = new SelectList(db.PhaseModels.Where(t => t.Deleted == null), "Id", "PhaseName", trialFeasibilityModels.PhaseId);
            ViewBag.TrialTypeId = new SelectList(db.TrialTypeModels.Where(t => t.Deleted == null), "Id", "TrialTypeName", trialFeasibilityModels.TrialTypeId);

            ViewBag.Contacts = db.ContactsModels;
            ViewBag.Roles = db.RolesModels;
            ViewBag.DocumentTypes = db.DocumentTypesModels;
            //before displaying the Trial lets save it to the recent Trials table
            var recentTrials = (from r in db.RecentTrialsModels
                where r.TrialId == id && User.Identity.Name == r.UserId
                select r).FirstOrDefault();
            if (recentTrials != null)
            {
                //need to Update the date of access
                recentTrials.LastAccessed = DateTime.Now;
                recentTrials.Access = "View";
                db.Entry(recentTrials).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var recentTrialsModels = new RecentTrialsModels
                {
                    TrialId = id,
                    LastAccessed = DateTime.Now,
                    UserId = User.Identity.Name,
                    Access = "View"
                };
                db.RecentTrialsModels.Add(recentTrialsModels);
                db.SaveChanges();
            }
            
            return View(trialFeasibilityModels);
        }

        [HttpPost]
        public ActionResult ChangeReminderStatus(DeleteReminderViewModels model)
        {
            var remStat = db.TrialRemindersModels.Find(model.Id);
            if (remStat == null) return RedirectToAction("Edit", new {id = model.TrialId});
            if (model.Delete)
            {
                db.TrialRemindersModels.Remove(remStat);
            }
            else
            {
                remStat.Checked = !remStat.Checked;
                db.Entry(remStat).State = EntityState.Modified;
            }
            
            db.SaveChanges();
            return RedirectToAction("ShowReminderStatus", new {model.TrialId});
        }

        
        public ActionResult ShowReminderStatus(DeleteReminderViewModels model)
        {
            var reminders = (from r in db.TrialRemindersModels
                             where r.TrialId == model.TrialId
                             select r).Distinct().OrderBy(d => d.DueDate).ToList();
            ViewBag.reminders = reminders.Count > 0 ? reminders : null;
            return PartialView("_ListReminders");
        }

        // POST: TrialFeasibility/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrialFeasibilityModels trialFeasibilityModels = db.TrialFeasibilityModels.Find(id);
            db.TrialFeasibilityModels.Remove(trialFeasibilityModels);
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
