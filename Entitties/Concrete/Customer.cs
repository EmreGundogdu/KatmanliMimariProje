﻿using Entitties.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entitties.Concrete
{
    public class Customer:IEntity
    {
        public string CustomerId { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }

    }
}
