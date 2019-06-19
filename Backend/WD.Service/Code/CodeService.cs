using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD.Dao.Code;
using WD.Dao.Course;
using WD.Model.Code;

namespace WD.Service.Code
{
    public class CodeService
    {
        private CodeDao codeDao = new CodeDao();

        public List<CodeDataModel> GetCourseCategory()
        {
            return codeDao.GetCourseCategory();
        }

        public List<CodeDataModel> GetTeacherList() {
            return codeDao.GetTeacherList();
        }

        public List<CodeDataModel> GetGenderCode()
        {
            return codeDao.GetGenderCode();
        }

        public List<CodeDataModel> GetTitleCode()
        {
            return codeDao.GetTitleCode();
        }
    }
}
