using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD.Model.Code;
using Dapper;

namespace WD.Dao.Code
{
    public class CodeDao
    {
        /// <summary>
        /// 獲得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDbConnectionString()
        {
            return WD.Common.Utility.ConfigUtility.GetDbConnectionString("DBConnection");
        }

        public List<CodeDataModel> GetCourseCategory()
        {
            string sql = @"SELECT CATEGORY_CODE AS Id,
	                               CATEGORY_CODE AS Value,
	                               CATEGORY_NAME AS Text
                            FROM COURSE_CATEGORY_CODE";
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                List<CodeDataModel> result = connection.Query<CodeDataModel>(sql).ToList();
                return result;
            }
        }

        public List<CodeDataModel> GetTeacherList()
        {
            string sql = @"SELECT PER_SERIL_NO AS Id,
	                               PER_SERIL_NO AS Value,
	                               C_NAME AS Text
                            FROM USERS
                            WHERE TITLE_CODE = '2'";
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                List<CodeDataModel> result = connection.Query<CodeDataModel>(sql).ToList();
                return result;
            }
        }

        public List<CodeDataModel> GetGenderCode()
        {
            string sql = @"SELECT GENDER_CODE AS Id,
	                                GENDER_CODE AS Value,
	                                GENDER_TEXT AS Text
                            FROM GENDER_CODE";
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                List<CodeDataModel> result = connection.Query<CodeDataModel>(sql).ToList();
                return result;
            }
        }

        public List<CodeDataModel> GetTitleCode()
        {
            string sql = @"SELECT TITLE_CODE AS Id,
	                                TITLE_CODE AS Value,
	                                TITLE_NAME AS Text
                            FROM TITLE_CODE";
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                List<CodeDataModel> result = connection.Query<CodeDataModel>(sql).ToList();
                return result;
            }
        }

    }
}
