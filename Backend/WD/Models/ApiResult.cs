using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WD.Models
{
    public class ApiResult
    {
        public Enum.ApiStatus Status { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}