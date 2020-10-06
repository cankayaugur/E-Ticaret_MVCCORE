using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OnemliBookStore.Ui.Identity;
 
namespace OnemliBookStore.Entity.Dtos
{
    public class RoleDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class RoleDetails
    {
        public IdentityRole Role { get; set; }
        public IList<User> Members { get; set; }
        public IList<User> NonMembers { get; set; }
    }

    public class RoleEdit
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToRemove { get; set; }
    }
}
