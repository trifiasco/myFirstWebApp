﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp.Entity
{
    public class Contact
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string DisplayName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
    }
}