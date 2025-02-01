using BusinessLogicLayer.RepositoryRelation;
using DataAccessLayer.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Diagnostics;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RepositoryRelation _repo;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, RepositoryRelation repo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ModelLayer model = new ModelLayer();
            
            ViewBag.message = TempData["message"];
           
            ViewBag.PageLogoPath = _repo.GetPageLogo("index".ToUpper());

            model.skillList = _repo.GetActiveSkill();
            model.MetaModelByPage = _repo.MetaDataByPage("index".ToUpper());
            return View(model);
        }


        public IActionResult SignIn(string? UserName, string? Password)
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                if (Password.ToLower().Trim() == "gupta")
                {
                    TempData["username"] = UserName;
                    return RedirectToAction("Index", "Admin");
                }
            }
            ViewBag.PageLogoPath = _repo.GetPageLogo("SignIn".ToUpper());
            return View();
        }

        public IActionResult ContactUs()
        {
            ContactUsPageModel model = new ContactUsPageModel();
            //var Linkdin = _configuration["Linkdin"];
            //var Instagram = _configuration["Instagram"];
            //var Facebook = _configuration["Facebook"];
            //var Github = _configuration["Github"];

            //ViewBag.Linkdin = Linkdin;
            //ViewBag.Instagram = Instagram;
            //ViewBag.Facebook = Facebook;
            //ViewBag.Github = Github;

            Dictionary<string, decimal> skillCalculation = _repo.TotalSkillLevelCategoryWise();

            var PageSocialProfileList = _repo.GetSocialProfilePage();

            ViewBag.EmailMessage = TempData["message"];
            ViewBag.PageLogoPath = _repo.GetPageLogo("ContactUs".ToUpper());

            model.SocialUrlList = PageSocialProfileList;
            model.skillCalculationOverAll = skillCalculation;

            return View(model);
        }

        [HttpPost]
        public IActionResult ContactUS(ContactUsModel model)
        {
            string message = _repo.SaveContactUsForm(model);

            TempData["message"] = message;
            return RedirectToAction("ContactUs");
        }

    }
}
