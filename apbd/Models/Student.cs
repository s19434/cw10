using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace cw10.Models
{
    public partial class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }


        [JsonIgnore] public virtual Enrollment Enrollment { get; set; }
    }
}
