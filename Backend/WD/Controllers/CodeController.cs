using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;
using WD.Model.Course;
using WD.Model.Member;
using WD.Models;
using WD.Service.Code;
using WD.Service.Course;
using WD.Service.Member;

namespace WD.Controllers
{
    [RoutePrefix("codeu")]
    public class CodeController : ApiController
    {
        private CodeService codeService = new CodeService();

        [Route("coursecategory")]
        public IHttpActionResult GetCourseCategory()
        {
            ApiResult apiResult = new ApiResult();
            apiResult.Data = codeService.GetCourseCategory();
            apiResult.Status = Models.Enum.ApiStatus.Success;
            apiResult.Message = "success";
            return Ok(apiResult);
        }

        [Route("teacherlist")]
        public IHttpActionResult GetTeacherList()
        {
            ApiResult apiResult = new ApiResult();
            apiResult.Data = codeService.GetTeacherList();
            apiResult.Status = Models.Enum.ApiStatus.Success;
            apiResult.Message = "success";
            return Ok(apiResult);
        }
    }
}
