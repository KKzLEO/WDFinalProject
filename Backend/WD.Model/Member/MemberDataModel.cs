using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WD.Model.Member
{
    public class MemberDataModel
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string EName { get; set; }
        public string CName { get; set; }
        public string TitleCode { get; set; }
        public string TitleName { get; set; }
        public string PerSerilNo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string GenderCode { get; set; }
        public string GenderText { get; set; }
        public string UserId { get; set; }
        public string Birthday { get; set; }
        public string FbToken { get; set; }
        public string FbId { get; set; }
        public int IsOut { get; set; }
        public bool IsModifyPassword { get; set; }
    }
}
