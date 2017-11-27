using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLeave.AdoDotNet.DTO
{
    public class CompanyDTO : EntityDTO
    {
        public string Name { get; set; }

        public DateTime RecruitementDate { get; set; }

        public DateTime? EndOfMissionDate { get; set; }

        public int Days 
        {
            get
            {
                return ((TimeSpan)((EndOfMissionDate ?? DateTime.Now) - RecruitementDate)).Days;
            }
            //private set; 
        }
    }
}
