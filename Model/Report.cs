using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace marvelHub.Model;

public class Report : Auditable
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(TypeName = "varchar"), StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    public virtual Post? Post { get; set; }

    public virtual Comment? Comment { get; set; }

    [InverseProperty("Report")]
    public virtual User? User { get; set; }
}