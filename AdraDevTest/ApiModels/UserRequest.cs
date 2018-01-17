using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdraDevTest.ApiModels
{
    public class UserRequest
    {
        public int year { get; set; }
        public int month { get; set; }
        public int startYear { get; set; }
        public int startMonth { get; set; }
        public int endYear { get; set; }
        public int endMonth { get; set; }
        public string accountType { get; set; }
        public string fileContent { get; set; }
    }
}