using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Owin;
using TrialManager.Classes;
using TrialManager.Models;
using WebGrease.Css.Ast.Selectors;

namespace TrialManager.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [AllowAnonymous]
        public ActionResult Index()
        {
            //at this point need to check the progress of all of the projects.
            var trialFeasibilityModels =
                db.TrialFeasibilityModels.Include(t => t.DiseaseTherapyAreaName)
                    .Include(t => t.GrantTypeName)
                    .Include(t => t.PhaseName)
                    .Include(t => t.TrialTypeName).ToList();
            List<HomeViewModels> trialList = new List<HomeViewModels>();
            //check if logged in so not having to replicate all of the counting
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Feasibility = 0;
                ViewBag.Live = 0;
                ViewBag.Setup = 0;
                ViewBag.Late = 0;
                ViewBag.InCloseDown = 0;
                ViewBag.Closed = 0;
                ViewBag.DidNotStart = 0;

                //check the settings for the Dashboard view

                var trialSettings = (from s in db.SettingsModels
                    join c in db.ContactsModels on s.ContactId equals c.Id
                    where c.UserId == User.Identity.Name
                    select s).FirstOrDefault();

                //if there is no setting record then create one with deafaults
                if (trialSettings == null)
                {
                    //now need to find the conatct record for the logged in user
                    var contactUser = (from c in db.ContactsModels
                        where c.UserId == User.Identity.Name
                        select c).FirstOrDefault();
                    if (contactUser != null)
                    {
                        var settingsRecord = new SettingsModels
                        {
                            ContactId = contactUser.Id,
                            HideClosed = "No",
                            IconType = "Male",
                            MiniMenu = "No",
                            QuickLinks = "Yes",
                            ShowDashStages = "Yes"
                        };
                        db.SettingsModels.Add(settingsRecord);
                        db.SaveChanges();
                    }
                    
                }
                else
                {
                    ViewBag.SettingsHideClosed = trialSettings.HideClosed;
                    ViewBag.SettingsIconType = trialSettings.IconType;
                    ViewBag.SettingsMiniMenu = trialSettings.MiniMenu;
                    ViewBag.SettingsQuickLinks = trialSettings.QuickLinks;
                    ViewBag.SettingsShowDashStages = trialSettings.ShowDashStages;
                }
               
                foreach (var trial in trialFeasibilityModels)
                {
                    var stage = "";
                    //Loop through each trial to find which stage it is at.
                    //Check to see if the trial has started after Feasibility?
                    var trialStartedModels = (from s in db.TrialStartedModels
                        where s.TrialId == trial.Id
                        select s).FirstOrDefault();
                    //now find the Chief Investigator of the Trial
                    var contactRole = (from c in db.TrialContactsModels
                        where c.TrialId == trial.Id && c.RoleName.RoleName == "Chief Investigator"
                        select c).FirstOrDefault();
                    var otherRole = (from c in db.TrialContactsModels
                        where c.TrialId == trial.Id && c.RoleName.RoleName == "Principal Investigator"
                        select c).FirstOrDefault();
                    var cI = contactRole != null ? contactRole.ContactName.ContactName : "Unassigned";
                    var pI = otherRole != null ? otherRole.ContactName.ContactName : "Unassigned";
                    //The trial has moved on and now need to check if its only in setUp or has move on further
                    var trialSetupModels = (from s in db.TrialSetupModels
                        where trial.Id == s.TrialId
                        select s).FirstOrDefault();
                    var projectId = trialSetupModels != null ? trialSetupModels.ProjectIdentifier : "Unassigned";
                    var rdNumber = trialSetupModels != null ? trialSetupModels.ResearchDevelopmentId : "Unassigned";
                    if (trialStartedModels != null && trialStartedModels.Started == "Yes")
                    {
                        if (trialSetupModels != null)
                        {
                            //at setup stage
                            var trialLateDevelopmentModels = (from l in db.TrialLateDevelopmentModels
                                where l.TrialId == trial.Id
                                select l).FirstOrDefault();
                            var setupComplete = (from s in db.TrialSetupCompleteModels
                                where s.TrialId == trial.Id && s.Completed == "Yes"
                                select s).FirstOrDefault();
                            var trialCloseDown = (from c in db.TrialCloseDownModels
                                where c.TrialId == trial.Id
                                select c).FirstOrDefault();
                            var activeTrial = (from a in db.TrialActiveModels
                                where a.TrialId == trial.Id
                                select a).FirstOrDefault(); 
                            if ((setupComplete != null && setupComplete.Completed == "Yes"))
                            {
                                if (trialLateDevelopmentModels != null)
                                {
                                    if (trialLateDevelopmentModels.LocalSiteActivationDate != null)
                                    {
                                        if (activeTrial != null)
                                        {
                                            if (trialCloseDown != null)
                                            {
                                                if (trialCloseDown.Archiving != null)
                                                {
                                                    ViewBag.Closed++;
                                                    stage = "Closed";
                                                }
                                                else
                                                {
                                                    if (activeTrial.StatusName.StatusName == "In Close Down")
                                                    {
                                                        ViewBag.InCloseDown++;
                                                        stage = "In Close Down";

                                                    }
                                                    else
                                                    {
                                                        ViewBag.Live++;
                                                        stage = "Live";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (activeTrial.StatusName.StatusName == "In Close Down")
                                                {
                                                        ViewBag.InCloseDown++;
                                                        stage = "In Close Down";
                                                    
                                                }
                                                else
                                                {
                                                    ViewBag.Live++;
                                                    stage = "Live";
                                                }
                                            }
                                            
                                        }
                                        else
                                        {
                                            ViewBag.Live++;
                                            stage = "Live";
                                        }
                                        
                                    }
                                    else
                                    {
                                        ViewBag.Late++;
                                        stage = "Late Development";
                                    }
                                    
                                }
                                else
                                {
                                    ViewBag.Late++;
                                    stage = "Late Development";
                                }
                                
                            }
                            else
                            {
                                ViewBag.Setup++;
                                stage = "In Setup";
                            }
                        }
                        else
                        {
                            ViewBag.Setup++;
                            stage = "In Setup";
                        }
                    }
                    else
                    {
                        if (trialStartedModels != null)
                        {
                            if (trialStartedModels.Started == "No")
                            {
                                ViewBag.DidNotStart++;
                                stage = "Did not Start";
                            }
                            else
                            {
                                //must equal Null so count as Feasibility
                                ViewBag.Feasibility++;
                                stage = "Feasibility";
                            }

                        }
                        else
                        {
                            //must equal Null so count as Feasibility
                            ViewBag.Feasibility++;
                            stage = "Feasibility";
                        }
                    }

                    //check to see if the trial is availble to the user for editing or viewing
                    var trialAccess = (from tgt in db.TrialGroupTrialModels
                        join tg in db.TrialGroupModels on tgt.TrialGroupId equals tg.Id
                        where tgt.TrialId == trial.Id
                        select tgt).ToList();

                    var accessGranted = false;
                    var ro = true;
                    foreach (var groupId in trialAccess)
                    {
                        var contactFound = (from ct in db.ContactTrialGroupModels
                            where groupId.TrialGroupId == ct.TrialGroupId && ct.ContactName.UserId == User.Identity.Name
                            select ct).FirstOrDefault();

                        if (contactFound != null)
                        {
                            accessGranted = true;
                            ro = contactFound.ReadOnly;
                        }

                    }

                    var reminderCount = (from r in db.TrialRemindersModels
                        where r.TrialId == trial.Id
                        select r).ToList();

                    var remCount = 0;
                    foreach (var reminder in reminderCount)
                    {
                        if (!reminder.Checked)
                        {
                            if ((reminder.DueDate - DateTime.Today).Days <= 50)
                            {
                                remCount++;
                            }
                        }

                    }

                    HomeViewModels homeView = new HomeViewModels
                    {
                        ShortName = trial.ShortName,
                        Stage = stage,
                        TrialTypeName = trial.TrialTypeName.TrialTypeName,
                        ProjectId = projectId,
                        ResearchId = rdNumber,
                        CI = cI,
                        PI = pI,
                        Access = accessGranted,
                        ReadOnly = ro,
                        ReminderCount = remCount,
                        TrialId = trial.Id
                    };
                    if (trialSettings != null)
                    {
                        if (trialSettings.HideClosed == "Yes")
                        {
                            if (stage != "Closed")
                            {
                                trialList.Add(homeView);
                            }
                        }
                        else
                        {
                            trialList.Add(homeView);
                        }
                    }
                    else
                    {
                        trialList.Add(homeView);
                    }


                }
                var threeWeeksAgo = DateTime.Now.AddDays(-21);
                var recentDeletes = (from r in db.RecentTrialsModels
                    where User.Identity.Name == r.UserId && r.LastAccessed <= threeWeeksAgo 
                                     select r).ToList();
                foreach (var deletes in recentDeletes)
                {
                    db.RecentTrialsModels.Remove(deletes);
                }
                db.SaveChanges();
                var recentLinks = (from r in db.RecentTrialsModels
                    where User.Identity.Name == r.UserId
                    select r).Distinct().OrderByDescending(d => d.LastAccessed).Take(20).ToList();
                ViewBag.recent = recentLinks.Count > 0 ? recentLinks : null;
            }
            
            return View(trialList.ToList()); 

        }

        [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
        public ActionResult Edit(int? id)
        {
            return RedirectToAction("Edit","TrialFeasibility", new {@id=id});
        }

        [Authorize(Roles = "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors,  NTRF_AUTO_MC_TrialManager_Membership")]
        public ActionResult ViewTrial(int? id)
        {
            return RedirectToAction("ViewTrial", "TrialFeasibility", new { @id = id });
        }

        [Authorize(
            Roles =
                "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors")]
        public ActionResult DisplayMenu()
        {
            ViewBag.PassportOver = 0;
            ViewBag.PassportNearly = 0;
            //check if there are any Staff Passports coming up for renewal.
            var staffPassportModels = db.StaffPassportModels.ToList();

            foreach (var passport in staffPassportModels)
            {
                var today = DateTime.Today;
                var monthsDiff = (passport.ResearchPassportRenewal - today).Days;
                if (monthsDiff <= 90)
                {
                    ViewBag.PassportOver++;
                }
                else
                {
                    if (monthsDiff <= 180)
                    {
                        ViewBag.PassportNearly++;
                    }
                }
            }
            //check the settings for the Dashboard view

            var checkSettings = new CheckSettings();
            var settings = checkSettings.GetSettings(User.Identity.Name);


            ViewBag.SettingsHideClosed = checkSettings.HideClosed;
            ViewBag.SettingsIconType = checkSettings.IconType;
            ViewBag.SettingsMiniMenu = checkSettings.MiniMenu;
            ViewBag.SettingsQuickLinks = checkSettings.QuickLinks;
            ViewBag.SettingsShowDashStages = checkSettings.ShowDashStages;
            return PartialView("_layOutSideBar");
        }

        [Authorize(
            Roles =
                "NTRF_AUTO_MC_TrialManager_Administrators, NTRF_AUTO_MC_TrialManager_Editors,  NTRF_AUTO_MC_TrialManager_Membership")]
        public ActionResult UserSettings()
        {
            //check the settings for the Dashboard view

            var checkSettings = new CheckSettings();
            var settings = checkSettings.GetSettings(User.Identity.Name);


            ViewBag.SettingsHideClosed = checkSettings.HideClosed;
            ViewBag.SettingsIconType = checkSettings.IconType;
            ViewBag.SettingsMiniMenu = checkSettings.MiniMenu;
            ViewBag.SettingsQuickLinks = checkSettings.QuickLinks;
            ViewBag.SettingsShowDashStages = checkSettings.ShowDashStages;

            return PartialView("_layOutSettings");
        }



    }
}