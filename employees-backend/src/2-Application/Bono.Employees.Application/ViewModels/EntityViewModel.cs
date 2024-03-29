﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bono.Employees.Application.ViewModels
{
    public class EntityViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
