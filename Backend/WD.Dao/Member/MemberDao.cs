using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD.Model;
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
            if (!string.IsNullOrEmpty(member.FbId))
            {
                return this.FbLogin(member);
            }
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
            string sql = @"EXEC sp_user_register @Account,@Password,@Email,@TitleCode,@UserId,@EName,@CName,@FbId,@GenderCode,@Phone,@Birthday";

            object parameters = new
            {
                Account = member.Account == null ? string.Empty : member.Account,
                Password = member.Password == null ? string.Empty : member.Password,
                Email = member.Email == null ? string.Empty : member.Email,
                TitleCode = member.TitleCode == null ? "1" : member.TitleCode,
                UserId = member.EName == null ? string.Empty : member.EName.Replace(" ", "_"),
                EName = member.EName == null ? string.Empty : member.EName,
                CName = member.CName == null ? string.Empty : member.CName,
                FbId = member.FbId == null ? string.Empty :member.FbId,
                GenderCode = member.GenderCode == null ? "0" : member.GenderCode,
                Phone = member.Phone == null ? string.Empty : member.Phone,
                Birthday = member.Birthday == null ? string.Empty : member.Birthday
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                string result = connection.ExecuteScalar(sql, parameters).ToString();
                if (result == "true") return true;
                else return false;
            }
        }

        public List<MemberDataModel> QueryUserData(MemberFilterModel arg)
        {
            string sql = @"SELECT U.ACCOUNT AS Account
                               ,PASSWORD AS Password
	                           ,EMAIL AS Email
	                           ,PHONE AS Phone
	                           ,CONVERT(varchar(100), BIRTHDAY, 23) As Birthday
	                           ,G.GENDER_CODE AS GenderCode
	                           ,G.GENDER_TEXT As GenderText
	                           ,U.USER_ID AS UserId
	                           ,U.E_NAME AS EName
	                           ,U.C_NAME AS CName
	                           ,U.PER_SERIL_NO AS PerSerilNo
	                           ,T.TITLE_NAME AS TitleName
	                           ,T.TITLE_CODE AS TitleCode
                        FROM USERS AS U
                        LEFT JOIN GENDER_CODE AS G
                        ON U.GENDER_CODE = G.GENDER_CODE
                        LEFT JOIN TITLE_CODE AS T
                        ON U.TITLE_CODE = T.TITLE_CODE 
                        WHERE U.IS_OUT <> 1 ";
            if (arg == null)
            {
                arg = new MemberFilterModel();
            }

            if (!string.IsNullOrEmpty(arg.Account))
            {
                sql += "AND ACCOUNT LIKE @Account ";
            }
            object parameters = new
            {
                Account = arg.Account == null ? string.Empty : '%' + arg.Account + '%'
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                List<MemberDataModel> result = connection.Query<MemberDataModel>(sql, parameters).ToList();
                return result;
            }
        }

        public bool UpdateUserData(MemberDataModel member)
        {
            string sql = @"UPDATE [dbo].[USERS]
                           SET [EMAIL] = @Email
                              ,[TITLE_CODE] = @TitleCode
                              ,[MOD_DATE] = GETDATE()
                              ,[E_NAME] = @EName
                              ,[C_NAME] = @CName
                              ,[USER_ID] = @UserId
                              ,[PHONE] = @Phone
                              ,[BIRTHDAY] = @Birthday
                              ,[GENDER_CODE] = @GenderCode
                              ,[IS_OUT] = @IsOut
                              ,[PASSWORD] = @Password
                         WHERE USERS.PER_SERIL_NO = @PerSerilNo";
            object parameters = new
            {
                Email = member.Email == null ? string.Empty : member.Email,
                TitleCode = member.TitleCode,
                EName = member.EName,
                CName = member.CName,
                UserId = member.EName.Replace(" ", "_"),
                Phone = member.Phone,
                Birthday = member.Birthday == null ? string.Empty : member.Birthday,
                GenderCode = member.GenderCode == null ? string.Empty : member.GenderCode,
                IsOut = member.IsOut,
                PerSerilNo = member.PerSerilNo,
                Password = member.Password
            };
            using(var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                int affectedRows = connection.Execute(sql, parameters);
                return affectedRows != 0;
            }
        }

        public SqlResult DeleteUserData(string perSerilNo)
        {
            string sql = @"EXEC sp_remove_user @PerSerilNo";
            object parameters = new
            {
                PerSerilNo = perSerilNo == null ? string.Empty : perSerilNo 
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                SqlResult sqlResult = connection.Query<SqlResult>(sql,parameters).ToList().FirstOrDefault();
                return sqlResult;
            }
        }

        public MemberDataModel FbLogin(MemberDataModel member) {
            string sql = @"SELECT ACCOUNT as Account,
	                               EMAIL as Email,
                                   usr.TITLE_CODE as TitleCode,
								   titcode.TITLE_NAME as TitleName,
	                               PER_SERIL_NO as PerSerilNo,
                                   E_NAME as EName,
                                   C_NAME as CName
                            FROM USERS(NOLOCK) as usr JOIN TITLE_CODE as titcode
							ON usr.TITLE_CODE = titcode.TITLE_CODE 
                            WHERE FB_ID = @FbId";
            object parameters = new
            {
                FbId = member.FbId == null ? string.Empty : member.FbId
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                MemberDataModel result = connection.Query<MemberDataModel>(sql, parameters).FirstOrDefault();
                return result;
            }
        }

        public bool IsAdmin(MemberDataModel member)
        {
            string sql = @"SELECT * FROM USERS WHERE TITLE_CODE = '0' AND ACCOUNT=@Account";
            object parameters = new
            {
                Account = member.Account
            };
            using (var connection = new SqlConnection(this.GetDbConnectionString()))
            {
                List<MemberDataModel> result = connection.Query<MemberDataModel>(sql, parameters).ToList();
                if (result.Count == 0) return false;
                else return true;
            }
        }
    }
}
