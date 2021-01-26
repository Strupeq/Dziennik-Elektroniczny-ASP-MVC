using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MVC_PROJECT.Models
{
    public class Grade
    {
        public int GradeID { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }
        public string StudentID { get; set; }
        public virtual ApplicationUser Student { get; set; }
        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; }
    }
}