using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ModelLayer
{
    public class ModelLayer
    {
        public List<SkillModel> skillList { get; set; } = new List<SkillModel>();
        public List<ProjectModel> ProjectList { get; set; } = new List<ProjectModel>();
        public WebsiteMetaModel MetaModelByPage { get; set; } = new WebsiteMetaModel();
    }

    public class ErrorModel
    {
        public int Id { get; set; }
        public string? ErrorMessage { get; set; }
        public string? DateOccurred { get; set; }
        public string? MethodName { get; set; }
        public string? IsStatus { get; set; }
    }

    public class WebPageModel
    {
        public int Id { get; set; }
        public string? PageName { get; set; }
        public IFormFile? PageLogo { get; set; }
        public string? PageLogoPath { get; set; }
        public string? CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class SkillModel
    {
        public int Id { get; set; }
        public string? SkillCategory { get; set; }
        public string? SkillName { get; set; }
        public string? SkillLevel { get; set; }
        public string? CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class WebsiteMetaModel
    {
        public int Id { get; set; }
        public string? PageName { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public string? PageTitle { get; set; }
        public IFormFile? FaviconUrlText { get; set; }
        public string? FaviconUrl { get; set; }
        public string? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public List<WebsiteMetaModel> WebMetaList { get; set; } = new List<WebsiteMetaModel>();
    }
    public class ContactUsModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
        public string? TokenNo { get; set; }
        public string? CreatedAt { get; set; }
        public bool IsActive { get; set; }
        
    }
    public class SocialProfileModel
    {
        public int Id { get; set; }
        public string? UrlLink { get; set; }
        public string? UrlText { get; set; }
        public string? ActionType { get; set; }
        public IFormFile? SocialProfileIcon { get; set; }
        public string? SocialProfileIconPath { get; set; }
        public string? CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class ProjectModel
    {
        public int Id { get; set; }
        public IFormFile? ProjectImage { get; set; }
        public string? ProjectImagePath { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectTitle { get; set; }
        public string? ProjectImageAlt { get; set; }
        public string? ProjectBriefIntro { get; set; }
        public string? ProjectCategory { get; set; }
    }
    public class ContactUsPageModel
    {
        public List<SocialProfileModel> SocialUrlList { get; set; } = new List<SocialProfileModel>();
        public Dictionary<string, decimal> skillCalculationOverAll { get; set; } = new Dictionary<string, decimal>();
    }
}
