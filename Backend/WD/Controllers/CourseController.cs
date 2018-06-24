using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WD.Model.Course;

namespace WD.Controllers
{
    [RoutePrefix("course")]
    public class CourseController : ApiController
    {
        public IHttpActionResult SearchCourse(CourseFilterModel arg) {
            return Ok();
        }
    }
}
