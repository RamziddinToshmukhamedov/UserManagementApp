﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementApp.Models
{
    public class UserDetailsResponse
    {
        public UserDetails Data { get; set; }
        public bool Success { get; set; }
    }
}
