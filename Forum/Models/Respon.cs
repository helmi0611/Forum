using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Respon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username harus lebih dari 3 karakter atau kurang dari 50 karakter")]
        public string Comment { get; set; }
        public Post Post { get; set; }
    }
}