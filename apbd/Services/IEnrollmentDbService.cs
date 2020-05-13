
using cw10.DTOs.Requests;
using cw10.DTOs.Responses;
using cw10.Models;

namespace cw10.Services
{
    public interface IEnrollmentDbService
    {
        public bool IsExist(string name);
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest enrollment);
        public Enrollment Promote(PromoteStudentRequest promotion);
        public bool EnrollmentExists(string studies, int semester);
    }
}