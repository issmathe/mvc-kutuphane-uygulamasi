using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography;
using UdemyProje.Models;
using UdemyProje.Utility;

namespace UdemyProje.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)] // normalde admin girecekti ancak html ekranında https://localhost:7212/Account/Login?ReturnUrl=%2Fkitapturu yazan birisiinin bu sayfaya erişmesini engelledik
    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public KitapTuruController(IKitapTuruRepository context) {
        _kitapTuruRepository = context;
        }
        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList= _kitapTuruRepository.GetAll().ToList();//datadan çektik
            return View(objKitapTuruList);//datadan gelenleri view index e gönderdik
        }

        public IActionResult Ekle()
        {
             return View();
        }
        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
          // aşağıdaki kod ile validationsuz yöntemle hata mesajı yazdırdık.
             if (ModelState.IsValid)
             {
                _kitapTuruRepository.Ekle(kitapTuru);
                _kitapTuruRepository.Kaydet ();//bunu yazmazsak veriler veri tabanına eklenmez
                TempData["basarili"] = "Yeni Kitap Türü Başarı İle Eklendi";
                return RedirectToAction("Index", "KitapTuru");
             } 
             return View(); 
        }

        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(u=>u.Id==id);
            if (kitapTuruVt == null)
            {
                return NotFound(kitapTuruVt);
            }
            return View(kitapTuruVt); // Pass the KitapTuru object to the view
        }

        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {
            // aşağıdaki kod ile validationsuz yöntemle hata mesajı yazdırdık.
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Guncelle(kitapTuru);
                _kitapTuruRepository.Kaydet();//bunu yazmazsak veriler veri tabanına eklenmez
                TempData["basarili"] = "Yeni Kitap Türü Başarı İle Güncellendi";
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }
        //get işlemi yaptık
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(u => u.Id == id);
            if (kitapTuruVt == null)
            {
                return NotFound(kitapTuruVt);
            }
            return View(kitapTuruVt); // Pass the KitapTuru object to the view
        }
        //post işlemi yapıyoruz
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            KitapTuru? kitapTuru = _kitapTuruRepository.Get(u=>u.Id==id);
            if(kitapTuru == null)
            {   
                return NotFound();
            }
            _kitapTuruRepository.Sil(kitapTuru);
            _kitapTuruRepository.Kaydet();
            TempData["basarili"] = "Silme İşlemi Başarılı";
            return RedirectToAction("Index", "KitapTuru");
        }


    }
}
