using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MovieApi.Model;


public class Actor
{
    [Key]
    public Guid ActorId { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Imie powinno zawierać od 2 do 20 znaków")]
    public string firstName { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Nazwisko powinno zawierać od 2 do 20 znaków")]
    public string lastName { get; set; }
    [JsonIgnore]
    public ICollection<Movie?>? Movies { get; set; }
}