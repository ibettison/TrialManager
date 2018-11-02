using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trialmanager.Models;

namespace TrialManager.Classes
{
    public class CheckTrialAccess
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public string GetAccess(string user, int? trialId)
        {
            var contactId = (from c in db.ContactsModels
                where c.UserId == user
                select c).FirstOrDefault();

            var groupAccess = (from g in db.TrialGroupTrialModels
                where g.TrialId == trialId
                select g).FirstOrDefault();

            var trialContact = (from tc in db.ContactTrialGroupModels
                where tc.ContactId == contactId.Id &&
                      tc.TrialGroupId == groupAccess.Id
                select tc).FirstOrDefault();

            if (trialContact != null) {
                return trialContact.ReadOnly ? "View" : "Edit";
                
            }
            return "No Access";
        }
    }
}