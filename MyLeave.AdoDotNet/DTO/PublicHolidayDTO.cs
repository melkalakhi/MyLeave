using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLeave.AdoDotNet.DTO
{
    public abstract class PublicHolidayDTO : EntityDTO
    {
        public int Day { get; set; }

        public int Month { get; set; }
    }
}
