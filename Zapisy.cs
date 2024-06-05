using System.ComponentModel.DataAnnotations;

namespace SzkoleniaAPI
{
    public class Zapisy
    {
        [Required]
        public string Tytul { get; set; }
        [Required]
        [MaxLength(25)]
        public string Imie { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nazwisko { get; set; }

    }
}
