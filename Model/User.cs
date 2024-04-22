using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marvelHub.Model;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(TypeName = "varchar"), StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar"), StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Column(TypeName ="varchar"), StringLength(255)]
    public string Password { get; set; } = string.Empty;

    [Column(TypeName = "varchar"), StringLength(255)]
    public string? Photo { get; set; } = string.Empty;

    //[Column(TypeName = "bit")]
    //public bool isAdmin { get; set; } = false;


    [InverseProperty("User")]
    public virtual ICollection<Post>? Post { get; set; }
}
