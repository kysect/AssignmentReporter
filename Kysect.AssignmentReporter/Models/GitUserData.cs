﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models
{
    public class GitUserData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public GitUserData(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
