using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Shared
{
    public class BaseEntity
    {
        public int? CreatedUserId { get; set; }

        public int? LastUpdatedUserId { get; set; }


        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        //public User CreatedUser { get; set; }

        //public User LastUpdatedUser { get; set; }
    }
}
