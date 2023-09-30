
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdemyProje.Models;
//veri tabanında ent.freamw. tablo için model sınıfı eklenen yer
namespace UdemyProje.Utility
{
    public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }
        public DbSet<KitapTuru>KitapTurleri { get; set; }
        public DbSet<Kitap>Kitaplar { get; set; }
        public DbSet<Kiralama> Kiralamalar { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }    
    }
}
