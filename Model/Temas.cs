using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace marvelHub.Model
{
    public class Temas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string marvelId { get; set; } = string.Empty;

        [InverseProperty("Tema")]
        public virtual ICollection<Postagem>? Postagem { get; set; }
    }
}
