using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD.Model.Course;

namespace WD.Dao.Course
{
    public class CourseDao
    {
        /// <summary>
        /// 獲得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDbConnectionString()
        {
            return WD.Common.Utility.ConfigUtility.GetDbConnectionString("DBConnection");
        }

        public List<CourseDataModel> SearchCourse(CourseFilterModel arg) {
            string sql = @"SELECT D.CATEGORY_CODE AS CategoryCode
	                              ,D.CATEGORY_NAME AS CategoryName
	                              ,B.COURSE_SERIL_NO AS CourseSerilNo
	                              ,A.COURSE_NAME AS CourseName
	                              ,A.START_DATE AS StartDate
	                              ,A.END_DATE AS EndDate
	                              ,A.DESCRIPTION AS Description
	                              ,B.PER_SERIL_NO AS TeacherPerSerilNo
	                              ,C.ACCOUNT AS TeacherAccount
	                              ,C.EMAIL AS TeacherEmail
	                              ,C.E_NAME AS TeacherEName
	                              ,C.C_NAME AS TeacherCName
                                  ,A.PRICE AS Price
                                  ,A.COURSE_IMAGE_NAME AS CourseImageName
                            FROM COURSE AS A JOIN TEACHERS AS B ON A.COURSE_SERIL_NO = B.COURSE_SERIL_NO
                            JOIN USERS AS C ON B.PER_SERIL_NO = C.PER_SERIL_NO
                            JOIN COURSE_CATEGORY_CODE AS D ON A.CATEGORY_CODE = D.CATEGORY_CODE
                            WHERE 1=1 ";
            if (!string.IsNullOrEmpty(arg.Name))
            {
                sql += "AND COURSE_NAME LIKE @CourseName ";
            }
            if (!string.IsNullOrEmpty(arg.CategoryCode))
            {
                sql += "AND D.CATEGORY_CODE = @CategoryCode";
            }

            object parameters = new
            {
                CourseName = arg.Name == null ? string.Empty : '%' + arg.Name + '%',
                CategoryCode = arg.CategoryCode == null ? string.Empty : arg.CategoryCode
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {

                List<CourseDataModel> result = connection.Query<CourseDataModel>(sql, parameters).ToList();
                return result;
            }
        }
    }
}
