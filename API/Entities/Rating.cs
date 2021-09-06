namespace API.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public Show Show { get; set; }
        public int ShowId { get; set; }
    }
}