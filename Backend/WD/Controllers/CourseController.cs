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
using WD.Service.Course;
using WD.Service.Member;

namespace WD.Controllers
{
    [RoutePrefix("course")]
    //[EnableCors("http://localhost:4200", "*", "*", SupportsCredentials = true)]
    public class CourseController : ApiController
    {
        private CourseService CourseService = new CourseService();
        private MemberService MemberService = new MemberService();

        [Route("search")]
        [HttpPost()]
        public IHttpActionResult SearchCourse(CourseFilterModel arg) {
            ApiResult apiResult = new ApiResult();
            apiResult.Data = this.CourseService.SearchCourse(arg);
            apiResult.Status = Models.Enum.ApiStatus.Success;
            apiResult.Message = "查詢成功";
            return Ok(apiResult);
        }

        [Route("delete")]
        [Authorize()]
        [HttpPost()]
        public IHttpActionResult DeleteCourse(CourseDataModel course)
        {
            MemberDataModel member = this.GetUser();
            ApiResult apiResult = new ApiResult();
            if (member != null && MemberService.IsAdmin(member)) {
                apiResult.Data = this.CourseService.DeleteCourse(course.CourseSerilNo);
                apiResult.Status = Models.Enum.ApiStatus.Success;
                apiResult.Message = "刪除成功";
            }
            else
            {
                apiResult.Status = Models.Enum.ApiStatus.Fail;
                apiResult.Message = "不要亂改";
            }

            return Ok(apiResult);
        }

        [HttpGet()]
        [Route("image/{courseEName}/{extension}")]
        public HttpResponseMessage GetCourseImage(string courseEName, string extension)
        {
            string path = WD.Common.Utility.ConfigUtility.GetAppSetting("CourseImagePath") + courseEName + '.' + extension;
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                response.Content = new StreamContent(file);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }
            catch (Exception ex)
            {
                path = WD.Common.Utility.ConfigUtility.GetAppSetting("CourseImagePath") + "default" + ".jpg";
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                response.Content = new StreamContent(file);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            }

            return response;
        }

        [Route("create")]
        [Authorize()]
        [HttpPost]
        public IHttpActionResult CreateCourse(CourseDataModel newCourse)
        {
            MemberDataModel member = this.GetUser();
            ApiResult apiResult = new ApiResult();
            if (member != null && MemberService.IsAdmin(member))
            {
                apiResult.Data = CourseService.CreateCourse(newCourse);
                apiResult.Status = Models.Enum.ApiStatus.Success;
                apiResult.Message = "新增成功";
            }
            else
            {
                apiResult.Status = Models.Enum.ApiStatus.Fail;
                apiResult.Message = "不要亂改";
            }

            return Ok(apiResult);


            //ApiResult result = new ApiResult();
            //result.Data = CourseService.CreateCourse(newCourse);
            //return Ok(result);
            
        }

        [Route("update")]
        [Authorize()]
        [HttpPost()]
        public IHttpActionResult UpdateCourse(CourseDataModel course)
        {
            MemberDataModel member = this.GetUser();
            ApiResult apiResult = new ApiResult();
            if (member != null && MemberService.IsAdmin(member))
            {
                apiResult.Data = this.CourseService.UpdateCourse(course);
                apiResult.Status = Models.Enum.ApiStatus.Success;
                apiResult.Message = "更新成功";
            }
            else
            {
                apiResult.Status = Models.Enum.ApiStatus.Fail;
                apiResult.Message = "不要亂改";
            }

            return Ok(apiResult);

            //ApiResult apiResult = new ApiResult();
            //apiResult.Data = this.CourseService.UpdateCourse(course);
            //apiResult.Status = Models.Enum.ApiStatus.Success;
            //apiResult.Message = "更新成功";
            //return Ok(apiResult);
        }

        [Route("uploadimage")]
        [Authorize()]
        public HttpResponseMessage UploadImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            MemberDataModel member = this.GetUser();
            if (member == null || !MemberService.IsAdmin(member)) {
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    string fileName = Guid.NewGuid().ToString();
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                        IList<string> allowedFileExtensions = new List<string> { ".jpg", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!allowedFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            //  where you want to attach your imageurl

                            //if needed write the code to update the table
                            fileName += extension;
                            var filePath = WD.Common.Utility.ConfigUtility.GetAppSetting("CourseImagePath") + fileName;
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = fileName.Replace(".","/");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }


        private MemberDataModel GetUser()
        {
            var user = HttpContext.Current.User;
            var cookies = Request.Headers.GetCookies();
            if (user?.Identity?.IsAuthenticated == true)
            {
                //取得 FormsIdentity
                var identity = (FormsIdentity)user.Identity;

                //取得 FormsAuthenticationTicket
                var ticket = identity.Ticket;

                //將 Ticket 內的 UserData 解析回 User 物件
                return JsonConvert.DeserializeObject<MemberDataModel>(ticket.UserData);
            }
            return null;
        }

    }
}
