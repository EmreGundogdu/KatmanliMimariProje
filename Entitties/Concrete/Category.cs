﻿using Entitties.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entitties.Concrete
{
    public class Category:IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
