using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MVC_PROJECT.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public string TeacherID { get; set; }
        public virtual ApplicationUser Teacher { get; set; }
        public int ClassID { get; set; }
        public virtual Class Class { get; set; }
    }
}