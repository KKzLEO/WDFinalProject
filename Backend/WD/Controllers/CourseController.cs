using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WD.Model.Course;
using WD.Models;
using WD.Service.Course;

namespace WD.Controllers
{
    [RoutePrefix("course")]
    public class CourseController : ApiController
    {
        private CourseService CourseService = new CourseService();

        [Route("search")]
        [HttpPost()]
        public IHttpActionResult SearchCourse(CourseFilterModel arg) {
            ApiResult apiResult = new ApiResult();
            apiResult.Data = this.CourseService.SearchCourse(arg);
            apiResult.Status = Models.Enum.ApiStatus.Success;
            apiResult.Message = "查詢成功";
            return Ok(apiResult);
        }

        [HttpGet()]
        [Route("image/{courseEName}")]
        public HttpResponseMessage GetCourseImage(string courseEName)
        {
            string path = WD.Common.Utility.ConfigUtility.GetAppSetting("CourseImagePath") + courseEName + ".jpg";

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                response.Content = new StreamContent(file);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }
            catch (Exception ex)
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                response.Content = new StreamContent(file);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            }

            return response;
        }
    }
}
