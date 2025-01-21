using BusinessLogicLayer.RepositoryRelation;
using DataAccessLayer.ModelLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    public class AdminController : Controller
    {
        private readonly RepositoryRelation _repo;
        public AdminController(RepositoryRelation repo)
        {
            _repo = repo;
        }

        public IActionResult Index(string DeleteId, string ActionType)
        {
            if (!string.IsNullOrEmpty(DeleteId) && !string.IsNullOrEmpty(ActionType))
            {
                string message = _repo.ModifyErrorLogs(DeleteId, ActionType);
                TempData["message"] = message;
                return RedirectToAction("Index");
            }
            ViewBag.message = TempData["message"];
            ViewBag.LoginUser = TempData["username"];
            var errorList = _repo.GetAllErrorLogs();
            return View(errorList);
        }

        public IActionResult WebPages(string Id, string Status)
        {
            if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Status))
            {
                string message = _repo.ModifyWebPages(Id,Status);

                TempData["message"] = message;
                return RedirectToAction("WebPages");
            }
            var PageList = _repo.GetAllPage();
            ViewBag.message = TempData["message"];
            return View(PageList);
        }

        [HttpPost]
        public IActionResult WebPages(WebPageModel model)
        {
            var message = _repo.InsertUpdateWebPage(model);
            TempData["message"] = message;
            return RedirectToAction("WebPages");
        }

        [HttpPost]
        public JsonResult UpdateWebPage(WebPageModel model)
        {
            var message = _repo.InsertUpdateWebPage(model);
            var result = new { success = true, message = message };
            return Json(result);
        }

        public IActionResult TechnicalSkill(string Id, string Status)
        {
            if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Status))
            {
                string message = _repo.ModifySkill(Id, Status);

                TempData["message"] = message;
                return RedirectToAction("TechnicalSkill");
            }

            var SkillList = _repo.GetAllSkill();
            ViewBag.message = TempData["message"];
            return View(SkillList);
        }
        [HttpPost]
        public IActionResult TechnicalSkill(SkillModel model)
        {
            var message = _repo.InsertUpdateSkill(model);
            TempData["message"] = message;
            return RedirectToAction("TechnicalSkill");
        }

        [HttpPost]
        public JsonResult UpdateSkill(SkillModel model)
        {
            var message = _repo.InsertUpdateSkill(model);
            var result = new { success = true, message = message };
            return Json(result);
        }

    }
}
