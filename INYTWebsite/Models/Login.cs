﻿using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LoginDate { get; set; }
    }
}
