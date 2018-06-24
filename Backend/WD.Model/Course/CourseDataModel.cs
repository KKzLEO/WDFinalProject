﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WD.Model.Course
{
    public class CourseDataModel
    {
        public string CourseName { get; set; }
        public string CourseSerilNo { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}