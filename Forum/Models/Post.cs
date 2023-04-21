using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Post harus lebih dari 3 karakter atau kurang dari 50 karakter")]
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
        ICollection<Respon> Respons { get; set; }
    }
}
