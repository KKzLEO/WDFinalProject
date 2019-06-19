using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WD.Models;
using WD.Service.Code;

namespace WD.Controllers
{
    [RoutePrefix("code")]
    public class CodeXController : ApiController
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

        [Route("gendercode")]
        public IHttpActionResult GetGenderCode()
        {
            ApiResult apiResult = new ApiResult();
            apiResult.Data = codeService.GetGenderCode();
            apiResult.Status = Models.Enum.ApiStatus.Success;
            apiResult.Message = "success";
            return Ok(apiResult);
        }

        [Route("titlecode")]
        public IHttpActionResult GetTitleCode()
        {
            ApiResult apiResult = new ApiResult();
            apiResult.Data = codeService.GetTitleCode();
            apiResult.Status = Models.Enum.ApiStatus.Success;
            apiResult.Message = "success";
            return Ok(apiResult);
        }
    }
}