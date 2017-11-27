using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLeave.AdoDotNet.DTO
{
    public class LeaveDTO : EntityDTO
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Retourne le nombre de jour du congé
        /// </summary>
        public int Days 
        {
            get
            {
                return EndDate != null ? ((TimeSpan)((DateTime)EndDate - StartDate)).Days : 0;
            }
            //private set; 
        }
    }
}
