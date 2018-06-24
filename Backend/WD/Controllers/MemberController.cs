using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WD.Model.Member;
using WD.Models;
using WD.Service.Member;

namespace WD.Controllers
{
    [RoutePrefix("access")]
    public class MemberController : ApiController
    {
        private MemberService MemberService = new MemberService();
        [Route("login")]
        [HttpPost()]
        public IHttpActionResult Login(MemberDataModel member) {
            member.Password = WD.Common.Utility.SecurityUtility.AESEnCrypt(member.Password);
            ApiResult apiResult = new ApiResult();
            apiResult.Data = this.MemberService.Login(member);
            return Ok(apiResult);
        }
        [Route("register")]
        [HttpPost()]
        public IHttpActionResult Register(MemberDataModel member) {
            member.Password = WD.Common.Utility.SecurityUtility.AESEnCrypt(member.Password);
            ApiResult apiResult = new ApiResult();
            bool isFinishRegister = this.MemberService.Register(member);
            if (isFinishRegister)
            {
                apiResult.Status = Models.Enum.ApiStatus.Success;
                apiResult.Message = "註冊成功";
            }
            else
            {
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                apiResult.Message = "註冊失敗，已有相同帳號";
            }
            return Ok(apiResult);
        }
    }
}
