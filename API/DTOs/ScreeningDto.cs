using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class ScreeningDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ScreeningTime { get; set; }
        public ICollection<SpectatorDto> Spectators { get; set; }
    }
}