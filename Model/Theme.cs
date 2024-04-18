using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace marvelHub.Model
{
    public class Theme
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar"), StringLength(255)]
        public string Type { get; set; } = string.Empty;

        [Column(TypeName = "varchar"), StringLength(255)]
        public string MarvelId { get; set; } = string.Empty;

        [InverseProperty("Theme")]
        public virtual ICollection<Post>? Post { get; set; }
    }
}
