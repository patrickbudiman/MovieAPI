using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public string description { get; set; }
        [Range(1, 10)]
        public double rating { get; set; }
        public string image { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
