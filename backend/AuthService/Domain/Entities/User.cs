﻿using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}