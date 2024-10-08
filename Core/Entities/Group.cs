﻿using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Group:BaseCoreEntity
    {
        public string Name { get; set; }
        public string ?Description { get; set; }
        public string ?Background {  get; set; }
        public virtual ICollection<GroupMember> ?Members { get; set; }
    }
}
