using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
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
            MemberDataModel result = this.MemberService.Login(member);
            if (result == null)
            {
                apiResult.Message = "登入失敗";
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
            }
            else
            {
                this.CreateCookie(result);
                apiResult.Data = result;
                apiResult.Message = "登入成功";
            }
            
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

        [Route("logout")]
        [HttpPost()]
        [Authorize()]
        public IHttpActionResult Logout() {
            this.ClearCookie();
            ApiResult apiResult = new ApiResult();
            apiResult.Message = "登出成功";
            apiResult.Status = Models.Enum.ApiStatus.Success;
            return Ok(apiResult);
        }

        [Route("testauth")]
        [Authorize()]
        [HttpPost]
        public IHttpActionResult TestAuth() {
            return Ok();
        }

        private void CreateCookie(MemberDataModel member) {
            var cookies = HttpContext.Current.Response.Cookies;

            //新增表單驗證用的票證
            var ticket = new FormsAuthenticationTicket(1,
                member.Account,                             //使用者名稱
                DateTime.Now,                               //發行時間
                DateTime.Now.AddMinutes(120),               //有效期限
                false,                                      //是否將 Cookie 設定成 Session Cookie，如果是則 Cookie 會在瀏覽器關閉後移除
                JsonConvert.SerializeObject(member),        //將要記錄的使用者資訊轉換為 JSON 字串
                FormsAuthentication.FormsCookiePath);       //儲存 Cookie 的路徑

            //將票證加密
            var encryptTicket = FormsAuthentication.Encrypt(ticket);

            //將票證寫入 Cookie
            cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket));
        }

        private void ClearCookie() {
            //移除瀏覽器的表單驗證票證
            FormsAuthentication.SignOut();
        }
    }
}
