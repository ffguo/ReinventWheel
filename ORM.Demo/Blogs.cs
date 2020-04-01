using ORM.Attributes;
using ORM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Demo
{
    public class Blogs : BaseEntity
    {
        [LengthValidate(5)]
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public short? Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
