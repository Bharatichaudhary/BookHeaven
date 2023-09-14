using System.ComponentModel.DataAnnotations;
using WebApplication4.data;

namespace WebApplication4.Models
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }
        [Display(Name="Name")]
        [Required(ErrorMessage ="Name can't be empty")]
        public string Name { get; set; }
        public string Publication { get; set; }
        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Publication can't be empty")]


        public Genre Genre { get; set; }
        [Required(ErrorMessage = "Select any one Genre")]

        public int PreferredAge { get; set; }
        [Display(Name = "Summary")]

        public string Summary { get; set; }
        public double Price { get; set; }
        [Display(Name = "Photo")]
        [Required(ErrorMessage = "Photo must be submitted")]

        public string Photos { get; set; }




    }
}
