using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;
using WD.Model;
using WD.Model.Member;
using WD.Models;
using WD.Service.Member;

namespace WD.Controllers
{
    [RoutePrefix("access")]
    //[EnableCors("http://localhost:4200", "*", "*", SupportsCredentials = true)]
    public class MemberController : ApiController
    {
        private MemberService MemberService = new MemberService();
        [Route("login")]
        [HttpPost()]
        public IHttpActionResult Login(MemberDataModel member) {

            if (!string.IsNullOrEmpty(member.FbToken))
            {
                var client = new FacebookClient(member.FbToken);
                dynamic fbResult = client.Get("me", new { fields = "name,id,email" });
                member.FbId = fbResult.id;
            }

            if(!string.IsNullOrEmpty(member.Password)) member.Password = WD.Common.Utility.SecurityUtility.AESEnCrypt(member.Password);
            ApiResult apiResult = new ApiResult();
            MemberDataModel result = this.MemberService.Login(member);
            if (result == null)
            {
                apiResult.Message = string.IsNullOrEmpty(member.FbId) ? "登入失敗" : "請先註冊帳號唷";
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
            if(!string.IsNullOrEmpty(member.FbToken))
            {
                var client = new FacebookClient(member.FbToken);
                dynamic fbResult = client.Get("me", new { fields = "name,id,email" });
                member.FbId = fbResult.id;
            }
            

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

        [Route("queryuser")]
        [Authorize()]
        [HttpPost]
        public IHttpActionResult QueryUserData(MemberFilterModel arg) 
        {
            ApiResult apiResult = new ApiResult();
            MemberDataModel user = this.GetUser();
            if (user == null || !MemberService.IsAdmin(user))
            {
                apiResult.Message = "非法使用";
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                return Ok(apiResult);
            }
            try
            {
                apiResult.Data = this.MemberService.QueryUserData(arg);
                apiResult.Message = "查詢成功";
                apiResult.Status = Models.Enum.ApiStatus.Success;
            }
            catch (Exception e)
            {
                apiResult.Message = "系統錯誤，請聯絡系統管理員";
                apiResult.Status = Models.Enum.ApiStatus.Fail;
            }
            return Ok(apiResult);
        }

        [Route("createuser")]
        [Authorize()]
        [HttpPost]
        public IHttpActionResult CreateUserByAdmin(MemberDataModel member)
        {
            ApiResult apiResult = new ApiResult();
            MemberDataModel user = this.GetUser();
            if (user == null || !MemberService.IsAdmin(user))
            {
                apiResult.Message = "非法使用";
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                return Ok(apiResult);
            }

            member.Password = WD.Common.Utility.SecurityUtility.AESEnCrypt(member.Password);

            this.CheckUserData(member, "create", ref apiResult);
            
            if(apiResult.Status == Models.Enum.ApiStatus.CustomerError)
            {
                return Ok(apiResult);
            }

            try
            {
                bool isFinishRegister = this.MemberService.Register(member);
                if (isFinishRegister)
                {
                    apiResult.Data = this.MemberService.QueryUserData(new MemberFilterModel());
                    apiResult.Message = "新增成功";
                    apiResult.Status = Models.Enum.ApiStatus.Success;
                }
                else
                {
                    apiResult.Message = "新增失敗，已有相同帳號";
                    apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                }
            }
            catch (Exception e)
            {
                apiResult.Message = "系統錯誤，請聯絡系統管理員";
                apiResult.Status = Models.Enum.ApiStatus.Fail;
            }
            return Ok(apiResult);
        }

        [Route("updateuser")]
        [Authorize()]
        [HttpPost]
        public IHttpActionResult UpdateUserData(MemberDataModel member)
        {
            ApiResult apiResult = new ApiResult();
            MemberDataModel user = this.GetUser();
            if (user == null || !MemberService.IsAdmin(user))
            {
                apiResult.Message = "非法使用";
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                return Ok(apiResult);
            }
            try
            {
                if (member.IsModifyPassword)
                {
                    member.Password = WD.Common.Utility.SecurityUtility.AESEnCrypt(member.Password);
                }
                bool isSuccesful = this.MemberService.UpdateUserData(member);
                if (isSuccesful)
                {
                    apiResult.Data = this.MemberService.QueryUserData(new MemberFilterModel());
                    apiResult.Message = "更新成功";
                    apiResult.Status = Models.Enum.ApiStatus.Success;
                }
                else
                {
                    apiResult.Message = "更新失敗";
                    apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                }
            }
            catch
            {
                apiResult.Message = "系統錯誤，請聯絡系統管理員";
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
            }
            return Ok(apiResult);
        }


        [Route("deleteuser")]
        [Authorize()]
        [HttpPost]
        public IHttpActionResult DeleteUserData(MemberDataModel member)
        {
            ApiResult apiResult = new ApiResult();
            MemberDataModel user = this.GetUser();
            if (user == null || !MemberService.IsAdmin(user)) {
                apiResult.Message = "非法使用";
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                return Ok(apiResult);
            }
            try
            {
                SqlResult sqlResult = this.MemberService.DeleteUserData(member.PerSerilNo);
                if (sqlResult.Status == "success")
                {
                    apiResult.Data = this.MemberService.QueryUserData(new MemberFilterModel());
                    apiResult.Message = sqlResult.Message;
                    apiResult.Status = Models.Enum.ApiStatus.Success;
                }
                else
                {
                    apiResult.Message = sqlResult.Message;
                    apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                }
            }
            catch
            {
                apiResult.Message = "系統錯誤，請聯絡系統管理員";
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
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
            ApiResult apiResult = new ApiResult();
            apiResult.Data = this.GetUser();
            return Ok(apiResult);
        }


        [Route("validatetoken")]
        [Authorize()]
        [HttpPost]
        public IHttpActionResult ValidateToken() {
            MemberDataModel member = this.GetUser();
            ApiResult apiResult = new ApiResult();
            if (member!=null && MemberService.IsAdmin(member))
            {
                apiResult.Status = Models.Enum.ApiStatus.Success;
                apiResult.Message = "驗證成功";
            }
            else
            {
                apiResult.Status = Models.Enum.ApiStatus.Fail;
                apiResult.Message = "不要亂改";
            }
            return Ok(apiResult);
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

        private MemberDataModel GetUser() {
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

        private void CheckUserData(MemberDataModel member, string operation, ref ApiResult apiResult)
        {
            List<string> errors = new List<string>();
            Regex rgx4Email = new Regex(@"^(([^<>()[\]\\.,;:\s@\']+(\.[^<>()[\]\\.,;:\s@\']+)*)|(\'.+\'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$");
            Regex rgx4Phone = new Regex(@"^[0-9]*$");
            switch (operation)
            {
                case "create":
                    if (string.IsNullOrEmpty(member.Account)) { errors.Add("請輸入帳號"); }
                    if (string.IsNullOrEmpty(member.Password)) { errors.Add("請輸入密碼"); }
                    if (string.IsNullOrEmpty(member.Phone)) { errors.Add("請輸入電話"); }
                    if (string.IsNullOrEmpty(member.CName)) { errors.Add("請輸入中文名字"); }
                    if (string.IsNullOrEmpty(member.EName)) { errors.Add("請輸入英文名字"); }
                    if (string.IsNullOrEmpty(member.TitleCode)){ errors.Add("請選擇職位"); }
                    if(!rgx4Email.IsMatch(member.Email)) { errors.Add("信箱格式錯誤"); }
                    if (!rgx4Phone.IsMatch(member.Phone)) { errors.Add("聯絡電話格式錯誤"); }
                    break;
                case "update":
                    if (string.IsNullOrEmpty(member.Account)) { errors.Add("請輸入帳號"); }
                    if (string.IsNullOrEmpty(member.Password)) { errors.Add("請輸入密碼"); }
                    if (string.IsNullOrEmpty(member.Phone)) { errors.Add("請輸入電話"); }
                    if (string.IsNullOrEmpty(member.CName)) { errors.Add("請輸入中文名字"); }
                    if (string.IsNullOrEmpty(member.EName)) { errors.Add("請輸入英文名字"); }
                    if (string.IsNullOrEmpty(member.TitleCode)) { errors.Add("請選擇職位"); }
                    if (!rgx4Email.IsMatch(member.Email)) { errors.Add("信箱格式錯誤"); }
                    if (!rgx4Phone.IsMatch(member.Phone)) { errors.Add("聯絡電話格式錯誤"); }
                    break;
            }
            if(errors.Count() > 0)
            {
                apiResult.Message = String.Join(", ", errors.ToArray());
                apiResult.Status = Models.Enum.ApiStatus.CustomerError;
                return;
            }
        }

        //private bool 
    }
}
