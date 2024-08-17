﻿using Domain.Common;

namespace Domain.Customer
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
