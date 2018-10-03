using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trialmanager.Controllers
{
    public class TrialManagementController : Controller
    {
        // GET: TrialManagement
        public ActionResult Index()
        {
            ViewBag.Title = "Create New Trial";
            ViewBag.Small = "Add details to create a new trial record.";
            ViewBag.link = "Dashboard";
            return View();
        }

        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }
    }
}