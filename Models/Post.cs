using System.ComponentModel.DataAnnotations;

namespace DT191G___Moment_2.Models
{
    public class Post
    {
        // Properties
        [Required(ErrorMessage = "Skriv inlägg med mellan 3 och 100 tecken")]
        [MaxLength(100, ErrorMessage = "Skriv inlägg med max 100 tecken")]
        [MinLength(3, ErrorMessage = "Skriv inlägg med minst 3 tecken")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Inloggad användare saknas")]
        public string Author { get; set; }

        //[Required]
        public DateTime Created { get; set; }

        // Tom constructor
        public Post()
        {
            Created = DateTime.Now;
        }

        // Constructor
        public Post(string text, string author)
        {
            Text = text;
            Author = author;
            Created = DateTime.Now;
        }
    }
}