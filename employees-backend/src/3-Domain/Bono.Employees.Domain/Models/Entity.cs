using System;
using System.Collections.Generic;
using System.Text;

namespace Bono.Employees.Domain.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; protected set; }

        public void SetDeleted(bool IsDeleted)
        {
            this.IsDeleted = IsDeleted;
        }
    }
}
