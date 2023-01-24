using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anyar.Models;

public class Team
{
    public int Id { get; set; }

    public int Order { get; set; }

    [Required(ErrorMessage ="Ad mutleq qeyd olunmalidir"), StringLength(maximumLength:30,MinimumLength =3,ErrorMessage ="Min 3 Max 30 element ola biler ")]
    public string Fullname { get; set; }

    [Required(ErrorMessage = "Pozisiya mutleq qeyd olunmalidir"), StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Min 3 Max 30 element ola biler ")]
    public string Position { get; set; }

    [Required(ErrorMessage = "Pozisiya mutleq qeyd olunmalidir"), StringLength(maximumLength: 256, ErrorMessage = " Max 256 element ola biler ")]
    public string Description { get; set; }
    
    public string? Image { get; set; }

    [NotMapped]
    public IFormFile?  ImageFile { get; set; }

    public string? InstagramUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? LinkedinUrl { get; set; }

}

