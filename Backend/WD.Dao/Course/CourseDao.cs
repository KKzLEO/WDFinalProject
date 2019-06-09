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
                                  ,A.CRE_DATE AS CreDate
                                  ,A.CRE_USR AS CreUsr
                                  ,A.MOD_DATE AS ModDate
                                  ,A.MOD_USR AS ModUsr
                                  ,A.TTL_HR AS TtlHr
                                  ,A.LEARNING_TEXT AS LearningText
                                  ,A.REQUIREMENT_TEXT AS RequirementText
                                  ,A.INS_TEXT AS InsText
                                  ,A.TARGET_TEXT AS TargetText
                                  ,A.SHORT_INTRO_TEXT AS ShortIntroText
                            FROM COURSE AS A JOIN TEACHERS AS B ON A.COURSE_SERIL_NO = B.COURSE_SERIL_NO
                            JOIN USERS AS C ON B.PER_SERIL_NO = C.PER_SERIL_NO
                            JOIN COURSE_CATEGORY_CODE AS D ON A.CATEGORY_CODE = D.CATEGORY_CODE
                            WHERE 1=1 ";
            if(arg == null)
            {
                arg = new CourseFilterModel();
            }

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
                foreach (CourseDataModel course in result) {
                    if (!string.IsNullOrEmpty(course.TargetText)) course.TargetText = course.TargetText.Replace(";", "<br />");
                    if (!string.IsNullOrEmpty(course.RequirementText)) course.RequirementText = course.RequirementText.Replace(";", "<br />");
                    //if (!string.IsNullOrEmpty(course.LearningText)) course.LearningText = course.LearningText.Replace(";", "<br />");
                    if (!string.IsNullOrEmpty(course.InsText)) course.InsText = course.InsText.Replace(";", "<br />");
                }
                return result;
            }
        }

        public List<CourseDataModel> CreateCourse(CourseDataModel newCourse)
        {
            string sql = @"EXEC sp_create_course @CourseName,
                                                 @CategoryCode, 
                                                 @Desc, 
                                                 @CreateUser,
                                                 @Price, 
                                                 @CourseImageName,
                                                 @TtlHr,
                                                 @LearningText,
                                                 @RequirementText,
                                                 @InsText,
                                                 @TargetText,
                                                 @ShortIntroText,
                                                 @TeacherPerSerilNo";
            object parameters = new
            {
                CourseName = newCourse.CourseName,
                CategoryCode = newCourse.CategoryCode,
                Desc = newCourse.Description,
                CreateUser = "Administrator",
                Price = newCourse.Price,
                CourseImageName = newCourse.CourseImageName,
                TtlHr = newCourse.TtlHr,
                LearningText= newCourse.LearningText,
                RequirementText = newCourse.RequirementText,
                InsText = newCourse.InsText,
                TargetText = newCourse.TargetText,
                ShortIntroText = newCourse.ShortIntroText,
                TeacherPerSerilNo = newCourse.TeacherPerSerilNo
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                List<CourseDataModel> result = connection.Query<CourseDataModel>(sql, parameters).ToList();
                return result;
            }

        }

        public List<CourseDataModel> UpdateCourse(CourseDataModel newCourse)
        {
            string sql = @"EXEC sp_update_course @CourseName,
                                                 @CategoryCode, 
                                                 @Desc,
                                                 @Price, 
                                                 @CourseImageName,
                                                 @TtlHr,
                                                 @LearningText,
                                                 @RequirementText,
                                                 @InsText,
                                                 @TargetText,
                                                 @ShortIntroText,
                                                 @TeacherPerSerilNo,
                                                 @CourseSerilNo";
            object parameters = new
            {
                CourseName = newCourse.CourseName,
                CategoryCode = newCourse.CategoryCode,
                Desc = newCourse.Description,
                Price = newCourse.Price,
                CourseImageName = newCourse.CourseImageName,
                TtlHr = newCourse.TtlHr,
                LearningText = newCourse.LearningText,
                RequirementText = newCourse.RequirementText,
                InsText = newCourse.InsText,
                TargetText = newCourse.TargetText,
                ShortIntroText = newCourse.ShortIntroText,
                TeacherPerSerilNo = newCourse.TeacherPerSerilNo,
                CourseSerilNo = newCourse.CourseSerilNo
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                List<CourseDataModel> result = connection.Query<CourseDataModel>(sql, parameters).ToList();
                return result;
            }

        }

        public List<CourseDataModel> DeleteCourse(string courseSerilNo) {
            string sql = @"Delete COURSE WHERE COURSE_SERIL_NO = @CourseSerilNo";
            object parameters = new
            {
                CourseSerilNo = courseSerilNo
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                connection.ExecuteScalar(sql, parameters);
                return this.SearchCourse(new CourseFilterModel());
            }
        }
    }
}
