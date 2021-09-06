using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Ratings")]
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public Show Show { get; set; }
        public int ShowId { get; set; }
    }
}