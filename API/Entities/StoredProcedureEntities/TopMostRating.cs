using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.StoredProcedureEntities
{
    public class TopMostRating
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public int NumberOfRatings { get; set; }
        public double AverageRating { get; set; }
    }
}