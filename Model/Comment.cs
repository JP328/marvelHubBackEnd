using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace marvelHub.Model
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar"), StringLength(255)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "varchar"), StringLength(255)]
        public string UserId { get; set; } = string.Empty;

        [InverseProperty("Comment")]
        public virtual ICollection<Report>? Report { get; set; }

        public virtual Post? Post { get; set; }
    }
}
