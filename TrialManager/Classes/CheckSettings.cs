using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using TrialManager.Models;


namespace TrialManager.Classes
{
    public class CheckSettings
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public bool GetSettings(string user)
        {
            var trialSettings = (from s in db.SettingsModels
                    join c in db.ContactsModels on s.ContactId equals c.Id
                    where c.UserId == user
                    select s).FirstOrDefault();

            //if there is no setting record then create one with deafaults
            if (trialSettings == null)
            {
                //now need to find the conatct record for the logged in user
                var contactUser = (from c in db.ContactsModels
                                   where c.UserId == user
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
                    trialSettings = (from s in db.SettingsModels
                        join c in db.ContactsModels on s.ContactId equals c.Id
                        where c.UserId == user
                        select s).FirstOrDefault();
                }
                
            }
            if (trialSettings != null)
            {
                HideClosed = trialSettings.HideClosed;
                IconType = trialSettings.IconType;
                MiniMenu = trialSettings.MiniMenu;
                QuickLinks = trialSettings.QuickLinks;
                ShowDashStages = trialSettings.ShowDashStages;
            }
            return true;
        }
      
        public string HideClosed { get; set; }
        public string IconType { get; set; }
        public string MiniMenu { get; set; }
        public string QuickLinks { get; set; }
        public string ShowDashStages { get; set; }

    }
    
}