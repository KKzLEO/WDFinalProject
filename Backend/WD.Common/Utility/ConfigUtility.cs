using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WD.Common.Utility
{
    /// <summary>
    /// 讀取Config資料的共用程式
    /// </summary>
    public class ConfigUtility
    {
        /// <summary>
        /// 取得Config檔內的AppSetting
        /// </summary>
        /// <param name="Key">Key Value</param>
        /// <returns></returns>
        public static string GetAppSetting(string Key)
        {
            string AppSetting = string.Empty;
            AppSetting = System.Configuration.ConfigurationManager.AppSettings[Key];

            return AppSetting;
        }

        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        public static string GetDbConnectionString(string connName)
        {
            return
                System.Configuration.ConfigurationManager.
                    ConnectionStrings[connName].ConnectionString.ToString();
        }
    }
}
