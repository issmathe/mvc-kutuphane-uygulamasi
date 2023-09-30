using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using UdemyProje.Models;
using UdemyProje.Utility;

namespace UdemyProje.Controllers
{

    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public KitapController(IKitapRepository kitapRepository,IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment )
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Index()
        {
            List<Kitap> objKitapList= _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();//datadan çektik
           
            return View(objKitapList);//datadan gelenleri view index e gönderdik
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString(),
                });
            ViewBag.KitapTuruList= KitapTuruList;

            if(id==null || id == 0)
            {
                return View();
            }
            else
            {
                //güncelleme
                Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
                if (kitapVt == null)
                {
                    return NotFound(kitapVt);
                }
                return View(kitapVt); // Pass the Kitap object to the view
            }
            
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Kitap kitap,IFormFile? file)
        {
            
            // aşağıdaki kod ile validationsuz yöntemle hata mesajı yazdırdık.
            if (ModelState.IsValid)
             {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath=Path.Combine(wwwRootPath,@"img");

                if ( file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;

                }


                if (kitap.Id == 0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Yeni Kitap Türü Başarı İle Eklendi";

                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = "Kitap Güncelleme başarılı";

                }




                _kitapRepository.Kaydet ();//bunu yazmazsak veriler veri tabanına eklenmez
                return RedirectToAction("Index", "Kitap");
             } 
             return View(); 
        }
        /*
        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepository.Get(u=>u.Id==id);
            if (kitapVt == null)
            {
                return NotFound(kitapVt);
            }
            return View(kitapVt); // Pass the Kitap object to the view
        }
        */
        /*
        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            // aşağıdaki kod ile validationsuz yöntemle hata mesajı yazdırdık.
            if (ModelState.IsValid)
            {
                _kitapRepository.Guncelle(kitap);
                _kitapRepository.Kaydet();//bunu yazmazsak veriler veri tabanına eklenmez
                TempData["basarili"] = "Yeni Kitap  Başarı İle Güncellendi";
                return RedirectToAction("Index", "Kitap");
            }
            return View();
        }
        */
        //get işlemi yaptık
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
            if (kitapVt == null)
            {
                return NotFound(kitapVt);
            }
            return View(kitapVt); // Pass the Kitap object to the view
        }
        //post işlemi yapıyoruz
        [HttpPost, ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPOST(int? id)
        {
            Kitap? kitap = _kitapRepository.Get(u=>u.Id==id);
            if(kitap == null)
            {   
                return NotFound();
            }
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Silme İşlemi Başarılı";
            return RedirectToAction("Index", "Kitap");
        }


    }
}
