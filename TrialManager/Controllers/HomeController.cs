﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Trialmanager.Models;
using TrialManager.Models;
using WebGrease.Css.Ast.Selectors;

namespace TrialManager.Controllers
{
    
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
                ViewBag.DidNotStart = 0;
                
                foreach (var trial in trialFeasibilityModels)
                {
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
                            if (trialLateDevelopmentModels != null || (setupComplete != null && setupComplete.Completed == "Yes"))
                            {
                                ViewBag.Late++;
                            }
                            else
                            {
                                ViewBag.Setup++;
                            }
                        }
                        else
                        {
                            ViewBag.Setup++;
                        }
                    }
                    else
                    {
                        if (trialStartedModels != null)
                        {
                            if (trialStartedModels.Started == "No")
                            {
                                ViewBag.DidNotStart++;
                            }
                            else
                            {
                                //must equal Null so count as Feasibility
                                ViewBag.Feasibility++;
                            }

                        }
                        else
                        {
                            //must equal Null so count as Feasibility
                            ViewBag.Feasibility++;
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
                        Commercial = trial.Commercial,
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
                    trialList.Add(homeView);
                }
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
    }
}