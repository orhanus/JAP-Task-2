using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.StoredProcedureEntities
{
    public class TopMostScreening
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int ScreeningCount { get; set; }
    }
}