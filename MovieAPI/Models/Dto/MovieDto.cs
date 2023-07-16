namespace MovieAPI.Models.Dto
{
    public class MovieDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public string image { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
