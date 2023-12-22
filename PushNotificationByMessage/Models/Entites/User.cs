using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PushNotificationByMessage.Models.Entites
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public string Name { get; set; }
        public string? NameCompany { get; set; }
        public string? Address { get; set; }

    }
}