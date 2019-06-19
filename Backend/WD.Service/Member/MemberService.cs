using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD.Dao.Member;
using WD.Model;
using WD.Model.Member;

namespace WD.Service.Member
{
    public class MemberService
    {
        private MemberDao MemberDao = new MemberDao();
        public MemberDataModel Login(MemberDataModel member)
        {
            return this.MemberDao.Login(member);
        }

        public bool Register(MemberDataModel member)
        {
            return this.MemberDao.Register(member);
        }

        public bool IsAdmin(MemberDataModel member)
        {
            return this.MemberDao.IsAdmin(member);
        }

        public List<MemberDataModel> QueryUserData(MemberFilterModel arg)
        {
            return this.MemberDao.QueryUserData(arg);
        }

        public bool UpdateUserData(MemberDataModel membr)
        {
            return this.MemberDao.UpdateUserData(membr);
        }

        public SqlResult DeleteUserData(string perSerilNo)
        {
            return this.MemberDao.DeleteUserData(perSerilNo);
        }
    }
}
