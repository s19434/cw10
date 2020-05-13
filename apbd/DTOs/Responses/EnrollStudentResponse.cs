using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw10.Models;

namespace cw10.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        public int IdEnrollment { get; set; }
        public int IdStudy { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }

    }
}
