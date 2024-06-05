using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SzkoleniaAPI
{
    public class Szkolenia
    {
        [Required]
        public DateTime Data { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Tytul { get; set; }
        [Required]
        [MaxLength(2000)]
        public string? Opis { get; set; }

    }
}
