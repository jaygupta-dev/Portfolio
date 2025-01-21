using DataAccessLayer.UtilityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Common
{
    public class Common
    {
        private readonly DataUtility _dataUtility;
        public Common(DataUtility dataUtility)
        {
            _dataUtility = dataUtility;
        }

        public void SaveErrorMessage(string method_name, Exception error_message)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ActionType","insert"),
                new SqlParameter("@MethodName",method_name),
                new SqlParameter("@ErrorMessage",error_message.Message)
            };

            _dataUtility.ExecuteSqlSP("sp_ManageErrorLogs", para);
        }

        public List<T> ConvertToList<T>(DataTable dt) where T : new()
        {
            var list = new List<T>();

            try
            {
                PropertyInfo[] modelProperties = typeof(T).GetProperties();

                foreach (DataRow row in dt.Rows)
                {
                    T obj = new T();

                    foreach (DataColumn column in dt.Columns)
                    {
                        PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);

                        if (prop != null)
                        {
                            object value = row[column];

                            if (value == DBNull.Value)
                            {
                                if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                                {
                                    prop.SetValue(obj, null);
                                }
                                else
                                {
                                    prop.SetValue(obj, GetDefaultValue(prop.PropertyType));
                                }
                            }
                            else
                            {
                                try
                                {
                                    prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                                }
                                catch (InvalidCastException)
                                {
                                    throw new InvalidOperationException($"Cannot convert value '{value}' to type '{prop.PropertyType.Name}' for property '{prop.Name}'.");
                                }
                            }
                        }
                    }

                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                SaveErrorMessage("ConvertToList", ex);
            }

            return list;
        }

        private static object GetDefaultValue(Type type)
        {
            if (type == typeof(string)) return string.Empty;
            if (type.IsValueType) return Activator.CreateInstance(type);
            return null;
        }

        public string SaveImage(IFormFile? File, string FilePath, string? FileName)
        {
            string fileName = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    FilePath = "Images/Others";
                }

                if (File != null && File.Length > 0)
                {
                    string ImageGuid = Guid.NewGuid().ToString();
                    string FileExtension = Path.GetExtension(File.FileName);
                    if (string.IsNullOrEmpty(FileName))
                    {
                        fileName = ImageGuid + FileExtension;
                    }
                    else
                    {
                        fileName = FileName + FileExtension;
                    }

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + FilePath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }

                }
            }
            catch (Exception ex)
            {
                SaveErrorMessage("SaveImage", ex);
            }
            return fileName;
        }

        public string SaveImage(IFormFile? File, string FilePath)
        {
            string fileName = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    FilePath = "Images/Others";
                }

                if (File != null && File.Length > 0)
                {
                    string ImageGuid = Guid.NewGuid().ToString();
                    string FileExtension = Path.GetExtension(File.FileName);

                    fileName = ImageGuid + FileExtension;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + FilePath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }

                }
            }
            catch (Exception ex)
            {
                SaveErrorMessage("SaveImage", ex);
            }
            return fileName;
        }

        public string SaveImage(IFormFile? File)
        {
            string fileName = string.Empty;
            try
            {
                string FilePath = "/Images/Others";


                if (File != null && File.Length > 0)
                {
                    string ImageGuid = Guid.NewGuid().ToString();
                    string FileExtension = Path.GetExtension(File.FileName);

                    fileName = ImageGuid + FileExtension;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + FilePath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }

                }
            }
            catch (Exception ex)
            {
                SaveErrorMessage("SaveImage", ex);
            }
            return fileName;
        }

        public string RandomNumber()
        {
            string RandomNumber = string.Empty;

            try
            {
                string value = "WEDCV27HNM56QAZXS38BGTYJ9FR104KUILOP";

                for (int i = 0; i < 6; i++)
                {
                    Random random = new Random();
                    int ranNum = random.Next(1, 36);

                    RandomNumber = RandomNumber + value[ranNum];
                }


            }
            catch (Exception ex)
            {
                SaveErrorMessage("RandomNumber", ex);
            }

            return RandomNumber;
        }

        private string GetBrowserInfoFromUserAgent(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return "Unknown Browser";

            // Regular expressions for detecting common browsers and versions
            var browsers = new Dictionary<string, string>
            {
                { "Chrome", @"Chrome/(\d+)" },
                { "Firefox", @"Firefox/(\d+)" },
                { "Safari", @"Version/(\d+).*Safari" },
                { "Edge", @"Edg/(\d+)" },
                { "MSIE", @"MSIE (\d+)" },  // For Internet Explorer
                { "Trident", @"Trident/(\d+)" }  // For older Internet Explorer versions
            };

            foreach (var browser in browsers)
            {
                var match = Regex.Match(userAgent, browser.Value);
                if (match.Success)
                {
                    return $"{browser.Key} {match.Groups[1].Value}";
                }
            }

            return "Unknown Browser";
        }

        public T ConvertToModel<T>(DataTable dt) where T : new()
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return default(T);
                }

                T model = new T();
                DataRow row = dt.Rows[0];

                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop != null && dt.Columns.Contains(prop.Name))
                    {
                        if (row[prop.Name] != DBNull.Value && row[prop.Name] != null)
                        {
                            prop.SetValue(model, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                        }
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                SaveErrorMessage("ConvertToModel", ex);
                return default(T);
            }
        }
    }
}
