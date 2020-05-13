using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace cw10.Models
{
    public partial class Enrollment
    {
        public Enrollment()
        {
            Student = new HashSet<Student>();
        }

        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }

        [JsonIgnore] public virtual Studies Study { get; set; }
        [JsonIgnore] public virtual ICollection<Student> Student { get; set; }

    }
}