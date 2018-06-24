using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD.Dao.Member;
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
    }
}
