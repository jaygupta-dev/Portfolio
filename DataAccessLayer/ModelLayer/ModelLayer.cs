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

    public class ProjectModel
    {
        public int Id { get; set; }
    }
}
