using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UdemyProje.Models
{
    public class ApplicationUser :IdentityUser
    {
        [Required]
        public int Ogrencino { get; set; }
        public string Adres { get; set; }
        public string fakulte { get; set; }
        public string Boum { get; set; }
    }
}
