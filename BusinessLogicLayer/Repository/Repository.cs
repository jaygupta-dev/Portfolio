using DataAccessLayer.ModelLayer;
using DataAccessLayer.UtilityClass;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                _common.SaveErrorMessage("ModifySkill", ex);
            }

            return message;
           
        }

        public string WebsiteMetaInsertUpdate(WebsiteMetaModel model)
        {
            string message = string.Empty;
            string FileName = string.Empty;

            try
            {
                string ActionType = model.Id == 0 ? "insert" : "update";
                if (model.FaviconUrlText != null)
                {
                    FileName = _common.SaveImage(model.FaviconUrlText, "/images/favicon");
                }
                else
                {
                    FileName = model.FaviconUrl;
                }
                
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType",ActionType),
                    new SqlParameter("@Id",model.Id),
                    new SqlParameter("@MetaTitle",model.MetaTitle),
                    new SqlParameter("@MetaKeyword",model.MetaKeyword),
                    new SqlParameter("@MetaDescription",model.MetaDescription),
                    new SqlParameter("@PageName",model.PageName),
                    new SqlParameter("@FaviconUrl",FileName),
                    new SqlParameter("@PageTitle",model.PageTitle)
                };

                int result = _dataUtility.ExecuteSqlSP("sp_ManageWebsiteMetadata", parameters);

                if (result == 0)
                {
                    message = "Please Try Again!!!";
                }
                else
                {
                    message = model.Id > 0 ? "Updated Successfully" : "Inserted Successfully";
                }
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("WebsiteMetaInsertUpdate", ex);
            }

            return message;
        }

        public string ManageWebsiteMeta(string ActionType = null, int Id = 0)
        {
            string message = string.Empty;
            int result = 0;
            try
            {

                if (!string.IsNullOrEmpty(ActionType) && Id != 0)
                {
                    SqlParameter[] parameters =
                    {
                            new SqlParameter("@ActionType",ActionType),
                            new SqlParameter("@Id",Id)
                    };

                    result = _dataUtility.ExecuteSqlSP("sp_ManageWebsiteMetadata", parameters);
                }

                if (result > 0)
                {
                    message =
                        ActionType == "active" ? "Selected Items Are Activated Successfully." :
                        ActionType == "deactive" ? "Item Deactivated Successfully." :
                        ActionType == "delete" ? "Item Deleted Successfully" :
                        "Please Try Again";
                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("ManageWebsiteMeta", ex);
            }

            return message;
        }

        public List<WebsiteMetaModel> GetMetaData(string ActionType = null, int Id = 0)
        {
            var MetaData = new List<WebsiteMetaModel>();
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType",ActionType),
                    new SqlParameter("@Id",Id)
                };

                DataTable res = _dataUtility.GetDataTableSP("sp_ManageWebsiteMetadata", parameters);

                if (res != null && res.Rows.Count > 0)
                {
                    MetaData = _common.ConvertToList<WebsiteMetaModel>(res);
                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("GetMetaData", ex);
            }

            return MetaData;
        }

        public WebsiteMetaModel GetMetaDataById(string ActionType = null, int Id = 0)
        {
            var MetaData = new WebsiteMetaModel();
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType",ActionType),
                    new SqlParameter("@Id",Id)
                };

                DataTable res = _dataUtility.GetDataTableSP("sp_ManageWebsiteMetadata", parameters);

                if (res != null && res.Rows.Count > 0)
                {
                    MetaData = _common.ConvertToModel<WebsiteMetaModel>(res);
                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("GetMetaData", ex);
            }

            return MetaData;
        }

        public WebsiteMetaModel MetaDataByPage(string PageName)
        {
            var MetaData = new WebsiteMetaModel();
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@PageName",PageName)
                };

                DataTable res = _dataUtility.GetDataTableSP("sp_GetMetaData", parameters);

                if (res != null && res.Rows.Count > 0)
                {
                    foreach (DataRow row in res.Rows)
                    {
                        MetaData.PageName = row["PageName"] != DBNull.Value ? Convert.ToString(row["PageName"]) : "";
                        MetaData.PageTitle = row["PageTitle"] != DBNull.Value ? Convert.ToString(row["PageTitle"]) : "";
                        MetaData.MetaTitle = row["MetaTitle"] != DBNull.Value ? Convert.ToString(row["MetaTitle"]) : "";
                        MetaData.MetaKeyword = row["MetaKeyword"] != DBNull.Value ? Convert.ToString(row["MetaKeyword"]) : "";
                        MetaData.MetaDescription = row["MetaDescription"] != DBNull.Value ? Convert.ToString(row["MetaDescription"]) : "";
                        MetaData.FaviconUrl = row["FaviconUrl"] != DBNull.Value ? Convert.ToString(row["FaviconUrl"]) : "";

                    }
                }

            }
            catch (Exception ex)
            {
                // Log any errors
                _common.SaveErrorMessage("GetMetaData",ex);
            }

            return MetaData;
        }

        public string SaveContactUsForm(ContactUsModel model)
        {
            string message = string.Empty;

            try
            {
                string TokenNo = _common.RandomNumber();

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType","insert"),
                    new SqlParameter("@Name",model.Name),
                    new SqlParameter("@Email",model.Email),
                    new SqlParameter("@MobileNo",model.MobileNo),
                    new SqlParameter("@Subject",model.Subject),
                    new SqlParameter("@Message",model.Message),
                    new SqlParameter("@TokenNo",TokenNo),
                    new SqlParameter("@IsStatus",0),
                };

                int res = _dataUtility.ExecuteSqlSP("sp_ManageContactUsForm", parameters);

                if (res > 0)
                {
                    SendEmail(model.Name, model.Email, model.Subject, model.Message, TokenNo);
                    message = "Thank You For Fill This Form!!!";
                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("SaveContactUsForm",ex);
            }
            return message;
        }

        private void SendEmail(string? ContactName, string? ContactEmail, string? ContactSubject, string? ContactMessage, string? TokenNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(ContactName) && !string.IsNullOrEmpty(ContactEmail) && !string.IsNullOrEmpty(ContactSubject) && !string.IsNullOrEmpty(ContactMessage) && !string.IsNullOrEmpty(TokenNo))
                {
                    string emailBody = $@"
        <html>
        <head>
            <title>Thank You for Contacting Us</title>
        </head>
        <body style='font-family: Arial, sans-serif; color: #333; margin: 0; padding: 0; background-color: #f9f9f9;'>
            <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);'>
                <h2 style='color: #2c3e50; text-align: center;'>Thank You for Reaching Out!</h2>
                <p style='font-size: 16px; line-height: 1.5; margin-bottom: 20px;'>Hi <strong>{ContactName}</strong>,</p>
                <p style='font-size: 16px; line-height: 1.5; margin-bottom: 20px;'>Thank you for contacting us regarding: <strong>{ContactSubject}</strong></p>
                <p style='font-size: 16px; line-height: 1.5; margin-bottom: 20px;'>We have received your message:</p>
                <blockquote style='font-size: 16px; line-height: 1.5; margin: 0 0 20px 20px; border-left: 4px solid #f39c12; padding-left: 10px; color: #555;'>
                    {ContactMessage}
                </blockquote>
                <p style='font-size: 16px; line-height: 1.5; margin-bottom: 20px;'>Your feedback/suggestion reference is: <strong style='color: #e74c3c;'>{TokenNo}</strong></p>
                <p style='font-size: 16px; line-height: 1.5; margin-bottom: 20px;'>I will review your feedback and get back to you as soon as possible.</p>
                <p style='font-size: 16px; line-height: 1.5;'>Best regards,</p>
                <p style='font-size: 16px; line-height: 1.5; font-weight: bold;'>Jayanendra Gupta</p>
            </div>
        </body>
        </html>";

                    var fromAddress = new MailAddress("jayanendragupta@gmail.com", "Jay Developer");
                    var toAddress = new MailAddress(ContactEmail);
                    const string formPassword = "ypkf vkel fpyc rxno";
                    var bccAddress = new MailAddress("jayanendragupta@hotmail.com");
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, formPassword)
                    };

                    var mailMessage = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = ContactSubject,
                        Body = emailBody,
                        IsBodyHtml = true
                    };
                    mailMessage.Bcc.Add(bccAddress);
                    smtp.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("SendEmail",ex);
            }
        }
        public string InsertUpdateSocialProfile(SocialProfileModel model)
        {
            string message = string.Empty;
            int result = 0;
            try
            {
                string FileName = "";

                if (model != null)
                {
                    if (model.SocialProfileIcon != null)
                    {
                        FileName = _common.SaveImage(model.SocialProfileIcon, "/images/socialicon");
                    }
                    else
                    {
                        FileName = model.SocialProfileIconPath != null ? model.SocialProfileIconPath : "";
                    }

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Id", model.Id),
                        new SqlParameter("@ActionType", "MODIFY"),
                        new SqlParameter("@UrlLink", model.UrlLink),
                        new SqlParameter("@UrlText", model.UrlText),
                        new SqlParameter("@SocialProfileIcon", FileName),
                    };

                    result = _dataUtility.ExecuteSqlSP("sp_ManageSocialLinks", parameters);


                    if (result > 0 || result == -1)
                    {
                        message =
                            model.Id  ==  0 ? "Inserted Successfully" :
                            model.Id > 0 ? "Updated Successfully" : "Neither Update Nor Insert!!!";
                    }
                    else
                    {
                        message = "Please Try Again";
                    }
                }
            }

            catch (Exception ex)
            {
                _common.SaveErrorMessage("InsertUpdateSocialProfile", ex);
            }
            return message;
        }
        public string ManageSocialProfile(string ActionType, int Id)
        {
            string message = string.Empty;
            int result = 0;
            try
            {

                    if (!string.IsNullOrEmpty(ActionType) && Id != 0)
                    {
                        SqlParameter[] parameters =
                        {
                            new SqlParameter("@ActionType", ActionType),
                            new SqlParameter("@Id", Id)
                        };

                        result = _dataUtility.ExecuteSqlSP("sp_ManageSocialLinks", parameters);
                    }

                    if (result > 0 || result == -1)
                    {
                        message =
                         ActionType == "deactive" ? "Deactivated Successfully" :
                         ActionType == "active" ? "Activated Successfully" :
                         "Deleted Successfully";
                    }
                    else
                    {
                        message = "Please Try Again";
                    }
            }

            catch (Exception ex)
            {
                _common.SaveErrorMessage("ManageSocialProfile", ex);
            }
            return message;
        }
   
        public string SolveEnquiryForm(int Id)
        {
            string message = string.Empty;

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType","solved"),
                    new SqlParameter("@Id",Id)
                };

                int res = _dataUtility.ExecuteSqlSP("sp_ManageContactUsForm", parameters);

                if (res > 0)
                {
                    message = "Marked As Solved !!!";
                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("SovlveEnquiryForm",ex);
            }

            return message;
        }

        public List<ContactUsModel> GetEnqueryList()
        {
            var enquiryList = new List<ContactUsModel>();
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType", "select")
                };

                DataTable res = _dataUtility.GetDataTableSP("sp_ManageContactUsForm", parameters);

                if (res != null && res.Rows.Count > 0)
                {
                    foreach (DataRow row in res.Rows)
                    {
                        ContactUsModel model = new ContactUsModel();

                        model.Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0;
                        model.Name = row["Name"] != DBNull.Value ? Convert.ToString(row["Name"]) : "";
                        model.Email = row["Email"] != DBNull.Value ? Convert.ToString(row["Email"]) : "";
                        model.MobileNo = row["MobileNo"] != DBNull.Value ? Convert.ToString(row["MobileNo"]) : "";
                        model.Message = row["Message"] != DBNull.Value ? Convert.ToString(row["Message"]) : "";
                        model.Subject = row["Subject"] != DBNull.Value ? Convert.ToString(row["Subject"]) : "";
                        model.TokenNo = row["TokenNo"] != DBNull.Value ? Convert.ToString(row["TokenNo"]) : "";
                        model.CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToString(row["CreatedAt"]) : "";
                        model.IsActive = row["IsStatus"] != DBNull.Value ? Convert.ToBoolean(row["IsStatus"]) : true;

                        enquiryList.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("ManageSocialProfile",ex);
            }

            return enquiryList;
        }

        public List<SocialProfileModel> GetSocialProfile()
        {
            var UrlLinkList = new List<SocialProfileModel>();
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ActionType", "SELECT")
                };

                DataTable res = _dataUtility.GetDataTableSP("sp_ManageSocialLinks", parameters);

                if (res != null && res.Rows.Count > 0)
                {
                    foreach (DataRow row in res.Rows)
                    {
                        SocialProfileModel model = new SocialProfileModel();
                        model.Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0;
                        model.UrlLink = row["UrlLink"] != DBNull.Value ? Convert.ToString(row["UrlLink"]) : "";
                        model.UrlText = row["UrlText"] != DBNull.Value ? Convert.ToString(row["UrlText"]) : "";
                        model.SocialProfileIconPath = row["SocialProfileIconPath"] != DBNull.Value ? Convert.ToString(row["SocialProfileIconPath"]) : "";
                        model.IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : true;

                        UrlLinkList.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {
                _common.SaveErrorMessage("ManageSocialProfile", ex);
            }

            return UrlLinkList;
        }

        public string InsertUpdateProjects(ProjectModel model)
        {
            string message = string.Empty;
            int result = 0;
            try
            {
                string FileName = "";

                if (model != null)
                {
                    if (model.ProjectImage != null)
                    {
                        FileName = _common.SaveImage(model.ProjectImage, "/images/projectimage");
                    }
                    else
                    {
                        FileName = model.ProjectImagePath != null ? model.ProjectImagePath : "";
                    }

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Id", model.Id),
                        new SqlParameter("@ActionType", "MODIFY"),
                        new SqlParameter("@UrlLink", model.ProjectName),
                        new SqlParameter("@UrlText", model.ProjectTitle),
                        new SqlParameter("@UrlText", model.ProjectImageAlt),
                        new SqlParameter("@UrlText", model.ProjectBriefIntro),
                        new SqlParameter("@UrlText", model.ProjectCategory),
                        new SqlParameter("@SocialProfileIcon", FileName),
                    };

                    result = _dataUtility.ExecuteSqlSP("", parameters);


                    if (result > 0 || result == -1)
                    {
                        message =
                            model.Id == 0 ? "Inserted Successfully" :
                            model.Id > 0 ? "Updated Successfully" : "Neither Update Nor Insert!!!";
                    }
                    else
                    {
                        message = "Please Try Again";
                    }
                }
            }

            catch (Exception ex)
            {
                _common.SaveErrorMessage("InsertUpdateProjects", ex);
            }
            return message;
        }

    }
}