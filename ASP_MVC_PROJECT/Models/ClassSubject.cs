using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MVC_PROJECT.Models
{
    public class ClassSubject
    {
        public int ClassSubjectID { get; set; }
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
    }
}