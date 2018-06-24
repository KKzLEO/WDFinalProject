using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD.Model.Member;

namespace WD.Dao.Member
{
    public class MemberDao
    {
        /// <summary>
        /// 獲得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDbConnectionString()
        {
            return WD.Common.Utility.ConfigUtility.GetDbConnectionString("DBConnection");
        }


        public MemberDataModel Login(MemberDataModel member)
        {
            string sql = @"SELECT ACCOUNT as Account,
	                               EMAIL as Email,
                                   usr.TITLE_CODE as TitleCode,
								   titcode.TITLE_NAME as TitleName,
	                               PER_SERIL_NO as PerSerilNo,
                                   E_NAME as EName,
                                   C_NAME as CName
                            FROM USERS(NOLOCK) as usr JOIN TITLE_CODE as titcode
							ON usr.TITLE_CODE = titcode.TITLE_CODE 
                            WHERE ACCOUNT = @Account AND PASSWORD = @Password";
            object parameters = new
            {
                Account = member.Account == null ? string.Empty : member.Account,
                Password = member.Password == null ? string.Empty : member.Password
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {

                MemberDataModel result = connection.Query<MemberDataModel>(sql, parameters).FirstOrDefault();
                return result;
            }
        }

        public bool Register(MemberDataModel member) {
            string sql = @"EXEC sp_user_register @Account,@Password,@Email,@TitleCode,@UserId,@EName,@CName";
            object parameters = new
            {
                Account = member.Account == null ? string.Empty : member.Account,
                Password = member.Password == null ? string.Empty : member.Password,
                Email = member.Email == null ? string.Empty : member.Email,
                TitleCode = member.TitleCode == null ? "1" : member.TitleCode,
                UserId = member.EName == null ? string.Empty : member.EName.Replace(" ", "_"),
                EName = member.EName == null ? string.Empty : member.EName,
                CName = member.CName == null ? string.Empty : member.CName
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                string result = connection.ExecuteScalar(sql, parameters).ToString();
                if (result == "true") return true;
                else return false;
            }

        }
    }
}
