﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class BaseQuerieResponse<T>
    {
        public int PageIndex { get; set; }  
        public int PageSize { get; set; }
        public int Total { get; set; }
        public List<T>? Items { get; set; }
    }
}
