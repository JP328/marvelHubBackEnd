using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace marvelHub.Model
{
    public class Post : Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar"), StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "varchar"), StringLength(2000)]
        public string Text { get; set; } = string.Empty;

        [InverseProperty("Post")]
        public virtual ICollection<Comment>? Comment { get; set; }

        public virtual Theme? Theme { get; set; }

        public virtual User? User { get; set; }

    }
}
