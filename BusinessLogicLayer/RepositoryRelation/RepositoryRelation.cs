using BusinessLogicLayer.Common;
using BusinessLogicLayer.Repository;
using DataAccessLayer.ModelLayer;
using DataAccessLayer.UtilityClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusinessLogicLayer.RepositoryRelation
{
    public class RepositoryRelation
    {
        private readonly Repository.Repository _repo;
        public RepositoryRelation(Repository.Repository repo)
        {
            _repo = repo;
        }

        public string ModifyErrorLogs(string Id, string ActionType)
        {
            return _repo.ModifyErrorLogs(Id, ActionType);
        }
        public List<ErrorModel> GetAllErrorLogs()
        {
            return _repo.GetAllErrorLogs();
        }

        public string InsertUpdateWebPage(WebPageModel model)
        {
            return _repo.InsertUpdateWebPage(model);
        }

        public List<WebPageModel> GetAllPage()
        {
            return _repo.GetAllPage();
        }

        public string ModifyWebPages(string Id, string Status)
        {
            return _repo.ModifyWebPages(Id, Status);
        }
        public string GetPageLogo(string Page = "")
        {

            string PageLogoPath = _repo.GetPageLogo(Page);

            return PageLogoPath;
        }     
        public string InsertUpdateSkill(SkillModel model)
        {
            return _repo.InsertUpdateSkill(model);
        }

        public List<SkillModel> GetAllSkill()
        {
            return _repo.GetAllSkill();
        }
        public string ModifySkill(string Id, string ActionType)
        {
            return _repo.ModifySkill(Id, ActionType);
        }

        public List<SkillModel> GetActiveSkill()
        {
            var lst = _repo.GetAllSkill();
            var skillList = lst.Where(p => p.IsActive == true).ToList();
            return skillList;
        }

        public string WebsiteMetaInsertUpdate(WebsiteMetaModel model)
        {
            return _repo.WebsiteMetaInsertUpdate(model);
        }

        public string ManageWebsiteMeta(string ActionType = null, int Id = 0)
        {
            return _repo.ManageWebsiteMeta(ActionType, Id);
        }

        public List<WebsiteMetaModel> GetMetaData(string ActionType = null, int Id = 0)
        {
            return _repo.GetMetaData(ActionType, Id);
        }

        public WebsiteMetaModel GetMetaDataById(string ActionType = null, int Id = 0)
        {
            return _repo.GetMetaDataById(ActionType, Id);
        }

        public WebsiteMetaModel MetaDataByPage(string PageName)
        {
            return _repo.MetaDataByPage(PageName);
        }

        public string SaveContactUsForm(ContactUsModel model)
        {
            return _repo.SaveContactUsForm(model);
        }
        public string InsertUpdateSocialProfile(SocialProfileModel model)
        {
            return _repo.InsertUpdateSocialProfile(model);
        }      
        public string ManageSocialProfile(string ActionType, int Id)
        {
            return _repo.ManageSocialProfile(ActionType, Id);
        }

        public string SolveEnquiryForm(int Id)
        {
            return _repo.SolveEnquiryForm(Id);
        }

        public List<ContactUsModel> GetEnqueryList()
        {
            return _repo.GetEnqueryList();
        }

        public List<SocialProfileModel> GetSocialProfile()
        {
            return _repo.GetSocialProfile();
        }

        public List<SocialProfileModel> GetSocialProfilePage()
        {
            var lst = _repo.GetSocialProfile();

            var SocialList = lst.Where(m => m.IsActive == true).Select(m => new SocialProfileModel()
            {
                UrlLink = m.UrlLink,
                UrlText = m.UrlText,
                SocialProfileIconPath = m.SocialProfileIconPath
            }).ToList();

            return SocialList;
        }

        public Dictionary<string, decimal> TotalSkillLevelCategoryWise()
        {
            var skill = _repo.GetAllSkill();
            var fd = skill.Where(m => m.SkillCategory != null && m.SkillCategory.ToLower() == "frontend" && m.IsActive == true).ToList();
            var bd = skill.Where(m => m.SkillCategory != null && m.SkillCategory.ToLower() == "backend" && m.IsActive == true).ToList();
            var db = skill.Where(m => m.SkillCategory != null && m.SkillCategory.ToLower() == "database" && m.IsActive == true).ToList();
            var ot = skill.Where(m => m.SkillCategory != null && m.SkillCategory.ToLower() == "others" && m.IsActive == true).ToList();

            int FDCount = Math.Max(1, fd.Count);
            int BDCount = Math.Max(1, bd.Count);
            int DBCount = Math.Max(1, db.Count);
            int OTSCount = Math.Max(1, ot.Count);

            decimal fSum = fd.Sum(m => Convert.ToDecimal(m.SkillLevel)); 
            decimal bSum = bd.Sum(m => Convert.ToDecimal(m.SkillLevel)); 
            decimal dSum = db.Sum(m => Convert.ToDecimal(m.SkillLevel)); 
            decimal oSum = ot.Sum(m => Convert.ToDecimal(m.SkillLevel));

            decimal frontPer = Math.Floor(fSum / FDCount);
            decimal backPer = Math.Floor(bSum / BDCount);
            decimal dbtPer = Math.Floor(dSum / DBCount);
            decimal otherPer = Math.Floor(oSum / OTSCount);
            
            Dictionary<string, decimal> OverAllSkillLevel = new Dictionary<string, decimal>();

            OverAllSkillLevel.Add("frontend",frontPer);
            OverAllSkillLevel.Add("backend",backPer);
            OverAllSkillLevel.Add("database", dbtPer);
            OverAllSkillLevel.Add("others", otherPer);

            return OverAllSkillLevel;
        }

        public string InsertUpdateProjects(ProjectModel model)
        {
            return _repo.InsertUpdateProjects(model);
        }

        public string InsertUpdateSlider(BannerSliderModel model)
        {
            return _repo.InsertUpdateSlider(model);
        }

        public List<BannerSliderModel> GetBannerSlider()
        {
            return _repo.GetBannerSlider();
        }

        public BannerSliderModel GetBannerIntroOne(string Id)
        {
            return _repo.GetBannerIntroOne(Id);
        }
        public string ManageSlider(string Id, string ActionType)
        {
            return _repo.ManageSlider(Id, ActionType);
        }

        public List<BannerSliderModel> GetActivePageSlider()
        {
            List<BannerSliderModel> SliderList = new List<BannerSliderModel>();

            var slide = _repo.GetBannerSlider();

            var activeslider = slide.Where(m => m.IsActive == true && m.WebPage.ToLower() == "index").Select(m => new BannerSliderModel
            {
                SliderImagePath = m.SliderImagePath,
                SliderImageAlt = m.SliderImageAlt,
                SlideTitle = m.SlideTitle,
                SlideContent = m.SlideContent,
            }).ToList();

            SliderList = activeslider.ToList();
            return SliderList;
        }

    }
}
