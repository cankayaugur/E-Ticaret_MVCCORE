﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Dtos
{
    public class OrderDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public CartDto Cart { get; set; }

    }
}
