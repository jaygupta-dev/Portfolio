using DataAccessLayer.ModelLayer;
using DataAccessLayer.UtilityClass;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Repository
{
    public class Repository
    {
        private readonly DataUtility _dataUtility;
        private readonly Common.Common _common;
        public Repository(DataUtility dataUtility, Common.Common common)
        {
            _dataUtility = dataUtility;
            _common = common;
        }

        public string ModifyErrorLogs(string Id, string ActionType)
        {
            string Message = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("Id",Convert.ToInt32(Id)),
                        new SqlParameter("ActionType",ActionType),
                    };

                    int res = _dataUtility.ExecuteSqlSP("sp_ManageErrorLogs", parameters);

                    if (res > 0)
                    {
                        Message = "Modified Successfully";
                    }
                    else
                    {
                        Message = "Try Again!!!";
                    }
                }
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("GetAllErrorLogs", ex);
            }
            return Message;
        }

        public List<ErrorModel> GetAllErrorLogs()
        {
            List<ErrorModel> errorLogsList = new List<ErrorModel>();
            try
            {
                DataTable res = _dataUtility.GetDataTableSP("sp_ManageErrorLogs");
                if (res != null)
                {
                    foreach (DataRow row in res.Rows)
                    {
                        errorLogsList = _common.ConvertToList<ErrorModel>(res);
                    }

                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("GetAllErrorLogs",ex);
            }
            return errorLogsList;
        }

        public string InsertUpdateWebPage(WebPageModel model)
        {
            string message = string.Empty;
            try
            {
                string FileName = _common.SaveImage(model.PageLogo, "/images/pagelogo");
                if (string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(model.PageLogoPath))
                {
                    FileName = model.PageLogoPath;
                }

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType","modify".ToUpper()),
                    new SqlParameter("@Id",model.Id),
                    new SqlParameter("@PageName",model.PageName != null ? model.PageName.ToUpper() : ""),
                    new SqlParameter("@PageLogoPath",FileName)
                };

                int result = _dataUtility.ExecuteSqlSP("sp_ManageWebPages", parameters);
                if (result == 0)
                {
                    message = "Data Not Inserted !!! Please Try Again.";
                }
                else
                {
                    message = model.Id > 0 ? "Data Updated Successfully" : "Data Inserted Successfully.";
                }
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("ManageWebAppPages",ex);
            }
            return message;
        }

        public List<WebPageModel> GetAllPage()
        {
            List<WebPageModel> webPageModels = new List<WebPageModel>();
            try
            {
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("@ActionType", "select".ToUpper());

                DataTable dataTable = _dataUtility.GetDataTableSP("sp_ManageWebPages", parameters);
                if(dataTable != null && dataTable.Rows.Count > 0)
                {
                    webPageModels = _common.ConvertToList<WebPageModel>(dataTable);
                }
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("GetAllPage",ex);
            }
            return webPageModels;
        }

        public string ModifyWebPages(string Id, string Status)
        {
            string message = string.Empty;

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType",Status),
                    new SqlParameter("@Id",Id),
                };

                int result = _dataUtility.ExecuteSqlSP("sp_ManageWebPages", parameters);

                if(result == 0)
                {
                    message = "Please try again!!!";
                }
                else
                {
                    message = 
                        Status == "activate" ? "Activated successfully" : 
                        Status == "deactivate" ? "Deactivated successfully" : 
                        Status == "delete" ? "Deleted successfully" :
                        "try again";
                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("ModifyWebPages", ex);
            }

            return message;
        }

        // get pagelogo
        public string GetPageLogo(string Page = "")
        {
            try
            {
                string Logo = "";

                DataTable dataTable = _dataUtility.GetDataTableText($"select PageLogoPath from TblWebPages where PageName = '{Page}'");

                if(dataTable != null && dataTable.Rows.Count > 0)
                {
                    Logo = dataTable.Rows[0][0].ToString();
                }
                return Logo;
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("GetPageLogo", ex);
                throw;
            }
        }

        public string InsertUpdateSkill(SkillModel model)
        {
            string message = "";

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType","modify".ToUpper()),
                    new SqlParameter("@Id",model.Id),
                    new SqlParameter("@SkillCategory",model.SkillCategory != null ? model.SkillCategory.ToUpper() : ""),
                    new SqlParameter("@SkillName",model.SkillName != null ? model.SkillName : ""),
                    new SqlParameter("@SkillLevel",model.SkillLevel != null ? model.SkillLevel.Trim() : "0")
                };

                int result = _dataUtility.ExecuteSqlSP("sp_ManageSkill", parameters);
                if (result == 0)
                {
                    message = "Data Not Inserted !!! Please Try Again.";
                }
                else
                {
                    message = model.Id > 0 ? "Data Updated Successfully" : "Data Inserted Successfully.";
                }
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("InsertUpdateSkill", ex);
                throw;
            }

            return message;
        }

        public List<SkillModel> GetAllSkill()
        {
            List<SkillModel> skillList = new List<SkillModel>();

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType","select")
                };

                DataTable result = _dataUtility.GetDataTableSP("sp_ManageSkill", parameters);
                if (result != null && result.Rows.Count > 0)
                {
                    skillList = _common.ConvertToList<SkillModel>(result);
                }
                
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("GetAllSkill", ex);
                throw;
            }

            return skillList;
        }

        public string ModifySkill(string Id, string Status)
        {
            string message = string.Empty;

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType",Status),
                    new SqlParameter("@Id",Id),
                };

                int result = _dataUtility.ExecuteSqlSP("sp_ManageSkill", parameters);

                if (result == 0)
                {
                    message = "Please try again!!!";
                }
                else
                {
                    message =
                        Status == "activate" ? "Activated successfully" :
                        Status == "deactivate" ? "Deactivated successfully" :
                        Status == "delete" ? "Deleted successfully" :
                        "try again";
                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("ModifyWebPages", ex);
            }

            return message;
           
        }


    }
}
