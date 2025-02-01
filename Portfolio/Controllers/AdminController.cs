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


        public IActionResult WebsiteMeta(string ActionType, int Id=0)
        {
            if (!string.IsNullOrEmpty(ActionType) && ActionType != "select" && Id != 0)
            {
                var message = _repo.ManageWebsiteMeta(ActionType, Id);
                TempData["message"] = message;
                return RedirectToAction("WebsiteMeta");
            }

            WebsiteMetaModel model = new WebsiteMetaModel();

            ViewBag.WebPages = _repo.GetAllPage();
            ViewBag.message = TempData["message"];

            if (ActionType == "select" && Id > 0)
                model = _repo.GetMetaDataById(ActionType, Id);
            else
                model.WebMetaList = _repo.GetMetaData("select");
            return View(model);
        }

        [HttpPost]
        public IActionResult WebsiteMeta(WebsiteMetaModel model)
        {
            var message = _repo.WebsiteMetaInsertUpdate(model);
            TempData["message"] = message;
            return RedirectToAction("WebsiteMeta");
        }

        public IActionResult SocialUrlLink(string ActionType, int Id)
        {
            string message = "";
            if ((!string.IsNullOrEmpty(ActionType) && Id != 0))
            {
                message = _repo.ManageSocialProfile(ActionType, Id);

                TempData["Message"] = message;

                return RedirectToAction("SocialUrlLink");
            }

            var socialProfiles = _repo.GetSocialProfile();
            ViewBag.message = TempData["Message"];
            return View(socialProfiles);
        }

        [HttpPost]
        public IActionResult SocialUrlLink(SocialProfileModel model)
        {
            string message = _repo.InsertUpdateSocialProfile(model);
            TempData["message"] = message;
            return RedirectToAction("SocialUrlLink");
        }

        public IActionResult GetEnquiryFrom(int Id)
        {
            if (Id != 0 && Id > 0)
            {
                string message = _repo.SolveEnquiryForm(Id);
                TempData["message"] = message;
                return RedirectToAction("GetEnquiryFrom");
            }
            ViewBag.message = TempData["message"];
            var EnquiryList = _repo.GetEnqueryList();
            return View(EnquiryList);
        }

        [HttpPost]
        public IActionResult UpdateSocialUrl(SocialProfileModel model)
        {
            var message = _repo.InsertUpdateSocialProfile(model);
            var result = new { success = true, message = message };
            return Json(result);
        }


        public IActionResult Projects(int Id, string ActionType)
        {
            //ProjectModel model = new ProjectModel();

            //if ((!string.IsNullOrEmpty(ActionType) && !string.IsNullOrEmpty(Id.ToString())) || (!string.IsNullOrEmpty(ActionType) && !string.IsNullOrEmpty(selectedIds)))
            //{
            //    var message = _adminContentRepo.ManageProject(null, ActionType, Id, selectedIds);
            //    TempData["message"] = message;
            //    return RedirectToAction("Projects");
            //}
            //model.CategoryList = _adminContentRepo.GetAllCategoryOnlyName();
            //ViewBag.WebPages = _adminContentRepo.GetAllPage();
            //var ProjectList = _adminContentRepo.GetProjects(Id);
            //model.ProjectList = ProjectList;
            ViewBag.message = TempData["message"];
            return View();
        }

        [HttpPost]
        public IActionResult Projects(ProjectModel model)
        {
            var message = _repo.InsertUpdateProjects(model);
            TempData["message"] = message;
            return RedirectToAction("Projects");
        }

        //public IActionResult GetIPData(int Id)
        //{
        //    if (Id > 0)
        //    {
        //        string Message = _adminContentRepo.DeleteAcivity(Id);
        //        TempData["message"] = Message;
        //        return RedirectToAction("GetIPData");
        //    }
        //    ViewBag.message = TempData["message"];
        //    var ActivityData = _adminContentRepo.AcivityData(Id);
        //    return View(ActivityData);
        //}

        //public IActionResult ProjectsContent(int ProjectId)
        //{
        //    return View();
        //}

    }
}
