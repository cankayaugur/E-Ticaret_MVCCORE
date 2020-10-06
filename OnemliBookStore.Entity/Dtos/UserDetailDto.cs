using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Dtos
{
    public class UserDetailDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Phone { get; set; }
        public IList<string> SelectedRoles { get; set; }
    }
}
