using ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Model
{
    public class BaseEntity
    {
        [MyKey]
        public int Id { get; set; }
    }
}
