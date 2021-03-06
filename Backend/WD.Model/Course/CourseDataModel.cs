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
        public string CourseImageName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string TeacherPerSerilNo { get; set; }
        public string TeacherAccount { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherEName { get; set; }
        public string TeacherCName { get; set; }
        public string ModDate { get; set; }
        public string ModUsr { get; set; }
        public string CreDate { get; set; }
        public string CreUsr { get; set; }
        public int TtlHr { get; set; }
        public string LearningText { get; set; }
        public string RequirementText { get; set; }
        public string InsText { get; set; }
        public string TargetText { get; set; }
        public string ShortIntroText { get; set; }
    }
}
