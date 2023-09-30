using UdemyProje.Utility;

namespace UdemyProje.Models
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
    {
        private  UygulamaDbContext _uygulamaDbContext;
        public KiralamaRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Kiralama Kiralama)
        {
            _uygulamaDbContext.Update(Kiralama);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
