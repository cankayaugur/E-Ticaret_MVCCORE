using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnemliBookStore.Ui.Identity
{
    public class User : IdentityUser //hazır kolonlar getirir, paswordHash userName, Mail vs.. Ekstra ne eklemek istenirse buraya eklenir.
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
