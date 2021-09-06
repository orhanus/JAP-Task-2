using System.Collections.Generic;

namespace API.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string NameLastname { get; set; }
        public ICollection<Show> Shows { get; set; }
    }
}