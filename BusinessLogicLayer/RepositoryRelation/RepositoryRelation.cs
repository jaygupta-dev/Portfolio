using BusinessLogicLayer.Repository;
using DataAccessLayer.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
