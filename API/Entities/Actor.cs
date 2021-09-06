using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Actors")]
    public class Actor
    {
        public int Id { get; set; }
        public string NameLastname { get; set; }
        public ICollection<Show> Shows { get; set; }
    }
}