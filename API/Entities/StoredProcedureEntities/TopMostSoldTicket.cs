using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.StoredProcedureEntities
{
    public class TopMostSoldTicket
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int ScreeningId { get; set; }
        public int TicketsSold { get; set; }
    }
}