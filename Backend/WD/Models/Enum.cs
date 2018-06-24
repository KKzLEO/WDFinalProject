using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WD.Models
{
    public class Enum
    {
        public enum ApiStatus
        {
            Success = 200,
            Fail = 500,
            CustomerError = 900
        }
    }
}