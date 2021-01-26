using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MVC_PROJECT.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string Name { get; set; }
        public string ClassTeacherID { get; set; }
        public virtual ApplicationUser ClassTeacher { get; set; }
        public virtual ICollection<ApplicationUser> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}

