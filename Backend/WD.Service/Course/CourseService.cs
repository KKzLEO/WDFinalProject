using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD.Dao.Course;
using WD.Model.Course;

namespace WD.Service.Course
{
    public class CourseService
    {
        private CourseDao CourseDao = new CourseDao();
        public List<CourseDataModel> SearchCourse(CourseFilterModel arg)
        {
            return this.CourseDao.SearchCourse(arg);
        }

        public bool DeleteCourse(string courseSerilNo)
        {
            return this.CourseDao.DeleteCourse(courseSerilNo);
        }
    }
}
