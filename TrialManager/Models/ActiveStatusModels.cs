﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class ActiveStatusModels
    {
        [Key]
        public int Id { get; set; }

        public string StatusName { get; set; }

    }
}