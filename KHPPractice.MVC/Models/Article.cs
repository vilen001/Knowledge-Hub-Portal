using KHP.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace KHPPractice.MVC.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Url]
        public string URL { get; set; }
        public string? Description { get; set; }
        public string? PostedBy { get; set; }
        public DateTime DateSubmmited { get; set; }
        public int CatagoryId { get; set; }
        public Catagory? Catagory { get; set; }
        public bool IsApproved { get; set; }

    }
}
