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
        public HomeController(ILogger<HomeController> logger, RepositoryRelation repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            ModelLayer model = new ModelLayer();
            
            ViewBag.message = TempData["message"];
           
            ViewBag.PageLogoPath = _repo.GetPageLogo("index".ToUpper());

            model.skillList = _repo.GetActiveSkill();
            return View(model);
        }

    }
}
